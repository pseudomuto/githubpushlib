using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHubPushLib
{
    public class FileTarget
    {
        public string Owner { get; private set; }

        public string Repo { get; private set; }

        public string Path { get; private set; }

        public string Branch { get; private set; }

        public FileTarget(string owner, string repo, string path, string branch = "master")
        {
            Guard.AgainstNullOrEmpty("owner", owner);
            Guard.AgainstNullOrEmpty("repo", repo);
            Guard.AgainstNullOrEmpty("path", path);

            this.Owner = owner;
            this.Repo = repo;
            this.Path = path;
            this.Branch = branch;
        }
    }
}
