using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubPushLib
{
    public abstract class BaseFile : File
    {
        protected Lazy<string> _content;
        
        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual string Content { get { return this._content.Value; } }

        public virtual string SHA { get; set; }

        protected BaseFile(string filePath)
        {
            Guard.AgainstNullOrEmpty("filePath", filePath);
        }

        protected virtual string GetFileName(string filePath)
        {
            return System.IO.Path.GetFileName(filePath);
        }

        protected virtual string GetFullPath(string filePath)
        {
            return System.IO.Path.GetFullPath(filePath);
        }

        protected virtual string LoadContent()
        {
            var raw = this.GetFileContents();
            return Convert.ToBase64String(raw);
        }

        protected abstract byte[] GetFileContents();        
    }
}
