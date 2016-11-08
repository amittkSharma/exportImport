using System.Windows.Controls;

namespace ExcelImport.ActionsModels
{
    /// <summary>
    /// Interaction logic for SqlConnectUsrCtrl.xaml
    /// </summary>
    public partial class SqlConnectUsrCtrl : UserControl
    {
        public SqlConnectUsrCtrl()
        {
            InitializeComponent();
        }

        public void UpdateUI(SqlConnectModel pModel)
        {
            this.DataContext = pModel;
        }
    }
}
