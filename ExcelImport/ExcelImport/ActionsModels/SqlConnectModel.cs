using System.Data.SqlClient;
using System.Windows.Input;
using ExcelImport.Common;

namespace ExcelImport.ActionsModels
{
    public class SqlConnectModel : ModelBase
    {
        private readonly DataExportRepository _dataExportRepository;
        public SqlConnectModel(DataExportRepository pDataExportRepository)
        {
            SqlConnectUsrCtrl = null;
            _dataExportRepository = pDataExportRepository;
        }

        public SqlConnectUsrCtrl SqlConnectUsrCtrl { get; set; }

        public object GetRepresentationUi()
        {
            SqlConnectUsrCtrl = new SqlConnectUsrCtrl();
            SqlConnectUsrCtrl.UpdateUI(this);

            return SqlConnectUsrCtrl;
        }

        public string UserName
        {
            get { return _mUserNameStr; }
            set
            {
                _mUserNameStr = value;
                Notify("UserName");
            }
        }
        private string _mUserNameStr = string.Empty;

        public string Password
        {
            get { return _mPasswordStr; }
            set
            {
                _mPasswordStr = value;
                Notify("Password");
            }
        }
        private string _mPasswordStr = string.Empty;

        public string DataSource
        {
            get { return _mDataSourceStr; }
            set
            {
                _mDataSourceStr = value;
                Notify("DataSource");
            }
        }
        private string _mDataSourceStr = string.Empty;

        public string Catalog
        {
            get { return _mCatalogStr; }
            set
            {
                _mCatalogStr = value;
                Notify("Catalog");
            }
        }
        private string _mCatalogStr = string.Empty;


        public ICommand ConnectToDbCmd
        {
            get { return new RelayCommand(param => ConnectToDb(), param => CanConnectToDbCommandExecute()); }
        }

        public ICommand ExportDataToDbCmd
        {
            get { return new RelayCommand(param => ExportDataToDb(), param => CanExportDataExecute()); }
        }

        private bool CanExportDataExecute()
        {
            if (_dataExportRepository.ValidDataCollection != null)
            {
                return SqlConnected && _dataExportRepository.ValidDataCollection.Rows.Count >= 1;
            }
            return false;
        }

        private void ExportDataToDb()
        {
            _dataExportRepository.ExportDataToDb(_sqlConnection, SqlConnected);
        }

        private bool CanConnectToDbCommandExecute()
        {
            if (!string.IsNullOrEmpty(UserName) &&
                !string.IsNullOrEmpty(DataSource) &&
                !string.IsNullOrEmpty(Catalog))
            {
                return true;
            }
            return false;
        }

        private SqlConnection _sqlConnection;
        public bool SqlConnected
        {
            get { return _mSqlConnected; }
            set
            {
                _mSqlConnected = value;
                Notify("SqlConnected");
            }
        }
        private bool _mSqlConnected;

        private void ConnectToDb()
        {
            _sqlConnection = new SqlConnection
            {
                ConnectionString = "Data Source=" + DataSource + ";" +
                                   "Initial Catalog=" + Catalog + ";" +
                                   "Integrated Security=SSPI;" +
                                   "User id=" + UserName + ";" +
                                   "Password=" + Password + ";"
            };
            try
            {
                _sqlConnection.Open();
                SqlConnected = true;
            }
            catch (SqlException)
            {
                _sqlConnection.Close();
                SqlConnected = false;
            }



        }
    }
}
