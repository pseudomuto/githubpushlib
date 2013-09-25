using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubPushLib
{
    public class DiskFile : BaseFile
    {
        public DiskFile(string filePath)
            : base(filePath)
        {
        }
        
        protected override byte[] GetFileContents()
        {
            return System.IO.File.ReadAllBytes(this.Path);
        }
    }
}
