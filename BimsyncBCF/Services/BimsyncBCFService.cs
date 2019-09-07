using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BimsyncBCF.Models.BCF;

namespace BimsyncBCF.Services
{
    class BimsyncBCFService : IBCFService
    {
        private HttpClient _client;

        public BimsyncBCFService(HttpClientHandler httpClientHandler)
        {
            _client = new HttpClient(httpClientHandler, true);
            _client.BaseAddress = new Uri("https://bcf.bimsync.com/bcf/beta/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "jQg3g5knpDCjair");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<List<IssueBoard>> GetIssueBoardsAsync(string bimsync_project_id)
        {
            List<IssueBoard> issueBoards = new List<IssueBoard>();
            // string path = String.Format("projects", bimsync_project_id);
            string path = String.Format("projects?bimsync_project_id={0}", bimsync_project_id);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                issueBoards = await response.Content.ReadAsAsync<List<IssueBoard>>();
            }
            return issueBoards;
        }

        public async Task<List<Topic>> GetTopicsAsync(string project_id)
        {
            List<Topic> topics = new List<Topic>();
            string path = String.Format("projects/{0}/topics", project_id);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                topics = await response.Content.ReadAsAsync<List<Topic>>();
            }
            return topics;
        }

        public async Task<List<Comment>> GetComments(string project_id, string topic_guid)
        {
            List<Comment> comments = new List<Comment>();
            string path = String.Format("projects/{0}/topics/{1}/comments", project_id, topic_guid);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                comments = await response.Content.ReadAsAsync<List<Comment>>();
            }

            return comments;
        }

        public async Task<List<Viewpoint>> GetViewpoints(string project_id, string topic_guid)
        {
            List<Viewpoint> viewpoints = new List<Viewpoint>();
            string path = String.Format("projects/{0}/topics/{1}/viewpoints", project_id, topic_guid);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                viewpoints = await response.Content.ReadAsAsync<List<Viewpoint>>();
            }

            return viewpoints;
        }

        public async Task<Viewpoint> GetViewpoint(string project_id, string topic_guid, string viewpoint_guid)
        {
            Viewpoint viewpoint = null;
            string path = String.Format("projects/{0}/topics/{1}/viewpoints/{2}", project_id, topic_guid, viewpoint_guid);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                viewpoint = await response.Content.ReadAsAsync<Viewpoint>();
            }

            return viewpoint;
        }

        public async Task<ImageSource> GetSnapshot(string project_id, string topic_guid, string viewpoint_guid)
        {
            BitmapImage image = new BitmapImage();
            string path = String.Format("projects/{0}/topics/{1}/viewpoints/{2}/snapshot", project_id, topic_guid, viewpoint_guid);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                using (Stream inputStream = await response.Content.ReadAsStreamAsync())
                {
                    image.BeginInit();
                    image.StreamSource = inputStream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                }
            }

            return image;
        }
    }
}
