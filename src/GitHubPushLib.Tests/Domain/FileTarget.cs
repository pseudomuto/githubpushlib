using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GitHubPushLib.Tests.Domain
{
    public class FileTarget
    {
        public class Constructor
        {
            [Fact]
            public void RequiresOwner()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new GitHubPushLib.FileTarget("", "repo", "path");
                });
            }

            [Fact]
            public void RequiresRepo()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new GitHubPushLib.FileTarget("owner", "", "path");
                });
            }

            [Fact]
            public void RequiresPath()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new GitHubPushLib.FileTarget("owner", "repo", "");
                });
            }
        }
    }
}
