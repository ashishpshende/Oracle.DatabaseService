using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Oracle.DatabaseService
{
    public static class DatabaseHelper
    {
        public static SqlDbType ToSqlDbType(this DbType type)
        {
            switch (type)
            {

                case System.Data.DbType.Int64:
                    return SqlDbType.BigInt;

                case System.Data.DbType.Binary:
                    return SqlDbType.Binary;

                case System.Data.DbType.Boolean:
                    return SqlDbType.Bit;

                case System.Data.DbType.AnsiStringFixedLength:
                case System.Data.DbType.StringFixedLength:
                    return SqlDbType.Char;

                case System.Data.DbType.Date:
                    return SqlDbType.Date;

                case System.Data.DbType.DateTime:
                    return SqlDbType.DateTime;

                case System.Data.DbType.DateTime2:
                    return SqlDbType.DateTime2;

                case System.Data.DbType.DateTimeOffset:
                    return SqlDbType.DateTimeOffset;

                case System.Data.DbType.Decimal:
                    return SqlDbType.Decimal;

                case System.Data.DbType.Int32:
                    return SqlDbType.Int;


                case System.Data.DbType.AnsiString:
                case System.Data.DbType.String:
                    return SqlDbType.VarChar;

                case System.Data.DbType.Single:
                    return SqlDbType.Real;

                case System.Data.DbType.Object:
                    return SqlDbType.Variant;

                case System.Data.DbType.Time:
                    return SqlDbType.Time;

                default:
                    return SqlDbType.BigInt;
            }
        }
        public static DbType ToDbType(this SqlDbType type)
        {
            switch (type)
            {
                case SqlDbType.BigInt:
                    return DbType.Int64;

                case SqlDbType.VarBinary:
                    return DbType.Binary;

                case SqlDbType.Bit:
                    return DbType.Boolean;

                case SqlDbType.Char:
                    return DbType.String;

                case SqlDbType.Date:
                    return DbType.Date;

                case SqlDbType.DateTime:
                    return DbType.DateTime;

                case SqlDbType.DateTimeOffset:
                    return DbType.DateTimeOffset;

                case SqlDbType.Decimal:
                    return DbType.Decimal;

                case SqlDbType.Float:
                    return DbType.Double;

                case SqlDbType.Int:
                    return DbType.Int32;

                case SqlDbType.Money:
                    return DbType.Decimal;

                case SqlDbType.NChar:
                    return DbType.StringFixedLength;

                case SqlDbType.NText:
                    return DbType.String;

                case SqlDbType.NVarChar:
                    return DbType.String;

                case SqlDbType.Real:
                    return DbType.Single;

                case SqlDbType.Timestamp:
                    return DbType.Binary;

                case SqlDbType.SmallInt:
                    return DbType.Int16;

                case SqlDbType.SmallMoney:
                    return DbType.Decimal;

                case SqlDbType.Variant:
                    return DbType.Object;

                case SqlDbType.Text:
                    return DbType.String;

                case SqlDbType.Time:
                    return DbType.Time;

                case SqlDbType.TinyInt:
                    return DbType.Byte;

                case SqlDbType.UniqueIdentifier:
                    return DbType.Guid;

                case SqlDbType.VarChar:
                    return DbType.AnsiString;

                case SqlDbType.Xml:
                    return DbType.Xml;

                default:
                    return DbType.Int32;
            }
        }
        public static Boolean IsValidField(SqlDataReader reader, string ColumnName)
        {

            if (reader.GetSchemaTable().Rows.OfType<DataRow>().Any(row => row["ColumnName"].ToString() == ColumnName))
            {
                if (reader.IsDBNull(reader.GetOrdinal(ColumnName)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidField(DataRow row, string v)
        {
            if (row.Table.Columns.Contains(v))
            {
                return !row.IsNull(v);
            }
            else
            {
                return false;
            }
        }

        public static bool ContainsKey(this DataRow row, string v)
        {
            if (row.Table.Columns.Contains(v))
            {
                return !row.IsNull(v);
            }
            else
            {
                return false;
            }
        }
        //Data Row

        //Get Decimal From Data Row
        public static Decimal? GetDecimalForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToDecimal(row[key]);
            }
            return null;
        }
        //Get Int64 From Data Row
        public static Int64? GetInt64ForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToInt64(row[key]);
            }
            return null;
        }
        //Get Int32 From Data Row
        public static Int32? GetInt32ForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToInt32(row[key]);
            }
            return null;
        }
        //Get Int16 From Data Row
        public static Int16? GetInt16ForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToInt16(row[key]);
            }
            return null;
        }
        //Get String From Data Row
        public static String GetStringForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToString(row[key]);
            }
            return null;
        }
        //Get Boolean From Data Row
        public static Boolean? GetBooleanForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToBoolean(row[key]);
            }
            return null;

        }
        //Get Float From Data Row
        public static Double? GetDoubleForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToDouble(row[key]);
            }
            return null;
        }
        //Get Float From Data Row
        public static Char? GetCharForKey(this DataRow row, String key)
        {

            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                return Convert.ToChar(row[key]);
            }
            return null;
        }
        //Get Date Time From Data Row
        public static DateTime? GetDateTimeForKey(this DataRow row, String key)
        {
            if (row.Table.Columns.Contains(key) && row[key] != DBNull.Value)
            {
                try
                {
                    return Convert.ToDateTime(row[key]);
                }
                catch
                {
                    return null;
                }

            }
            return null;
        }



        //Data Reader
        //Get Decimal From Data Row
        public static Decimal? GetDecimalForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetDecimal(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Int64 From Data Row
        public static Int64? GetInt64ForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetInt64(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Int32 From Data Row
        public static Int64? GetInt32ForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetInt32(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Int16 From SqlDataReader 
        public static Int16? GetInt16ForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetInt16(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get String From SqlDataReader 
        public static String GetStringForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetString(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Boolean From SqlDataReader 
        public static Boolean? GetBooleanForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetBoolean(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Float From SqlDataReader 
        public static Double? GetDoubleForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetDouble(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Float From SqlDataReader 
        public static Char? GetCharForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetChar(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Date Time From SqlDataReader 
        public static DateTime? GetDateTimeForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetDateTime(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
        //Get Date Time From SqlDataReader 
        public static Byte? GetByteForKey(this SqlDataReader reader, String key)
        {
            try
            {
                return reader.GetByte(reader.GetOrdinal(key));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
    }
}
