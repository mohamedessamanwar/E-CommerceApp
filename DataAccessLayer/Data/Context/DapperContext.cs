using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Data.Context
{
    public class DapperContext
    {

        private readonly IConfiguration configuration;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration  ;
        }
        // IDbConnection is an interface in ADO.NET, which represents
        // a connection to a database.
        // By returning IDbConnection, you allow flexibility, as
           // the method can return connections to any type of
          //  database(SQL Server, MySQL, etc.) that implements this
          //  interface, not just SQL Server.
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
