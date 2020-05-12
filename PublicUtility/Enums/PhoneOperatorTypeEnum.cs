using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.PublicUtility.Enums
{
    public enum PhoneOperatorTypeEnum
    {
        [EnumDescription("نامشخص")]
        Null = 0,

        #region Phone City Area

        [EnumDescription("آذربایجان شرقی 041")]
        EastAzerbaijan_041 = 041,

        [EnumDescription("آذربایجان غربی 044")]
        WestAzerbaijan_044 = 044,

        [EnumDescription("اردبیل 045")]
        Ardabil_045 = 045,
        
        [EnumDescription("اصفهان 031")]
        Isfahan_031 = 031,

        [EnumDescription("البرز 026")]
        Alborz_026 = 026,

        [EnumDescription("ایلام 084")]
        Ilam_084 = 084,
        
        [EnumDescription("بوشهر 077")]
        Bushehr_077 = 077,
        
        [EnumDescription("تهران 021")]
        Tehran_021 = 021,

        [EnumDescription("خراسان جنوبی 056")]
        KhorasanSouth_056 = 056,

        [EnumDescription("خراسان رضوی 051")]
        KhorasanRazavi_051 = 051,

        [EnumDescription("خراسان شمالی 058")]
        KhorasanNorth_058 = 058,

        [EnumDescription("زنجان 024")]
        Zanjan_024 = 024,

        [EnumDescription("سمنان 023")]
        Semnan_023 = 023,

        [EnumDescription("سیستان و بلوچستان 054")]
        SistanBaluchestan_054 = 054,

        [EnumDescription("فارس 071")]
        Fars_071 = 071,

        [EnumDescription("قزوین 028")]
        Qazvin_028 = 028,

        [EnumDescription("قم 025")]
        Qom_025 = 025,

        [EnumDescription("لرستان 066")]
        Lorestan_066 = 066,

        [EnumDescription("مازندران 011")]
        Mazandaran_011 = 011,

        [EnumDescription("مرکزی 086")]
        Markazi_086 = 086,

        [EnumDescription("هرمزگان 076")]
        Hormozgan_076 = 076,

        [EnumDescription("همدان 081")]
        Hamadan_081 = 081,

        [EnumDescription("چهارمحال و بختیاری 038")]
        ChaharMahaalBakhtiari_038 = 038,

        [EnumDescription("کردستان 087")]
        Kurdistan_087 = 087,

        [EnumDescription("کرمان 034")]
        Kerman_034 = 034,

        [EnumDescription("کرمانشاه 083")]
        Kermanshah_083 = 083,

        [EnumDescription("کهگیلویه و بویراحمد 074")]
        KohgiluyehBoyerAhmad_074 = 074,

        [EnumDescription("گلستان 017")]
        Golestan_017 = 017,

        [EnumDescription("گیلان 013")]
        Gilan_013 = 013,

        [EnumDescription("یزد 035")]
        Yazd_035 = 035,
        
        #endregion

        #region MCI Area

        [EnumDescription("اپراتور اول ، همراه اول 0990")]
        Mci_0990 = 0990,

        [EnumDescription("اپراتور اول ، همراه اول 0991 ")]
        Mci_0991 = 0991,
        
        [EnumDescription("اپراتور اول ، همراه اول 0910 ")]
        Mci_0910 = 0910,

        [EnumDescription("همراه اول استان گلستان، گیلان، مازندران 0911")]
        Mci_0911 = 0911,

        [EnumDescription("همراه اول استان تهران، البرز، زنجان، سمنان، قزوین، قم 0912")]
        Mci_0912 = 0912,

        [EnumDescription("همراه اول استان اصفهان، کرمان، یزد، چهارمحال و بختیاری 0913")]
        Mci_0913 = 0913,

        [EnumDescription("همراه اول استان آذربایجان شرقی، غربی، اردبیل 0914")]
        Mci_0914 = 0914,

        [EnumDescription("همراه اول استان خراسان شمالی، رضوی، جنوبی، سیستان و بلوچستان 0915")]
        Mci_0915 = 0915,

        [EnumDescription("همراه اول استان خوزستان، لرستان 0916")]
        Mci_0916 = 0916,

        [EnumDescription("همراه اول استان فارس، کهگیلویه و بویر احمد، هرمزگان، بوشهر 0917")]
        Mci_0917 = 0917,

        [EnumDescription("همراه اول استان همدان، ایلام، مرکزی، کردستان، کرمانشاه 0918")]
        Mci_0918 = 0918,

        [EnumDescription("اپراتور اول ، همراه اول 0919")]
        Mci_0919 = 0919,
        
        #endregion

        #region Irancell Area

        [EnumDescription("ایرانسل 0901")]
        Mtn_0901 = 0901,
        [EnumDescription("ایرانسل 0902")]
        Mtn_0902 = 0902,
        [EnumDescription("ایرانسل 0903")]
        Mtn_0903 = 0903,
        [EnumDescription("ایرانسل 0905")]
        Mtn_0905 = 0905,
        [EnumDescription("ایرانسل 0930")]
        Mtn_0930 = 0930,
        [EnumDescription("ایرانسل 0933")]
        Mtn_0933 = 0933,
        [EnumDescription("ایرانسل 0935")]
        Mtn_0935 = 0935,
        [EnumDescription("ایرانسل 0936")]
        Mtn_0936 = 0936,
        [EnumDescription("ایرانسل 0937")]
        Mtn_0937 = 0937,
        [EnumDescription("ایرانسل 0938")]
        Mtn_0938 = 0938,
        [EnumDescription("ایرانسل 0939")]
        Mtn_0939 = 0939,

        #endregion

        #region Raytel Area

        [EnumDescription("رایتل ، تامین تلکام 0920")]
        Raytel_0920 = 0920,
        [EnumDescription("رایتل ، تامین تلکام 0921")]
        Raytel_0921 = 0921,
        [EnumDescription("رایتل ، تامین تلکام 0922")]
        Raytel_0922 = 0922,

        #endregion

        #region Talia Area
        [EnumDescription("اپراتور تالیا 0932")]
        Taliya_0932 = 0932,
        #endregion

        #region Spadan Area

        [EnumDescription("اسپادان 0931")]
        Spadan_0931 = 0931,

        #endregion

        #region TKC Area

        [EnumDescription("شبکه مستقل تلفن همراه کیش 0934")]
        TKC_0934 = 0934,

        #endregion
    }
}
