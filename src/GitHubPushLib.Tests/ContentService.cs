using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Should.Fluent;
using Xunit;

namespace GitHubPushLib.Tests
{
    public class ContentService
    {
        private string _authToken = "123456asDfGhrtY456";
        private Mock<ContentRepo> _mockRepo;
        private GitHubPushLib.ContentService _subject;

        public ContentService()
        {
            this._mockRepo = new Mock<ContentRepo>();            
            this._subject = new GitHubPushLib.ContentService(
                    this._authToken, 
                    this._mockRepo.Object
                );
        }

        public class Constructor
        {
            [Fact]
            public void RequiresAuthToken()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new GitHubPushLib.ContentService("");
                });
            }
        }

        public class PushFile : ContentService
        {
            public class WhenTargetDoesntExist : ContentService
            {
                public WhenTargetDoesntExist()
                {
                    this._mockRepo.Setup(m => m.GetFile(this._authToken, It.IsAny<FileTarget>()))
                        .Returns((File)null);
                }

                [Fact]
                public void CallsCreateFileOnRepo()
                {
                    this._mockRepo
                        .Setup(m => m.CreateFile(
                            this._authToken, 
                            It.IsAny<File>(), 
                            It.IsAny<FileTarget>(),
                            It.IsAny<string>()
                        ))
                        .Verifiable();

                    var file = new DiskFile("Resources/content_file.gif");
                    var target = new FileTarget("pseudomuto", "reponame", "content_file.gif");

                    this._subject.PushFile(file, target, "creating file");

                    this._mockRepo.Verify();
                }
            }

            public class WhenTargetExists : ContentService
            {
                public WhenTargetExists()
                {
                    this._mockRepo.Setup(m => m.GetFile(this._authToken, It.IsAny<FileTarget>()))
                        .Returns(new DiskFile("Resources/content_file.gif"));
                }

                [Fact]
                public void CallsUpdateFileOnRepo()
                {
                    this._mockRepo
                        .Setup(m => m.UpdateFile(
                            this._authToken,
                            It.IsAny<File>(),
                            It.IsAny<FileTarget>(),
                            It.IsAny<string>()
                        ))
                        .Verifiable();

                    var file = new DiskFile("Resources/content_file.gif");
                    var target = new FileTarget("pseudomuto", "reponame", "content_file.gif");

                    this._subject.PushFile(file, target, "updating file");

                    this._mockRepo.Verify();
                }
            }

            [Fact]
            public void RequiresFile()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    this._subject.PushFile(null, new FileTarget("owner", "repo", "path"), "msg");
                });
            }

            [Fact]
            public void RequiresTarget()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var file = new DiskFile("Resources/content_file.gif");
                    this._subject.PushFile(file, null, "msg");
                });
            }

            [Fact]
            public void RequiresMessage()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var file = new DiskFile("Resources/content_file.gif");
                    this._subject.PushFile(
                            file, 
                            new FileTarget("owner", "repo", "path"), 
                            ""
                        );
                });
            }
        }
    }
}
