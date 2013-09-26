using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHubPushLib
{
    public class GitHubContentRepo : ContentRepo
    {
        private static readonly string BASE_URI = "https://api.github.com";

        public File GetFile(string authToken, FileTarget target)
        {
            var uri = string.Format(
                    "repos/{0}/{1}/contents/{2}",
                    target.Owner,
                    target.Repo,
                    target.Path.TrimStart('/')
                );

            var request = new RestRequest(uri);
            PrepareRequest(authToken, request);

            var response = this.ExecuteRequest<ContentResponse>(request);

            return response.Data;
        }

        

        public File CreateFile(string authToken, File file, FileTarget target, string message)
        {
            var uri = string.Format(
                    "repos/{0}/{1}/contents/{2}",
                    target.Owner,
                    target.Repo,
                    target.Path.TrimStart('/')
                );

            var body = new CreateBody();
            body.message = message;
            body.content = file.Content;

            var request = new RestRequest(uri, Method.PUT);
            PrepareRequest(authToken, request);
            request.AddBody(body);

            var response = this.ExecuteRequest<CommitResponse>(request);
            return response.Data.Content;
        }

        public File UpdateFile(string authToken, File file, FileTarget target, string message)
        {
            var uri = string.Format(
                    "repos/{0}/{1}/contents/{2}",
                    target.Owner,
                    target.Repo,
                    target.Path.TrimStart('/')
                );

            var body = new UpdateBody();
            body.message = message;
            body.content = file.Content;
            body.sha = file.SHA;

            var request = new RestRequest(uri, Method.PUT);
            PrepareRequest(authToken, request);
            request.AddBody(body);

            var response = this.ExecuteRequest<CommitResponse>(request);
            return response.Data.Content;
        }

        protected virtual IRestResponse<TResponse> ExecuteRequest<TResponse>(RestRequest request)
            where TResponse : new()
        {
            var client = new RestClient(BASE_URI);
            return client.Execute<TResponse>(request);
        }

        private static void PrepareRequest(string authToken, RestRequest request)
        {
            request.AddHeader("Authorization", "token " + authToken);
            request.RequestFormat = DataFormat.Json;
        }

        protected class ContentResponse : File
        {
            public string Name { get; set; }

            public string Path { get; set; }

            public string Content { get; set; }

            public string SHA { get; set; }
        }

        protected class CommitResponse
        {
            public ContentResponse Content { get; set; }
        }

        protected class CreateBody
        {
            public string message { get; set; }

            public string content { get; set; }
        }

        protected class UpdateBody : CreateBody
        {
            public string sha { get; set; }
        }
    }
}
