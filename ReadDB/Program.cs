using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDB
{
    class Program
    {
        static void Main(string[] args)
        {
            RuningParadoxTest();
        }

        private static void RuningParadoxTest()
        {
            const string ConnectionStringFormat =
                "Driver={{Microsoft Paradox Driver (*.db )}};Uid={0};UserCommitSync=Yes;Threads=3;SafeTransactions=0;" +
                "ParadoxUserName={0};ParadoxNetStyle=4.x;ParadoxNetPath={1};PageTimeout=5;MaxScanRows=8;" +
                "MaxBufferSize=65535;DriverID=538;Fil=Paradox 7.X;DefaultDir={2};Dbq={2}";

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.Odbc");
            using (DbConnection connection = factory.CreateConnection())
            {
                string userName = "Admin";
                string paradoxNetPath = @"C:\Programas\BDE";
                string databasePath = @"C:\DB";

                connection.ConnectionString =
                    String.Format(ConnectionStringFormat, userName, paradoxNetPath, databasePath);
                connection.Open();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select * from [TABLE_NAME]";
                    DbDataAdapter _da = factory.CreateDataAdapter();
                    _da.SelectCommand = command;
                    DataSet _ds = new DataSet();

                    _da.Fill(_ds);

                    object itemCount = command.ExecuteScalar();
                    Console.WriteLine("Order items: {0}", itemCount);
                    Console.ReadKey();
                }
            }
        }

    }
}
