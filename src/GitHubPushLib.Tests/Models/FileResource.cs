using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Should.Fluent;
using System.IO;

namespace GitHubPushLib.Tests.Models
{
    public class FileResource
    {
        private IFile _subject = new GitHubPushLib.FileResource("Resources/content_file.gif");

        public class Constructor : FileResource
        {
            [Fact]
            public void RequiresFileName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new GitHubPushLib.FileResource(string.Empty);
                });
            }

            [Fact]
            public void SetsName()
            {
                this._subject.Name
                    .Should().Equal("content_file.gif");
            }

            [Fact]
            public void SetsPathToAbsolutePath()
            {
                Path.IsPathRooted(this._subject.Path).Should().Be.True();
            }
        }

        public class Content : FileResource
        {
            [Fact]
            public void GetsFileContentsAsBase64String()
            {
                this._subject.Content
                    .Should().Not.Be.NullOrEmpty();
            }
        }
    }
}
