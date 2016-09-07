using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;
using iTextSharp.text.pdf;

namespace PDFConverter
{
    class PDF:INotifyPropertyChanged
    {
        public string FilePath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private string status;
        private int page_number;
        
        public string Status
        {
            get 
            {
                return this.status;
            }
            
            set
            {
                this.status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        public int PageNumber
        {
            get
            {
                return this.page_number;
            }

            set
            {
                this.page_number = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PageNumber"));
                }
            }
        }

        public PDF(string file_path) 
        {
            this.FilePath = file_path;
            this.Status = "Waiting";
            this.PageNumber = 0;
        }

        public void CopyOut(string dest_path)
        {
            StringBuilder content = new StringBuilder("");

            using (PdfReader reader = new PdfReader(FilePath))
            {
                this.PageNumber = reader.NumberOfPages;

                for (int page = 1; page <= page_number; page++)
                {
                    content.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, page));
                }
            }

            using (StreamWriter writer = new StreamWriter(dest_path))
            {
                writer.Write(content);
            }
        }

    }

    class PDFManager
    {
        private string pdf_dir;
        private string txt_dir;
        public List<PDF> pdfs;

        public BackgroundWorker worker;

        public PDFManager(string pdf_dir, string txt_dir, BackgroundWorker worker)
        {
            this.pdf_dir = pdf_dir;
            this.txt_dir = txt_dir;
            this.worker = worker;

            pdfs = new List<PDF>();

            string[] pdf_paths = Directory.GetFiles(pdf_dir);

            foreach(string pdf_path in pdf_paths)
            {
                pdfs.Add(new PDF(pdf_path));
            }
        }

        public bool CheckDirectory()
        {

            return true;
        }

        public void StartProcess()
        {
            int file_number = pdfs.Count;

            for (int i = 0; i < file_number; i++)
            {
                // PDF pdf_now = pdfs[i];
                string dest_path = txt_dir + "\\" + Path.GetFileNameWithoutExtension(pdfs[i].FilePath) + ".txt";

                try
                {
                    pdfs[i].CopyOut(dest_path);
                    pdfs[i].Status = "Sucess";
                }
                catch
                {
                    pdfs[i].Status = "Failed";
                }
                finally
                {
                    worker.ReportProgress(100 * (i + 1) / file_number);
                }
            }
        }
    }
}
