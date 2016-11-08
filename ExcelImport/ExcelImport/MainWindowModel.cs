using ExcelImport.ActionsModels;
using ExcelImport.Common;

namespace ExcelImport
{
    class MainWindowModel : ModelBase
    {
        public MainWindowModel()
        {
            GetRepresentationUi();
        }

        public FileBrowseModel FileBrowseModel { get; set; }
        public SqlConnectModel SqlConnectModel { get; set; }

        public object Control
        {
            get { return _mObjControl; }
            set
            {
                _mObjControl = value;
                Notify("Control");
            }
        }
        private object _mObjControl;
        
        public object SqlConnectControl
        {
            get { return _mObjSqlConnectControl; }
            set
            {
                _mObjSqlConnectControl = value;
                Notify("SqlConnectControl");
            }
        }
        private object _mObjSqlConnectControl;

        private void GetRepresentationUi()
        {
            SqlConnectModel = new SqlConnectModel(DataExportRepository.Instance);
            FileBrowseModel = new FileBrowseModel(new DataValidationRepository(), DataExportRepository.Instance);

            Control = FileBrowseModel.GetRepresentationUi();
            SqlConnectControl = SqlConnectModel.GetRepresentationUi();

        }
    }
}
