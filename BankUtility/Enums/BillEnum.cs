using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.BankUtility.Enums
{
    public enum BillEnum
    {
        [EnumDescription("-")]
        NotSet = 0,
        [EnumDescription("نامشخص")]
        None = 0,
        [EnumDescription("شرکت آب و فاضلاب")]
        WaterDepartment = 10,
        [EnumDescription("شرکت توزیع نیروی برق")]
        PowerDepartment = 20,
        [EnumDescription("شرکت گاز")]
        GasDepartment = 30,
        [EnumDescription("شرکت مخابرات")]
        Telecommunications = 40,
        [EnumDescription("همراه اول")]
        Mci = 50,
        [EnumDescription("عوارض شهرداری")]
        TaxMunicipality = 60,
        [EnumDescription("سازمان مالیاتی")]
        TaxOrganization = 80,
        [EnumDescription("راهنمایی و رانندگی")]
        DrivingFine = 90,
    }
}
