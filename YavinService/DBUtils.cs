using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YavinService
{
    public class DBUtils
    {

        private SqlConnection conn;

        private static DBUtils instance;

        private DBUtils()
        {
            conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Matt\\Development\\projects\\yavin\\YavinService\\App_Data\\YavinDB.mdf;Integrated Security=True");
        }

        public static DBUtils Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBUtils();
                }
                return instance;
            }
        }

        public List<Dictionary<string, object>> Select(string tableName, List<string> attrs, Dictionary<string, object> parameters)
        {

            string sqlText = "SELECT {attrs} FROM [{table}]";
            sqlText = UseTableName(sqlText, tableName);
            sqlText = UseAttrs(sqlText, attrs);

            if (parameters != null)
            {
                sqlText = AddWhereEqualsClause(sqlText, parameters.Keys.ToList());
            }
            
            SqlCommand command = CreateCommandWithParameters(conn, sqlText, parameters);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (string attr in attrs)
                    {
                        result.Add(attr, reader[attr]);
                    }
                    results.Add(result);
                }
            }

            conn.Close();

            return results.Count == 0 ? null : results;
        }

        public bool Insert(string tableName, Dictionary<string, object> parameters) 
        {
            string sqlText = "INSERT INTO [{table}] ({attrs}) VALUES ({valueAttrs})";
            sqlText = UseTableName(sqlText, tableName);
            sqlText = UseAttrs(sqlText, parameters.Keys.ToList());
            sqlText = UseValueAttrs(sqlText, parameters.Keys.ToList());

            SqlCommand command = CreateCommandWithParameters(conn, sqlText, parameters);

            conn.Open();
            int result = command.ExecuteNonQuery();
            conn.Close();

            return (result == 1);
        }

        public bool Delete(string tableName, Dictionary<string, object> parameters)
        {
            string sqlText = "DELETE FROM [{table}]";
            sqlText = UseTableName(sqlText, tableName);
            if (parameters != null)
            {
                sqlText = AddWhereEqualsClause(sqlText, parameters.Keys.ToList());
            }

            SqlCommand command = CreateCommandWithParameters(conn, sqlText, parameters);

            conn.Open();
            int result = command.ExecuteNonQuery();
            conn.Close();

            return (result == 1);
        }

        public bool Edit(string tableName, Dictionary<string, object> updateParameters, Dictionary<string, object> parameters)
        {
            string sqlText = "UPDATE [{table}] SET {updateAttrs}";
            sqlText = UseTableName(sqlText, tableName);
            sqlText = UseUpdateAttrs(sqlText, updateParameters.Keys.ToList());
            if (parameters != null)
            {
                sqlText = AddWhereEqualsClause(sqlText, parameters.Keys.ToList());
            }

            SqlCommand command = CreateCommandWithParameters(conn, sqlText, parameters);
            command = AddParametersToCommand(command, updateParameters);

            conn.Open();
            int result = command.ExecuteNonQuery();
            conn.Close();

            return (result == 1);
        }

        private string UseTableName(string sqlText, string tableName)
        {
            return sqlText.Replace("{table}", tableName);
        }

        private string UseAttrs(string sqlText, List<string> attrs)
        {
            return sqlText.Replace("{attrs}", string.Join(",", attrs));
        }

        private string UseValueAttrs(string sqlText, List<string> valueAttrs)
        {
            return sqlText.Replace("{valueAttrs}", string.Join(",", valueAttrs.Select(attr => "@" + attr).ToList()));
        }

        private string UseUpdateAttrs(string sqlText, List<string> updateAttrs)
        {
            return sqlText.Replace("{updateAttrs}", string.Join(",", updateAttrs.Select(attr => attr + " = @" + attr).ToList()));
        }

        private string AddWhereEqualsClause(string sqlText, List<string> clauseAttrs)
        {
            sqlText += " WHERE ";

            foreach (string attr in clauseAttrs)
            {
                sqlText += attr + "=@" + attr + " AND ";
            }

            return sqlText.Remove(sqlText.Length - 5); // Remove the final AND with spaces
        }

        private SqlCommand CreateCommandWithParameters(SqlConnection connection, string sqlText, Dictionary<string, object> parameters)
        {
            SqlCommand command = new SqlCommand(sqlText, connection);

            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    command.Parameters.AddWithValue("@" + key, parameters[key]);
                }
            }

            return command;
        }

        private SqlCommand AddParametersToCommand(SqlCommand command, Dictionary<string, object> parameters)
        {
            foreach (string key in parameters.Keys)
            {
                command.Parameters.AddWithValue("@" + key, parameters[key]);
            }

            return command;
        }
        
    }
}