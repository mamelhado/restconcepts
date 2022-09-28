using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Infra.Data.Context
{
    public static class DataBaseInformation
    {
        public const string SCHEMA = "rest";
        public const string ColumnId = "id";

        #region AddressEntity
        public const string Address_Table = "tb_address";
        public const string Address_Street = "street";
        public const string Address_Number = "number";
        public const string Address_SuplementarInfo = "suplementar_info";
        public const string Address_State = "state";
        public const string Address_City = "city";
        public const string Address_ZipCode = "zip_code";
        #endregion

        #region CustomerEntity
        public const string Customer_Table = "tb_customer";
        public const string Customer_Birth = "birth";
        public const string Customer_LastName = "last_name";
        public const string Customer_AddressId = "address_id";
        public const string Customer_Name = "name";
        #endregion

        #region UserEntity
        public const string User_Table = "tb_user";
        public const string User_FirstName = "firs_name";
        public const string User_LastName = "last_name";
        public const string User_Username = "user_name";
        public const string User_Role = "role";
        public const string Pass_Hash = "hash_pass";
        #endregion
    }
}
