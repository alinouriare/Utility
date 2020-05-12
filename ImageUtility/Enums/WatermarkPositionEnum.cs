using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.ImageUtility.Enums
{
    public enum WatermarkPositionEnum
    {
        [EnumDescription("بالا چپ")]
        TopLeft = 10,
        [EnumDescription("بالا راست")]
        TopRight = 20,
        [EnumDescription("پایین چپ")]
        BottomLeft = 30,
        [EnumDescription("پایین راست")]
        BottomRight = 40,
        [EnumDescription("تکرار در تمام تصویر")]
        Repeat = 50,
    }
}
