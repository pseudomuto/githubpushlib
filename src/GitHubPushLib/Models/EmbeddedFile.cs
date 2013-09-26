using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace GitHubPushLib
{
    public class EmbeddedFile : BaseFile
    {
        private Assembly _container;

        public EmbeddedFile(string resourceName, Assembly container = null)
            : base(resourceName)
        {
            this._container = container ?? Assembly.GetCallingAssembly();

            this.Name = this.GetFileName(resourceName);
            this.Path = this.GetFullPath(resourceName);

            this._content = new Lazy<string>(() =>
            {
                return this.LoadContent();
            });  
        }

        protected override string GetFileName(string filePath)
        {
            var regex = new Regex(@"(\w+\.\w+)$", RegexOptions.IgnoreCase);
            return regex.Match(filePath).Groups[1].Value;
        }

        protected override string GetFullPath(string filePath)
        {
            var name = this._container.GetName().Name;
            return string.Concat(name, ".", filePath);
        }

        protected override byte[] GetFileContents()
        {
            using (var stream = this._container.GetManifestResourceStream(this.Path))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                return data;
            }
        }
    }
}
