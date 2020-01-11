using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Util
{
    public class ConnectionHelper
    {
        public static SqlConnection Create(string name)
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString);

        }
    }
}
