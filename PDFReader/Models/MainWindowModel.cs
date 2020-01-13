using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFReader.Models
{
    public class MainWindowModel
    {
        public List<FileModel> UploadedFiles { get; set; }
        public string OutputPath { get; set; }

    }
}
