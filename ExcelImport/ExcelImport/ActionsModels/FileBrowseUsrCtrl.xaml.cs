using System.Windows.Controls;

namespace ExcelImport.ActionsModels
{
    /// <summary>
    /// Interaction logic for FileBrowseUsrCtrl.xaml
    /// </summary>
    public partial class FileBrowseUsrCtrl : UserControl
    {
        public FileBrowseUsrCtrl()
        {
            InitializeComponent();
        }

        public void UpdateUI(FileBrowseModel pModel)
        {
            this.DataContext = pModel;
        }
    }
}
