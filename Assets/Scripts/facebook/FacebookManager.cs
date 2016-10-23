using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour 
{
    private static FacebookManager         _instance;

	public static FacebookManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FacebookManager>();
            }
            return _instance;
        }
    }

    public bool             IsLoggedIn          { get; set; }
    public string           AppLinkURL          { get; set; }


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _instance = this;
        IsLoggedIn = true;
    }


    public void initFB()
    {
        if (!FB.IsInitialized)
            FB.Init(SetInit, OnHideUnity, null);
        else
            IsLoggedIn = FB.IsLoggedIn;
    }

    private void SetInit()
    {
        Debug.Log("FB init done");

        if (FB.IsLoggedIn)
            Debug.Log("log in");
        else
            Debug.Log("log out");

        IsLoggedIn = FB.IsLoggedIn;
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void GetProfile()
    {
        FB.GetAppLink(DealWithAppLink);
        Share();
    }

    void DealWithAppLink(IAppLinkResult result)
    {
        if (!string.IsNullOrEmpty(result.Url))
        {
            AppLinkURL = "" + result.Url + "";
            Debug.Log(AppLinkURL);
        }
        else
            AppLinkURL = "https://play.google.com/store/apps/details?id=com.exdio.dokidoki";
    }

    public void Share()
    {
        string heroine = SceneManager.Instance.ClearName;

        FB.ShareLink (
            new System.Uri(AppLinkURL),
            "두근두근 나으리",
            GameData.Instance._endingData[heroine].message,
            new System.Uri(GameData.Instance._endingData[heroine].url)
            );

        // 보안이 위험한 코드, sharelink 함수를 권장함
        //FB.FeedShare(
        //    string.Empty,
        //    new System.Uri(AppLinkURL),
        //    "두근두근 나으리",
        //    GameData.Instance._endingData[heroine].caption,
        //    GameData.Instance._endingData[heroine].message,
        //    new System.Uri(GameData.Instance._endingData[heroine].url),
        //    string.Empty,
        //    ShareCallback
        //    );
    }

    void ShareCallback(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("share cancelled");
        }
        else if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("error on share");
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("success on share");
        }
    }
}
