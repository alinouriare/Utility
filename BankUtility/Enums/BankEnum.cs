using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.BankUtility.Attributes;
using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.BankUtility.Enums
{
    public enum BankEnum
    {
        None = 0,

        [EnumDescription("نا مشخص")]
        [EnumShebaFormat("0")]
        [EnumCardFormat("0")]
        Unknwon = 1,

        [EnumDescription("توسعه صادرات ایران")]
        [EnumShebaFormat("020")]
        [EnumCardFormat("207177", "627648")]
        SaderatImprove = 10,

        [EnumDescription("کارآفرین")]
        [EnumShebaFormat("053")]
        [EnumCardFormat("502910", "627488")]
        Entrepreneur = 20,

        [EnumDescription("سپه")]
        [EnumShebaFormat("015")]
        [EnumCardFormat("589210")]
        Sepah = 30,

        [EnumDescription("رفاه کارگران")]
        [EnumShebaFormat("013")]
        [EnumCardFormat("589463")]
        RefahWorkers = 40,

        [EnumDescription("صادرات ایران")]
        [EnumShebaFormat("019")]
        [EnumCardFormat("603769")]
        Saderat = 50,

        [EnumDescription("کشاورزی")]
        [EnumShebaFormat("016")]
        [EnumCardFormat("603770", "639217")]
        Agricultural = 60,

        [EnumDescription("ملّی")]
        [EnumShebaFormat("017")]
        [EnumCardFormat("603799")]
        Melli = 70,

        [EnumDescription("ملّت")]
        [EnumShebaFormat("012")]
        [EnumCardFormat("610433")] //[EnumCardFormat("991975", "610433")]
        Mellat = 80,

        [EnumDescription("سامان")]
        [EnumShebaFormat("056")]
        [EnumCardFormat("621986")]
        Saman = 90,

        [EnumDescription("پارسیان")]
        [EnumShebaFormat("054")]
        [EnumCardFormat("622106", "627884", "639194")]
        Parsian = 100,

        [EnumDescription("تجارت")]
        [EnumShebaFormat("018")]
        [EnumCardFormat("627353", "585983")]
        Tejarat = 110,

        [EnumDescription("اقتصاد نوین")]
        [EnumShebaFormat("055")]
        [EnumCardFormat("627412")]
        EghtesadNovin = 120,

        [EnumDescription("پست ایران")]
        [EnumShebaFormat("021")]
        [EnumCardFormat("627760")]
        PostBank = 130,

        [EnumDescription("صنعت و معدن")]
        [EnumShebaFormat("011")]
        [EnumCardFormat("627961")]
        Industry = 140,

        [EnumDescription("مسکن")]
        [EnumShebaFormat("014")]
        [EnumCardFormat("628023")]
        Maskan = 150,

        [EnumDescription("موسسه اعتباری توسعه")]
        [EnumShebaFormat("051")]
        [EnumCardFormat("628157")]
        ImproveCredential = 160,

        [EnumDescription("مرکزی")]
        [EnumShebaFormat("010")]
        [EnumCardFormat("636795")]
        Central = 170,

        [EnumDescription("پاسارگاد")]
        [EnumShebaFormat("057")]
        [EnumCardFormat("502229", "639347")]
        Pasrgad = 180,

        [EnumDescription("سرمایه")]
        [EnumShebaFormat("058")]
        [EnumCardFormat("639607")]
        Capital = 190,

        [EnumDescription("شهر")]
        [EnumShebaFormat("061")]
        [EnumCardFormat("502806", "504706")]
        City = 200,

        [EnumDescription("توسعه تعاون")]
        [EnumShebaFormat("022")]
        [EnumCardFormat("502908")]
        CooperationImprove = 210,

        [EnumDescription("دی")]
        [EnumShebaFormat("066")]
        [EnumCardFormat("502938")]
        Dey = 220,

        [EnumDescription("گردشگری")]
        [EnumShebaFormat("064")]
        [EnumCardFormat("505416")]
        Travel = 230,

        [EnumDescription("ایران زمین")]
        [EnumShebaFormat("069")]
        [EnumCardFormat("505785")]
        IranZamin = 240,

        [EnumDescription("موسسه اعتباری کوثر")]
        [EnumShebaFormat("")]
        [EnumCardFormat("505801")]
        Kosar = 250,

        [EnumDescription(" قرض الحسنه مهر ایران")]
        [EnumShebaFormat("060")]
        [EnumCardFormat("606373")]
        MeherIran = 260,

        [EnumDescription("انصار")]
        [EnumShebaFormat("063")]
        [EnumCardFormat("627381")]
        Ansar = 270,

        [EnumDescription("آینده")]
        [EnumShebaFormat("062")]
        [EnumCardFormat("636214")]
        Ayande = 280,

        [EnumDescription("حکمت ایرانیان")]
        [EnumShebaFormat("065")]
        [EnumCardFormat("636949")]
        Hekmat = 290,

        [EnumDescription("سینا")]
        [EnumShebaFormat("059")]
        [EnumCardFormat("639346")]
        Sina = 300,

        [EnumDescription("مهر اقتصاد")]
        [EnumShebaFormat("")]
        [EnumCardFormat("639370")]
        MehrEghtesad = 310,

        [EnumDescription("قوامین")]
        [EnumShebaFormat("052")]
        [EnumCardFormat("639599")]
        Ghavamin = 320,

        /*[EnumDescription("تات")]
        [EnumShebaFormat("")]
        [EnumCardFormat("636214")]
        Tat = 330,/**/

        [EnumDescription("رسالت")]
        [EnumShebaFormat("070")]
        [EnumCardFormat("504172")]
        Resalat = 340,

        [EnumDescription("ایران ونزوئلا")]
        [EnumShebaFormat("095")]
        [EnumCardFormat("000000")]
        IranVenezuelaBank = 350,

        [EnumDescription("خاورمیانه")]
        [EnumShebaFormat("078")]
        [EnumCardFormat("585947")]
        MiddleEastBank = 360,
    }
}
