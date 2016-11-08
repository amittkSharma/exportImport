using System.Windows;

namespace ExcelImport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var mainWindowModel = new MainWindowModel();
            InitializeComponent();

            this.DataContext = mainWindowModel;
        }
    }
}
