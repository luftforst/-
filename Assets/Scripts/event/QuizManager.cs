using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 흑화, 안대례 문제
 * 일회성 이벤트이므로 그냥 하드코딩
 */

public class QuizManager : Singleton<QuizManager> 
{
    public class QuizData
    {
        public string       question;
        public string[]     select          = new string[3];
        public int          rightAnswer;

        public void Log()
        {
            Debug.Log("select : " + select);
        }
    }

    public List<QuizData>       quiz1           = new List<QuizData>();   // 흑화
    public List<QuizData>       quiz2           = new List<QuizData>();   // 안대례

    void Start()
    {
        setQuiz1();
        setQuiz2();
    }

    void setQuiz1()
    {
        QuizData data = new QuizData();

        // 1
        data.question = "재가 어렸을 떼 부모님이 돌아가셨고";

        data.select[0] = "제가 어렸을때 부모님이 돌아가셨고";
        data.select[1] = "졔가 어렸을 때 부모님이 도라가셨고";
        data.select[2] = "제가 어렸을 때 부모님이 돌아가셨고";

        data.rightAnswer = 3;

        quiz1.Add(data);

        // 2
        data = new QuizData();

        data.question = "그이후로부터는 언니랑 두리 사랏어요.";

        data.select[0] = "그 이후로부터는 언니랑 둘이 살았어요";
        data.select[1] = "그 이후로 부터는 언니랑 둘이 살았어요";
        data.select[2] = "그 이후로 부터는 언니랑 둘이 살앗어요";

        data.rightAnswer = 1;

        quiz1.Add(data);

        // 3
        data = new QuizData();

        data.question = "언니는 저를 위헤 벼르별일을 다당하며 노력헷지만";

        data.select[0] = "언니는 저를 위해 별의 별 일을 다 당하며 노력했지만";
        data.select[1] = "언니는 저를 위해 별의 별 일을 다 당하며 노오력했지만";
        data.select[2] = "언니는 저를 위해 별의별 일을 다 당하며 노력했지만";

        data.rightAnswer = 3;

        quiz1.Add(data);

        // 4
        data = new QuizData();

        data.question = "결국 언니는 음시글 훔친제로 곤장을 맏아 주꼬마랐죠.";

        data.select[0] = "결국 언니는 음식을 훔친 죄로 곤장을 맞아 죽고 말았조";
        data.select[1] = "결국 언니는 음식을 훔친 죄로 곤장을 맞아 죽고 말았죠";
        data.select[2] = "결국 언니는 음식을 훔친죄로 곤장을 맞아 죽고 말았죠";

        data.rightAnswer = 2;

        quiz1.Add(data);

        // 5
        data = new QuizData();

        data.question = "그런대 언니가 주꼬난이후로부터 씐긔한 이리 이러나긔 시작헷어요.";

        data.select[0] = "그런데 언니가 죽고 난 이후로부터 신기한 일이 일어나기 시작했어요";
        data.select[1] = "그런대 언니가 죽고 난 이후로부터 신기한 이리 일어나기 시작했어요";
        data.select[2] = "그런데 언니가 죽고 난 이후로부터 신긔한 일이 일어나기 시작했어요";

        data.rightAnswer = 1;

        quiz1.Add(data);

        // 6
        data = new QuizData();

        data.question = "기싱이보이기시작헸거등여!";

        data.select[0] = "귀신이 보이기 시작 헸거든요!";
        data.select[1] = "귀신이 보이기 시작했거든요!";
        data.select[2] = "귀신이 보이기 시작 했거든요!";

        data.rightAnswer = 2;

        quiz1.Add(data);

        // 7
        data = new QuizData();

        data.question = "그떼 이후로 언니랑 가치 살며 구슬 하며 머꼬살고이써요.";

        data.select[0] = "그때 이후로 언니랑 같이 살며 굿을 하며 먹고살고 있어요";
        data.select[1] = "그 때 이후로 언니랑 같이 살며 굿을 하며 먹고살고 있어요";
        data.select[2] = "그때 이후로 언니랑 같이 살며 굿을 하며 먹고 살고 있어요";

        data.rightAnswer = 1;

        quiz1.Add(data);

        //for (int i = 0; i < 7; i++)
        //{
        //    for (int j = 0; j < 3; j++)
        //        Debug.Log("quiz1 " + i + " : " + quiz1[i].select[j]);
        //}
    }

