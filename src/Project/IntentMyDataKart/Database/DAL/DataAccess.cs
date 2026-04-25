using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IntentMyDataKart.Database.DAL
{
    public static class DataAccess
    {
        public static IDataAccess GetDataAccess()
        {
            return new SqlDataAccess();
        }
    }
}
