using System.ComponentModel;

namespace Ananas.Web.Mvc.Base
{
    class DataEnum
    {
    }

    //登录的错误类型
    public enum UserError
    {
        NameError = 0,
        PswdError = 1,
        EmailError = 2,
        CodeError = 3,
    };

    //验证码类型
    public enum ValidateType
    {
        Number = 0,
        Letter = 1,
        NumberLetter = 2,
    };

    //页面类型
    public enum PageType
    {
        Index=0,
        Projects=1,
        Works=2,
        Code=3,
        Musics=4,
        Images=5,
        Novels=6,
        Home=7,
    }

    //文件类型
    public enum FileType
    {
        MusicLocal = 0,
        MusicWeb = 1,
        Image = 2,
        NovelLocal = 3,
        NovelWeb = 4,
        TxImage = 5,
        ImageAlbum = 6,
        MusicAlbum = 7,
        DefinedImage=8,
        CodeAlbum=9,
        Code=10,
    };

    //文件类型
    public enum CommentType
    {
        Commrnt = 0,
        Zan = 1,
    };

    //首页插件类型
    public enum PluginType
    {
        [Description("空白")]
        Blank=0,
        [Description("文本链接")]
        TextLink = 1,
        [Description("图标链接")]
        IconLink = 2,
        [Description("图片预览器")]
        ImageView=3,
        [Description("时钟")]
        ClockPlugin=4,
        [Description("音乐播放器")]
        MusicPlay=5,
        [Description("天气插件")]
        WeatherPlugin=6,
        [Description("视频播放器")]
        VideoPlugin = 7,
        [Description("网页插件")]
        HtmlPlugin = 8,
    };

    //首页类型
    public enum HomeType
    {
        Group = 0,
        Block= 1,
    };


    public enum NovelType
    {
        [Description("言情")]
        YanQing = 0,
        [Description("科幻恐怖")]
        KeHuan = 1,
        [Description("最喜欢")]
        Favourite = 2,
        [Description("其他")]
        Other = 3,
    }

}
