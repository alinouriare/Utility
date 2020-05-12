using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.VideoUtility.Enums
{
    public enum VideoEncodeEnum
    {
        [EnumDescription("240p")]
        Encode240 = 240,
        [EnumDescription("360p")]
        Encode360 = 360,
        [EnumDescription("480p")]
        Encode480 = 480,
        [EnumDescription("720p")]
        Encode720 = 720,
        [EnumDescription("1080p")]
        Encode1080 = 1080,
    }
}
