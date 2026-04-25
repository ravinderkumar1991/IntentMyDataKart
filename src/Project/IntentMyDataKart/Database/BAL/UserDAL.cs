using IntentMyDataKart.Database.DAL;
using IntentMyDataKart.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace IntentMyDataKart.Database.BAL
{
    public class UserDAL
    {

        public static List<CompanyRegistrationModel> GetCompany()
        {
            List<CompanyRegistrationModel> list = new List<CompanyRegistrationModel>();
            var parameter = new Dictionary<string, object>();
            using (IDataAccess Data = DataAccess.GetDataAccess())
            {
                IDataReader reader = Data.RetrieveData(StoredProcedures.USP_GetCompany, parameter);
                while (reader.Read())
                {
                    CompanyRegistrationModel objOutPut = new CompanyRegistrationModel();
                    if (!string.IsNullOrEmpty(Convert.ToString(reader["CompanyRegistrationId"])))
                        objOutPut.CompanyRegistrationId = Convert.ToInt64(reader["CompanyRegistrationId"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["Company"])))
                        objOutPut.Company = Convert.ToString(reader["Company"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["Country"])))
                        objOutPut.Country = Convert.ToString(reader["Country"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["State"])))
                        objOutPut.State = Convert.ToString(reader["State"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["City"])))
                        objOutPut.City = Convert.ToString(reader["City"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["Address"])))
                        objOutPut.Pincode = Convert.ToString(reader["Address"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["IsActive"])))
                        objOutPut.IsActive = Convert.ToBoolean(reader["IsActive"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["CreatedOn"])))
                        objOutPut.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["CreatedBy"])))
                        objOutPut.CreatedBy = Convert.ToString(reader["CreatedBy"]);


                    list.Add(objOutPut);
                }
            }
            return list;
        }

        public static List<SelectListItem> GetRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var parameter = new Dictionary<string, object>();
            using (IDataAccess Data = DataAccess.GetDataAccess())
            {
                IDataReader reader = Data.RetrieveData(StoredProcedures.USP_GetRole, parameter);
                while (reader.Read())
                {
                    SelectListItem objOutPut = new SelectListItem();
                    if (!string.IsNullOrEmpty(Convert.ToString(reader["RoleId"])))
                        objOutPut.Value = Convert.ToString(reader["RoleId"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(reader["Role"])))
                        objOutPut.Text = Convert.ToString(reader["Role"]);

                    list.Add(objOutPut);
                }
            }
            return list;
        }
    }
}
