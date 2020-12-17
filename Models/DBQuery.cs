using System;
using System.Collections.Generic;

namespace Oracle.DatabaseService
{
    public class DBQuery
    {
        public String text { get; set; }

        public DBTransaction Transaction { get; set; }

        public Dictionary<string, object> parameters { get; set; }

        public DBQuery()
        {
            this.parameters = new Dictionary<string, object>();
        }
        public DBQuery(String text)
        {
            this.text = text;
            this.parameters = new Dictionary<string, object>();
        }

        public DBQuery(String text, Dictionary<string, Object> parameter)
        {
            this.text = text;
            this.parameters = parameter;
        }
        public DBQuery(String text, Dictionary<string, Object> parameter,DBTransaction transaction)
        {
            this.text = text;
            this.parameters = parameter;
            this.Transaction = transaction;
        }
    }
}
