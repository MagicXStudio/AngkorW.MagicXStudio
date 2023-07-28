using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahAppsDragablzDemo.Services
{
    internal class FilesService : IFilesService
    {

        public static IFilesService? Default { get; set; }

        public FilesService()
        {
        }

        public string Content { get; set; } = DateTime.Now.ToString();
    }
}
