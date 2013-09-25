using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubPushLib
{
    public interface IFile
    {
        string Name { get; }

        string Path { get; }

        string Content { get; }
    }
}
