using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Quiz : MonoBehaviour 
{
    public Button           dialogButton;
    public Text             dialogText;
    public Text             dialogName;

    public Button[]         select;
    public Text[]           text;

    public List<QuizManager.QuizData>   quiz;
    private int                         quizNumber      = 0;

    private string                      name;

    private int                         right           = 0;    // 맞힌 개수
    private const int                   _myAnswer       = 5;

	void Start() 
    {
        if (this.gameObject.name == "quiz1")
        {
            quiz = QuizManager.Instance.quiz1;
            name = "흑화";
        }
        else if (this.gameObject.name == "quiz2")
        {
            quiz = QuizManager.Instance.quiz2;
            name = "안대례";
        }
	}

	void Update() 
    {
        if (quizNumber >= 7)
        {
            SceneManager.Instance.IsEvent = true;

            if (right >= _myAnswer)
            {
                Debug.Log("합격");
                GaugeDestiny.Instance.setDestinyPoint(true);
            }

            else
            {
                Debug.Log("불합격");
                GaugeDestiny.Instance.setDestinyPoint(false);
            }

            this.gameObject.SetActive(false);

            return;
        }

        dialogName.text = name;
        dialogText.text = quiz[quizNumber].question;

        for (int i = 0; i < 3; i++)
        {
            text[i].text = quiz[quizNumber].select[i];
        }
	}

    public void Judge(int select_number)
    {
        if (quiz[quizNumber].rightAnswer == select_number)
        {
            right++;
        }

        quizNumber++;
    }
}
