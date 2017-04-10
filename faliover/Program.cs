using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace faliover
{
    class Program
    {
        private static string sqlConnectionCommand = System.Configuration.ConfigurationManager.ConnectionStrings["b2c"].ToString();
        private static string sqlSelectCommand = "select @@SERVERNAME";
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public int run()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionCommand))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;

                        sqlCommand.Connection = sqlConnection;

                        sqlCommand.CommandText = sqlSelectCommand;

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0]);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
                //sqlDataReader.Close();
                //sqlCommand.Dispose();
                //sqlConnection.Close();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Try Connecnt....");
                logger.Fatal(e);
                System.Threading.Thread.Sleep(1000);
                return 0;
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Program start = new Program();
                int opt = start.run();
            }
        }
    }
}
