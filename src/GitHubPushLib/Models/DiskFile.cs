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
            this.Name = this.GetFileName(filePath);
            this.Path = this.GetFullPath(filePath);

            this._content = new Lazy<string>(() =>
            {
                return this.LoadContent();
            });  
        }
        
        protected override byte[] GetFileContents()
        {
            return System.IO.File.ReadAllBytes(this.Path);
        }
    }
}
