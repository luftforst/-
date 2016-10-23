using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class GameData : Singleton<GameData> 
{
    public enum DialogType
    {
        dialog  = 0,
        select,
        objectEvent,
        ending,
        nextStage,
        returnMain
    }

    public enum SelectType
    {
        Positive = 0,
        Negative
    }

    public Dictionary<int, StageData>       _stageData       = new Dictionary<int, StageData>();

    public Dictionary<int, SelectType>      _selectType      = new Dictionary<int, SelectType>();

    public Dictionary<int, EventData>       _eventData       = new Dictionary<int, EventData>();

    public Dictionary<string, HeroineTouchData> _heroineTouchData    = new Dictionary<string,HeroineTouchData>();

    public Dictionary<string, EndingData>       _endingData          = new Dictionary<string,EndingData>();

    public class StageData
    {
        public int              index;
        public string           stageName;
        public int              touchMax;
        public string           sound;
        public string           soundEffect;

        public Dictionary<int, DialogData>  dialogData      = new Dictionary<int,DialogData>();

        public void Log()
        {
            Debug.Log(
                    "index : " + index +
                    ", stageName : " + stageName +
                    ", touchMax : " + touchMax +
                    ", sound : " + sound +
                    ", soundEffect : " + soundEffect
                );

        }
    }

    public class DialogData
    {
        public DialogType   type;
        public int          index;

        public string       character;

        public string       speaker;
        public string       dialog;

        public string       backgroundName;
        public string       expression;

        public string       sound;
        public string       soundEffect;

        public int          change;

        public void Log()
        {
            Debug.Log(
                "type : " + type + 
                "\nindex : " + index +
                "\nspeaker : " + speaker + 
                "\ndialog : " + dialog +
                "\nbackgroundName : " + backgroundName + 
                "\nexpression : " + expression +
                "\nsound : " + sound +
                "WnsoundEffect : " + soundEffect +
                "\nchange : " + change
                );
        }
    }

    public class EventData
    {
        public enum DataType 
        {
            SuccessData = 0,
            FailData
        }

        public string eventName;

        public int successLovePoint;
        public int successPage;

        public int failLovePoint;
        public int failPage;

        public void Log()
        {
            Debug.Log(
                "event name : " + eventName +
                ", successLovePoint : " + successLovePoint +
                ", successPage : " + successPage +
                ", failLovePoint : " + failLovePoint +
                ", failPage : " + failPage
                );
        }
    }

    public class HeroineTouchData
    {
        public int          index;
        public string       name;

        public List<TouchPoint> touchData       = new List<TouchPoint>();

        public void Log()
        {
            Debug.Log(
                "index : " + index +
                ", name : " + name
                );
        }

        public class TouchPoint
        {
            public int          point;
            public string       hitAreaName;
            public string       tapMotionName;

            public void Log()
            {
                Debug.Log(
                    "point : " + point +
                    ", hitAreaName : " + hitAreaName +
                    ", tapMotionName : " + tapMotionName
                    );
            }
        }
    }

    public class EndingData
    {
        public int          index;
        public string       name;
        public string       caption;
        public string       url;
        public string       message;

        public void Log()
        {
            Debug.Log(
                "index : " + index +
                ", name : " + name +
                ", caption : " + caption +
                ", url : " + url +
                ", message : " + message
                );
        }
    }

	void Start () 
    {
        // all data load
        LoadDialogData();

        // get heroine touch data
        LoadHeroineTouchData();

        // get facebook message data
        LoadFacebookMessageData();
	}

    void LoadDialogData()
    {
        JSONNode stage = new JSONNode();

        stage = DataParsing.Instance.ReadStringFromFile("Dialogue");

        // stage data
        int i = 0;
        while ( stage["StageData"][i] != null )
        {
            StageData stageObj   = new StageData();

            stageObj.index          = stage["StageData"][i]["index"].AsInt;
            stageObj.stageName      = stage["StageData"][i]["stage_name"];
            stageObj.touchMax       = stage["StageData"][i]["touch_point_max"].AsInt;
            stageObj.sound          = stage["StageData"][i]["sound"];
            stageObj.soundEffect    = stage["StageData"][i]["sound_effect"];

            _stageData[stageObj.index] = stageObj;

            //_stageData[stageObj.index].Log();

            // dialog data
            int j = 0;
            while (stage["StageData"][i]["Dialog"][j] != null)
            {
                DialogData dialogObj  = new DialogData();

                dialogObj.type              = (DialogType)stage["StageData"][i]["Dialog"][j]["type"].AsInt;
                dialogObj.index             = j;
                dialogObj.speaker           = stage["StageData"][i]["Dialog"][j]["speaker"];
                dialogObj.dialog            = stage["StageData"][i]["Dialog"][j]["dialog"];
                dialogObj.backgroundName    = stage["StageData"][i]["Dialog"][j]["background_name"];
                dialogObj.expression        = stage["StageData"][i]["Dialog"][j]["expression"];
                dialogObj.change            = stage["StageData"][i]["Dialog"][j]["change"].AsInt;

                if (stage["StageData"][i]["Dialog"][j]["character"] != null)
                    dialogObj.character = stage["StageData"][i]["Dialog"][j]["character"];

                if (stage["StageData"][i]["Dialog"][j]["sound"] != null)
                    dialogObj.sound = stage["StageData"][i]["Dialog"][j]["sound"];

                //dialogObj.Log();

                if (dialogObj.type == DialogType.objectEvent)
                {
                    EventData evt   = new EventData();

                    evt.eventName           = stage["StageData"][i]["Dialog"][j]["event"];

                    _eventData[j]           = LoadEventData(evt, evt.eventName);
                    
                    //evt.Log();

                    j++;
                    continue;
                }

                if (dialogObj.type == DialogType.select)
                {
                    _selectType[j] = (SelectType)stage["StageData"][i]["Dialog"][j]["select_type"].AsInt;
                }

                stageObj.dialogData[j]  = dialogObj;
                
                j++;
            }
            i++;
        }
    }

    public StageData getStageData(int index)
    {
        if (_stageData.ContainsKey(index))
        {
            return _stageData[index];
        }

        return null;
    }

    public StageData getCurrentStageData()
    {
        //Debug.Log("current stage : " + DataManager.Instance.curStage);
        return getStageData(DynamicDataManager.Instance._curStage);
    }

    public void LoadHeroineTouchData()
    {
        JSONNode heroineTouch = new JSONNode();

        heroineTouch = DataParsing.Instance.ReadStringFromFile("HeroineData");
        
        int i = 0;
        while (heroineTouch["Heroine"][i] != null)
        {
            HeroineTouchData heroineTouchData = new HeroineTouchData();

            heroineTouchData.index      = heroineTouch["Heroine"][i]["index"].AsInt;
            heroineTouchData.name       = heroineTouch["Heroine"][i]["name"];

            int j = 0;
            while (heroineTouch["Heroine"][i]["TouchPoint"][j] != null)
            {
                HeroineTouchData.TouchPoint touchPoint = new HeroineTouchData.TouchPoint();

                touchPoint.point                = heroineTouch["Heroine"][i]["TouchPoint"][j]["point"].AsInt;
                touchPoint.hitAreaName          = heroineTouch["Heroine"][i]["TouchPoint"][j]["hit_area_name"];
                touchPoint.tapMotionName        = heroineTouch["Heroine"][i]["TouchPoint"][j]["tap_motion"];

                heroineTouchData.touchData.Add(touchPoint);

                //touchPoint.Log();

                j++;
            }

            _heroineTouchData[heroineTouchData.name] = heroineTouchData;

            //heroineTouchData.Log();

            i++;
        }
    }


    public EventData LoadEventData(EventData evt, string name)
    {
        JSONNode eventData = new JSONNode();

        eventData = DataParsing.Instance.ReadStringFromFile("eventData");

        evt.successLovePoint    = eventData[name][(int)EventData.DataType.SuccessData]["success_point"].AsInt;
        evt.successPage         = eventData[name][(int)EventData.DataType.SuccessData]["success_page"].AsInt;
        evt.failLovePoint       = eventData[name][(int)EventData.DataType.FailData]["fail_point"].AsInt;
        evt.failPage            = eventData[name][(int)EventData.DataType.FailData]["fail_page"].AsInt;
        
        return evt;
    }

    public void LoadFacebookMessageData()
    {
        JSONNode ending = new JSONNode();

        ending = DataParsing.Instance.ReadStringFromFile("endingData");

        int i = 0;
        while (ending["ending"][i] != null)
        {
            EndingData endingMessage = new EndingData();

            endingMessage.index         = ending["ending"][i]["index"].AsInt;
            endingMessage.name          = ending["ending"][i]["name"];
            endingMessage.caption       = ending["ending"][i]["caption"];
            endingMessage.url           = ending["ending"][i]["url"];
            endingMessage.message       = ending["ending"][i]["message"];

            _endingData[endingMessage.name] = endingMessage;
            i++;
        }
    }

}
