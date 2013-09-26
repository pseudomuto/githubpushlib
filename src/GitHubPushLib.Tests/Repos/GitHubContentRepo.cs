using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace GitHubPushLib.Tests.Repos
{
    public class GitHubContentRepo
    {
        private MockGitHubContentRepo _subject = new MockGitHubContentRepo();

        public class GetFile : FileTest
        {
            public GetFile()
                : base("some_token", "repos/pseudomuto/test/contents/readme.md", Method.GET)
            {
                this._subject.GetFile("some_token", new FileTarget(
                        "pseudomuto",
                        "test",
                        "readme.md"
                    ));
            }            
        }

        public class CreateFile : FileTest
        {
            public CreateFile()
                : base("some_token", "repos/pseudomuto/test/contents/readme.gif", Method.PUT)
            {
                var file = new DiskFile("Resources/content_file.gif");

                this._subject.CreateFile("some_token", file, new FileTarget(
                        "pseudomuto",
                        "test",
                        "readme.gif"
                    ), "some message");
            }
        }

        public class UpdateFile : FileTest
        {
            public UpdateFile()
                : base("some_token", "repos/pseudomuto/test/contents/readme.gif", Method.PUT)
            {
                var file = new DiskFile("Resources/content_file.gif");

                this._subject.UpdateFile("some_token", file, new FileTarget(
                        "pseudomuto",
                        "test",
                        "readme.gif"
                    ), "some message");
            }
        }

        public abstract class FileTest : GitHubContentRepo
        {
            private string _authToken;
            private string _endpoint;
            private Method _requestMethod;

            protected FileTest(string authToken, string endpoint, Method requestMethod)
            {
                this._authToken = authToken;
                this._endpoint = endpoint;
                this._requestMethod = requestMethod;
            }

            [Fact]
            public void AddsAuthHeaderToRequest()
            {
                this._subject.AuthToken
                    .Should().Equal(this._authToken);
            }

            [Fact]
            public void IssuesRequestUsingCorrectMethod()
            {
                this._subject.RequestMethod
                    .Should().Equal(this._requestMethod);
            }

            [Fact]
            public void RequestsCorrectEndpoint()
            {
                this._subject.RequestUri
                    .Should().Equal(this._endpoint);
            }
        }

        class MockGitHubContentRepo : GitHubPushLib.GitHubContentRepo
        {
            public Method RequestMethod { get; private set; }

            public string RequestUri { get; private set; }

            public string AuthToken { get; private set; }

            protected override IRestResponse<TResponse> ExecuteRequest<TResponse>(
                RestRequest request)
            {
                this.RequestMethod = request.Method;
                this.RequestUri = request.Resource;

                var headers = request.Parameters.Where(p => 
                        p.Type.Equals(ParameterType.HttpHeader)
                    );

                var token = headers.Single(h => h.Name.Equals("Authorization")).Value.ToString();
                this.AuthToken = token.Substring(token.IndexOf(' ') + 1);

                var response = new RestResponse<TResponse>();
                response.Data = new TResponse();
                return response;
            }
        }
    }
}
