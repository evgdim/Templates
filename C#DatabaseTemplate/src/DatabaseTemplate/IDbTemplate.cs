using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTemplate
{
    public interface IDbTemplate
    {
        List<T> Select<T>(string select, Dictionary<string, object> parameters, Func<SqlDataReader, T> mapper);
        List<T> Select<T>(string select, Func<SqlDataReader, T> mapper);
        int Update(string statement, Dictionary<string, object> parameters);
        T UpdateScalar<T>(string statement, Dictionary<string, object> parameters);
    }
}
