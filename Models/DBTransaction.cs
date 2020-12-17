using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Oracle.DatabaseService
{
    public class DBTransaction: IDisposable
    {
        public String Name { get; set; }
        public OracleTransaction Transaction { get; set; }
        public OracleConnection Connection  { get; set; }

        public DBTransaction(String Name)
        {
            this.Name = Name;
            this.Connection = new OracleConnection(ConnectionHandler.buildConnectionString());
            this.Connection.Open();
            this.Transaction = this.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void Dispose()
        {
            this.Connection.Close();
        }
        public void Commit()
        {
            this.Transaction.Commit();
        }
        public void Rollback()
        {
            this.Transaction.Rollback();
        }
    }
}
