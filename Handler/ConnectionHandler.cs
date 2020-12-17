using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace Oracle.DatabaseService
{
    public class ConnectionHandler
    {
        public ConnectionHandler()
        {

        }
        public static string buildConnectionString()
        {
            return  "User Id="      + Config.username           + 
                    ";Password="    + Config.password           +
                    ";Data Source="  + Config.instance           +
                    ":"             + Config.portNumber         + 
                    "/"             + Config.serviceName        +
                    ";";
        }
        public static Boolean connect()
        {
            OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString());
            try
            {
                connection.Open();
                
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return false;
            }
            return true;
        }
    }
}
