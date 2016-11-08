using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ExcelImport.Common;

namespace ExcelImport.ActionsModels
{
    public class FileBrowseModel : ModelBase
    {
        private readonly DataValidationRepository _dataValidationRepository;
        private readonly DataExportRepository _dataExportRepository;
        
        public FileBrowseModel(DataValidationRepository pDataValidationRepository,  DataExportRepository pDataExportRepository)
        {
            _dataValidationRepository = pDataValidationRepository;
            _dataExportRepository = pDataExportRepository;

            LstInValidData = new ObservableCollection<InValidData>();
            FileBrowseUsrCtrl = null;
        }

   
        public FileBrowseUsrCtrl FileBrowseUsrCtrl { get; set; }

        public object GetRepresentationUi()
        {
            FileBrowseUsrCtrl = new FileBrowseUsrCtrl();
            FileBrowseUsrCtrl.UpdateUI(this);

            return FileBrowseUsrCtrl;
        }

        public ICommand BrowseFileCmd
        {
            get { return new RelayCommand(param => BrowseFile(), param => CanGenerateSlideCommandExecute()); }
        }

        public ICommand StartUploadCmd
        {
            get { return new RelayCommand(param => StartValidatingFile(), param => CanValidationFile()); }
        }

        private bool CanValidationFile()
        {
            if (String.IsNullOrEmpty(FilePath))
            {
                return false;
            }
            return true;
        }

        public string FilePath
        {
            get { return _mFilePathStr; }
            set
            {
                _mFilePathStr = value;
                Notify("FilePath");
            }
        }

        private string _mFilePathStr = string.Empty;

        private bool CanGenerateSlideCommandExecute()
        {
            return true;
        }

        private void BrowseFile()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog {DefaultExt = ".csv", Filter = "Csv Files (*.csv)|*.csv"};

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                FilePath = dlg.FileName;
            }
        }

        public ObservableCollection<InValidData> LstInValidData { get; set; }

        public int InValidDataCount
        {
            get { return _mInValidDataCount; }
            set
            {
                _mInValidDataCount = value;
                Notify("InValidDataCount");
            }
        }
        private int _mInValidDataCount;

        public int ValidDataCount
        {
            get { return _mValidDataCount; }
            set
            {
                _mValidDataCount = value;
                Notify("ValidDataCount");
            }
        }
        private int _mValidDataCount;

        public int TotalDataCount
        {
            get { return _mTotalDataCount; }
            set
            {
                _mTotalDataCount = value;
                Notify("TotalDataCount");
            }
        }
        private int _mTotalDataCount;

        public int ProgressValue
        {
            get { return _mProgressValue; }
            set
            {
                _mProgressValue = value;
                Notify("ProgressValue");
            }
        }
        private int _mProgressValue;

        private BackgroundWorker _backgroundWorker1;

        private async void StartValidatingFile()
        {
            _backgroundWorker1 = new BackgroundWorker();
            ProgressValue = 20;
            _backgroundWorker1.DoWork += BackgroundWorker1DoWork;
            _backgroundWorker1.WorkerReportsProgress = true;
            _backgroundWorker1.ProgressChanged += BackgroundWorker1ProgressChanged;
            _backgroundWorker1.RunWorkerCompleted += BackgroundWorker1RunWorkerCompleted;
            _backgroundWorker1.RunWorkerAsync();
         
        }


        private void BackgroundWorker1ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressValue = e.ProgressPercentage));
        }

        private void BackgroundWorker1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressValue = ProgressValue + 20;
        }


        private void BackgroundWorker1DoWork(object sender, DoWorkEventArgs e)
        {
            Task.Run(() =>
            {
                string[] csvData = File.ReadAllLines(FilePath);

                ValidatedData data = _dataValidationRepository.StartValidatingData(csvData);

                ProgressValue = ProgressValue + 80;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    LstInValidData.Clear();


                    foreach (var obj in data.InValidData)
                    {
                        LstInValidData.Add(obj);
                    }

                    InValidDataCount = LstInValidData.Count;
                    ValidDataCount = data.ValidData.Rows.Count;
                    TotalDataCount = InValidDataCount + ValidDataCount;

                    _dataExportRepository.ValidDataCollection = data.ValidData;
                });
            });

        }
    }
}
