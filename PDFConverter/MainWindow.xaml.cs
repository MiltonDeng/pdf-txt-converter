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

namespace PDFConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PDFManager pdf_manager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetFileInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test");
            pdf_manager = new PDFManager(tbPDFDir.Text, tbTXTDir.Text);

            dgPDFState.ItemsSource = pdf_manager.pdf_files;
        }

        private void btnStartProcess_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }
    }
}
