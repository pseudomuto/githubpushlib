using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHubPushLib
{
    public interface ContentRepo
    {
        File GetFile(string authToken, FileTarget target);

        File CreateFile(string authToken, File file, FileTarget target, string message);

        File UpdateFile(string authToken, File file, FileTarget target, string message);
    }
}
