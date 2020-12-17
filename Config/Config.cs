using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracle.DatabaseService
{
    public class Config
    { 
        public static String instance = "192.168.0.67";
        public static String schema = "tpaportal";
        public static int portNumber = 1524;
        public static String username = "tpaportal";
        public static String password = "welcome";
        public static String serviceName = "utility"; 
        public static bool isConfigured = false;
        public Config(String instance_name, String schema_name, String service_name, String user_name, String pass_word ,  int port_Number = 1524 )
        {
            instance = instance_name;
            schema = schema_name;
            portNumber = port_Number;
            username = user_name;
            password = pass_word;
            serviceName = service_name;
            isConfigured = true;
        }
        public static bool configure(String instance_name, String schema_name, String service_name, String user_name, String pass_word, int port_Number = 1524)
        {
            instance = instance_name;
            schema = schema_name;
            portNumber = port_Number;
            username = user_name;
            password = pass_word;
            serviceName = service_name;
            isConfigured = true;
            return isConfigured;
        }
    }
}
