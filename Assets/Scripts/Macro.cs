using System.Collections.Generic;

public class Macro
{
    enum Type
    {
        up = 0,
        down,
        right,
        left
    }

    // image
    public const string IMAGE_TITLE             = "title";
    public const string IMAGE_ENDING_TITLE      = "_ending_title";

    public const string IMAGE_ENDING            = "_ending";

    public const string IMAGE_DIALOG            = "dialog";
    public const string IMAGE_DIALOG_OUT        = "_out";  // 꼬리표가 화면 밖으로 향함
    public const string IMAGE_DIALOG_IN         = "_in";    // 꼬리표가 화면 안으로 향함
    public const string IMAGE_DIALOG_NARRATION  = "_narration";    // 꼬리표가 없음

    // sound
    public const string SOUND_CLEAR             = "도장";
    public const string SOUND_SMASH             = "찰싹";
    public const string SOUND_TOUCH             = "touch";
    public const string SOUND_PAGE              = "page";

    public const string SOUND_TITLE             = "5백억년전별빛_bgm";

    // Scene
    public const string SCENE_TITLE             = "Title";
    public const string SCENE_LOADING           = "Loading";
    public const string SCENE_DATING            = "Dating";

    // character
    public const string HERO                        = "천민";

    // 미연시 live2d 히로인 불러올때
    public static readonly string[] HEROINE         = { "자운영", "흑화", "을씨년", "안대례", "이왕" };

    // stage heroine
    public static readonly string[] STAGE           = { "자운영", "자운영", "흑화/을씨년", "안대례", "이왕" };
    public static readonly string[] START_HEROINE   = { "자운영", "자운영", "흑화", "안대례", "이왕" };

    // ending
    public static readonly string[] ENDING_STAGE    = { "자운영", "흑화", "을씨년", "안대례", "이왕", "이왕_배드" };


}
