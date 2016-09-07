using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;
using System.Threading;

namespace PDFConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PDFManager pdf_manager;
        BackgroundWorker backgroundWorker;

        public MainWindow()
        {
            InitializeComponent();

            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += backgroundWorker_DoWork;
            this.backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            pdf_manager.StartProcess();

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbProgress.Value = e.ProgressPercentage;
        }

        private void btnGetFileInfo_Click(object sender, RoutedEventArgs e)
        {
            pdf_manager = new PDFManager(tbPDFDir.Text, tbTXTDir.Text, backgroundWorker);
            dgPDFState.ItemsSource = pdf_manager.pdfs;

            btnStartProcess.IsEnabled = true;
        }

        private void btnStartProcess_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }
    }
}
