using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Oracle.DatabaseService
{
    public class DBStoredProcedureParameter
    {
        public String Name { get; set; }
        public Object Value { get; set; }
        public Boolean IsCollection { get; set; }
        public OracleDbType DbType { get; set; }
        public ParameterDirection ParameterType { get; set; }

        public DBStoredProcedureParameter()
        {
            this.IsCollection = false;
            this.ParameterType = ParameterDirection.Input;
        }
    }
}
