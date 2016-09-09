using DatabaseTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsageApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IDbTemplate dbTmpl = new DbTemplate(@"Data Source=.\SQLEXPRESS; Database=dskclaims-mgr; Integrated Security=True;");

            List<long> ids = dbTmpl.Select("select top 5 * from Complaints", reader =>
                                {
                                    return reader.GetInt64(0);
                                });
            ids.ForEach(Console.WriteLine);
            Console.ReadKey();

        }
    }
}
