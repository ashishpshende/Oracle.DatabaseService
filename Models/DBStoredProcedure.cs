using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Oracle.DatabaseService
{
    public class DBStoredProcedure
    {
        public string Name { get; set; }
        public List<DBStoredProcedureParameter> Parameters { get; set; }

        public DBStoredProcedure(String name)
        {
            this.Name = name;
            this.Parameters = new List<DBStoredProcedureParameter>();
        }
    }
    public class ParameterDefine
    {
        public String Name { get; set; }
        public String Value { get; set; }
        public OracleDbType DbType { get; set; }
        public ParameterDirection ParameterType { get; set; }

        public ParameterDefine() {
            this.ParameterType = ParameterDirection.Input;
        }

       
    }
}
