using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace GitHubPushLib.Tests.Models
{
    public class EmbeddedFile
    {
        private File _subject 
            = new GitHubPushLib.EmbeddedFile("Resources.embedded.content_file.gif");

        public class Constructor : EmbeddedFile
        {
            [Fact]
            public void RequiresFileName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new GitHubPushLib.DiskFile(string.Empty);
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
                this._subject.Path
                    .Should().Equal("GitHubPushLib.Tests.Resources.embedded.content_file.gif");
            }
        }

        public class Content : EmbeddedFile
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
