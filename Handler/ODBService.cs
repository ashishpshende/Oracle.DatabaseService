using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Oracle.DatabaseService
{
    public class ODBService
    {
        private static ODBService localInstance;
        public static ODBService SharedInstance()
        {
            if (localInstance == null)
            {
                localInstance = new ODBService();
            }
            return localInstance;
        }
        private ODBService()
        {

        }
        public DataSet Execute(DBStoredProcedure procedure)
        {
            DataSet result = null;
            using (OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString()))
            {
                try
                {
                    connection.Open();

                    OracleCommand objCmd = new OracleCommand
                    {
                        CommandText = procedure.Name,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    foreach (var item in procedure.Parameters)
                    {
                        OracleParameter prm = new OracleParameter
                        {
                            OracleDbType = item.DbType
                        };
                        if (item.ParameterType == ParameterDirection.Output)
                        {
                            prm.Direction = ParameterDirection.Output;

                        }
                        else
                        {
                            prm.Value = item.Value;
                        }
                        
                        if(item.IsCollection)
                            prm.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                        prm.ParameterName = item.Name;                        
                        objCmd.Parameters.Add(prm);

                        
                    }
                    using (OracleDataAdapter adapter = new OracleDataAdapter())
                    {
                        adapter.SelectCommand = objCmd;
                        result = new DataSet();
                        adapter.Fill(result);
                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.Message);
                    throw (ex);
                }
            }
            return result;
        }
        public DataSet Execute(DBQuery query)
        {
            
            DataSet result = new DataSet();
            using (OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString()))
            {
                try
                {
                    connection.Open();
                    using (OracleDataAdapter adapter = new OracleDataAdapter())
                    {
                        adapter.SelectCommand = new OracleCommand(query.text, connection);

                        foreach(string key in query.parameters.Keys)
                        {
                            adapter.SelectCommand.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                        }
                        adapter.Fill(result);
                    };                                       
                    connection.Close();

                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.Message);
                    throw (ex);
                }
            }
            return result;
        }

        public Boolean ExecuteInsert(DBQuery query)
        {
            Boolean result = true;
            if (query.Transaction != null)
            {
                try
                {
                    OracleCommand Command = query.Transaction.Connection.CreateCommand();
                    Command.Transaction = query.Transaction.Transaction;
                    Command.CommandType = CommandType.Text;
                    Command.CommandText = query.text;
                    Command.BindByName = true;
                    foreach (string key in query.parameters.Keys)
                    {
                        Command.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                    }
                    int rowsAffected = Command.ExecuteNonQuery();
                    result = (rowsAffected == 0) ? false : true;
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine(ex.Message);
                    throw (ex);
                }
                finally
                {

                }

            }
            else
            {
                using (OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString()))
                {
                    try
                    {
                        connection.Open();

                        OracleCommand Command = connection.CreateCommand();
                        Command.CommandType = CommandType.Text;
                        Command.CommandText = query.text;
                        Command.BindByName = true;
                        foreach (string key in query.parameters.Keys)
                        {
                            Command.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                        }
                        int rowsAffected = Command.ExecuteNonQuery();
                        result = (rowsAffected == 0) ? false : true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        Console.WriteLine(ex.Message);
                        throw (ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return result;
        }
        public Int64 ExecuteInsertReturnId(DBQuery query)
        {
            Int64 result = 0;
            if (query.Transaction != null)
            {
                try
                {
                    OracleCommand Command = query.Transaction.Connection.CreateCommand();
                    Command.Transaction = query.Transaction.Transaction;
                    Command.CommandType = CommandType.Text;
                    Command.CommandText = query.text;
                    Command.BindByName = true;
                    foreach (string key in query.parameters.Keys)
                    {
                        if (query.parameters[key] != null)
                            Command.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                    }
                    OracleParameter outputParameter = new OracleParameter("RECORD_ID", OracleDbType.Int64);
                    outputParameter.Direction = ParameterDirection.Output;
                    Command.Parameters.Add(outputParameter);
                    int rowsAffected = Command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        result = long.Parse(outputParameter.Value.ToString()); // Convert.ToInt64(outputParameter.Value);                    

                    else
                        result = 0;
                }
                catch (Exception ex)
                {
                    result = 0;
                    Console.WriteLine(ex.Message);
                    throw (ex);
                }
                finally
                {
                   
                }
            }
            else
            {
                using (OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString()))
                {
                    try
                    {
                        connection.Open();

                        OracleCommand Command = connection.CreateCommand();
                        Command.CommandType = CommandType.Text;
                        Command.CommandText = query.text;
                        Command.BindByName = true;
                        foreach (string key in query.parameters.Keys)
                        {
                            if (query.parameters[key] != null)
                                Command.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                        }
                        OracleParameter outputParameter = new OracleParameter("RECORD_ID", OracleDbType.Int64);
                        outputParameter.Direction = ParameterDirection.Output;
                        Command.Parameters.Add(outputParameter);
                        int rowsAffected = Command.ExecuteNonQuery();
                        if (rowsAffected != 0)
                            result = long.Parse(outputParameter.Value.ToString()); // Convert.ToInt64(outputParameter.Value);                    

                        else
                            result = 0;
                    }
                    catch (Exception ex)
                    {
                        result = 0;
                        Console.WriteLine(ex.Message);
                        throw (ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
                
            return result;
        }
        public Boolean ExecuteUpdate(DBQuery query)
        {
            Boolean result = true;
            using (OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString()))
            {
                try
                {
                    connection.Open();

                    OracleCommand Command = connection.CreateCommand();
                    Command.CommandType = CommandType.Text;
                    Command.BindByName = true;
                    Command.CommandText = query.text;
                    foreach (string key in query.parameters.Keys)
                    {
                        Command.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                    }
                    int rowsAffected = Command.ExecuteNonQuery();
                    result = rowsAffected!=0 ? true:false;
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine(ex.Message);
                    throw (ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }     
        public Boolean ExecuteDelete(DBQuery query)
        {
            Boolean result = true;
            using (OracleConnection connection = new OracleConnection(ConnectionHandler.buildConnectionString()))
            {
                try
                {
                    connection.Open();

                    OracleCommand Command = connection.CreateCommand();
                    Command.CommandType = CommandType.Text;
                    Command.CommandText = query.text;
                    foreach (string key in query.parameters.Keys)
                    {
                        Command.Parameters.Add(new OracleParameter(key, query.parameters[key]));
                    }
                    int rowsAffected = Command.ExecuteNonQuery();
                    result = (rowsAffected == 0) ? false : true;
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine(ex.Message);
                   throw (ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }
    }
}
