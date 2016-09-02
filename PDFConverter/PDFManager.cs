using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using iTextSharp.text.pdf;

namespace PDFConverter
{
    class PDF
    {
        public string FilePath { get; set; }
        public bool State { get; set; }
        public int PageNumber { get; set; }

        public PDF(string file_path) 
        {
            this.FilePath = file_path;
            this.State = false;
            this.PageNumber = 0;
        }

        public void CopyOut(string dest_path)
        {
            StringBuilder content;

            using (PdfReader reader = new PdfReader(FilePath))
            {
                this.PageNumber = reader.NumberOfPages;
            }
        }

    }

    class PDFManager
    {
        private string pdf_dir;
        private string txt_dir;
        public List<PDF> pdf_files;

        public PDFManager(string pdf_dir, string txt_dir)
        {
            this.pdf_dir = pdf_dir;
            this.txt_dir = txt_dir;
            pdf_files = new List<PDF>();

            string[] pdf_paths = Directory.GetFiles(pdf_dir);

            foreach(string pdf_path in pdf_paths)
            {
                pdf_files.Add(new PDF(pdf_path));
            }
        }

        public bool CheckDirectory()
        {

            return true;
        }




    }
}
