using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FLogin : MonoBehaviour
{
    private FacebookManager     _facebook;

    void Start()
    {
        _facebook       = FacebookManager.Instance;
        _facebook.      initFB();
    }

    

    private void AuthCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.Log("error : " + result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("fb is login");

                _facebook.IsLoggedIn = true;
                _facebook.GetProfile();
            }
            else
            {
                Debug.Log("User cancelled login");
            }
        }
    }

    public void FacebookLogIn()
    {
        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }
}
