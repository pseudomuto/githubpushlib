using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubPushLib
{
    public class FileResource : BaseFileResource
    {
        public FileResource(string filePath)
            : base(filePath)
        {
        }
        
        protected override byte[] GetFileContents()
        {
            return File.ReadAllBytes(this.Path);
        }
    }
}
