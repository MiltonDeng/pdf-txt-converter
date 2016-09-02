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
        private bool state;
        private int page_number;

        public bool State
        {
            get 
            {
                return this.state;
            }
            
            set
            {
                this.state = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("State"));
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
            this.State = false;
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

        public PDFManager(string pdf_dir, string txt_dir)
        {
            this.pdf_dir = pdf_dir;
            this.txt_dir = txt_dir;
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
            foreach (PDF pdf in pdfs)
            {
                string dest_path = txt_dir + "\\" + Path.GetFileNameWithoutExtension(pdf.FilePath) + ".txt";
                pdf.CopyOut(dest_path);
            }
        }

    }
}
