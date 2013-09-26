using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHubPushLib
{
    public sealed class ContentService
    {
        private ContentRepo _repo;
        private string _authToken;

        public ContentService(string authToken, ContentRepo repo = null)
        {
            Guard.AgainstNullOrEmpty("authToken", authToken);

            this._repo = repo;
            this._authToken = authToken;
        }

        public File PushFile(File file, FileTarget target, string message)
        {
            Guard.AgainstNull("file", file);
            Guard.AgainstNull("target", target);
            Guard.AgainstNullOrEmpty("message", message);

            var existingFile = this._repo.GetFile(this._authToken, target);

            if (existingFile != null)
            {
                // set the hash to the existing one...
                file.Hash = existingFile.Hash;
            }

            return existingFile == null ?
                this._repo.CreateFile(this._authToken, file, target, message) :
                this._repo.UpdateFile(this._authToken, file, target, message);
        }
    }
}