    void setQuiz2()
    {
        QuizData data = new QuizData();

        // 1
        data.question = "누구?";

        data.select[0] = "이 친구는, 소똥이라고 하는데 이름은 흔해빠졌지만 인성만큼은 아주 진국인 친구입니다.";
        data.select[1] = "이 친구는, 한 집에 있을 때 많은 도움을 받은 친구입니다.";
        data.select[2] = "이 친구는, 그 무엇과도 바꿀 수 없는 가장 소중하고 중요한 친구입니다.";

        data.rightAnswer = 1;

        quiz2.Add(data);

        // 2
        data = new QuizData();

        data.question = "아~ 아주 오래 알고 지낸 사이인가 보오?";

        data.select[0] = "그렇습니다. 아주 오래전부터 알고 지낸 친구인데, 이제 이 친구 없이 어떻게 살아가야 할지...";
        data.select[1] = "그렇습니다. 가족처럼 한시도 떨어진 적 없던 사이죠. ";
        data.select[2] = "그렇습니다. 많은 시간을 함께했죠.";

        data.rightAnswer = 3;

        quiz2.Add(data);

        // 3
        data = new QuizData();

        data.question = "흐응... 아주 애틋한 사이인가 보오~?";

        data.select[0] = "네, 그렇죠! (아주 기쁜 듯이)";
        data.select[1] = "네, 그렇죠... (먼 산을 보며 아련하게)";
        data.select[2] = "네, 그렇죠. (과거를 회상하는 듯 촉촉하게)";

        data.rightAnswer = 1;

        quiz2.Add(data);

        // 4
        data = new QuizData();

        data.question = "...";

        data.select[0] = "소똥이와 재회의 기쁨을 나눈다.";
        data.select[1] = "안대례에게 왜 그러냐고 물어본다.";
        data.select[2] = "안대례의 눈치를 본다";

        data.rightAnswer = 2;

        quiz2.Add(data);

        // 5
        data = new QuizData();

        data.question = "(앞머리를 쓸어넘기며 소똥이를 노려본다)";

        data.select[0] = "안대례의 눈치를 본다";
        data.select[1] = "소똥이를 왜 노려보는 거예요?!";
        data.select[2] = "왜 그래요? 혹시 어디 아파요?";

        data.rightAnswer = 3;

        quiz2.Add(data);

        // 6
        data = new QuizData();

        data.question = "기분이 좋지 않군.";

        data.select[0] = "소똥이와 하던 얘기를 멈추고 가만히 있는다.";
        data.select[1] = "안대례를 무시한 채 소똥이와 웃으며 대화를 나눈다.";
        data.select[2] = "안대례의 옷깃을 잡아당긴다.";

        data.rightAnswer = 3;

        quiz2.Add(data);

        // 7
        data = new QuizData();

        data.question = "(천민의 손을 끌고 천민을 자신의 뒤로 숨긴다)";

        data.select[0] = "(화를 내며) 대체 왜 이러는 거예요!";
        data.select[1] = "(영문을 모르겠다는 듯) 대체 왜 이러는 거예요?";
        data.select[2] = "(소심하게) 대체 왜 이러는 거예요...";

        data.rightAnswer = 2;

        quiz2.Add(data);

        /*
         * 			//오브젝트 이벤트_소똥이랑 무슨 사이야 끝
					//호감도 max -> 내시 해피 change : 118
					//호감도 max X || 운명게이지 max X -> 내시 배드 change : 125
					//호감도 max X & 운명게이지 max -> 다음 히로인 change : 139
         */
    }
}
