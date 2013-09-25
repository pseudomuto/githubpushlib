using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubPushLib
{
    public abstract class BaseFile : File
    {
        private Lazy<string> _content;

        public virtual string Name { get; private set; }

        public virtual string Path { get; private set; }

        public virtual string Content { get { return this._content.Value; } }

        protected BaseFile(string filePath)
        {
            Guard.AgainstNullOrEmpty("filePath", filePath);

            this.Name = System.IO.Path.GetFileName(filePath);
            this.Path = System.IO.Path.GetFullPath(filePath);

            this._content = new Lazy<string>(() =>
            {
                return this.LoadContent();
            });
        }

        protected virtual string LoadContent()
        {
            var raw = this.GetFileContents();
            return Convert.ToBase64String(raw);
        }

        protected abstract byte[] GetFileContents();        
    }
}
