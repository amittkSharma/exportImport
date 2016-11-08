using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ExcelImport.ActionsModels
{
    public class DataExportRepository
    {
        private static DataExportRepository _instance;

        private DataExportRepository() { }

        public static DataExportRepository Instance
        {
            get { return _instance ?? (_instance = new DataExportRepository()); }
        }

        public DataTable ValidDataCollection { get; set; }

        public void ExportDataToDb(SqlConnection connection, bool pSqlConnected)
        {
            using (var sqlBulkCopy = new SqlBulkCopy(connection))
            {
                connection.Close();
                sqlBulkCopy.DestinationTableName = "dbo.Taxes";
                if (pSqlConnected)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    sqlBulkCopy.WriteToServer(ValidDataCollection);
                }
                MessageBox.Show("Data is saved in the database");
                connection.Close();
            }
        }
    }
}
