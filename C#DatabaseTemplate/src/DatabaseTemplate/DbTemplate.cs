using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTemplate
{
    public class DbTemplate : IDbTemplate
    {
        private string connectionString;
        public DbTemplate(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<T> Select<T>(string select, Dictionary<string, object> parameters, Func<SqlDataReader, T> mapper)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(select, conn))
                {
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (KeyValuePair<string, object> p in parameters)
                        {
                            cmd.Parameters.AddWithValue(p.Key, p.Value);
                        }
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<T> result = new List<T>();
                        while (reader.Read())
                        {
                            result.Add(mapper.Invoke(reader));
                        }
                        return result;
                    }
                }
            }
        }
        public List<T> Select<T>(string select, Func<SqlDataReader, T> mapper)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(select, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<T> result = new List<T>();
                        while (reader.Read())
                        {
                            result.Add(mapper.Invoke(reader));
                        }
                        return result;
                    }
                }
            }
        }

        public int Update(string statement, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(statement, conn))
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    int affectedRows = cmd.ExecuteNonQuery();
                    return affectedRows;
                }
            }
        }

        public T UpdateScalar<T>(string statement, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(statement, conn))
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    T scalar = (T)cmd.ExecuteScalar();
                    return scalar;
                }
            }
        }
    }
}
