using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BimsyncBCF.Models.Bimsync;

namespace BimsyncBCF.Services
{
    class BimsyncService
    {
        private HttpClient _client;

        public BimsyncService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://api.bimsync.com/v2/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "6VZqTGrYD4A8kApNTJ2uB4");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<List<Project>> GetProjects()
        {
            List<Project> Projects = new List<Project>();
            // string path = String.Format("projects", bimsync_project_id);
            string path = String.Format("projects");
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Projects = await response.Content.ReadAsAsync<List<Project>>();
            }
            return Projects;
        }
    }
}
