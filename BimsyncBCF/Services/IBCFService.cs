using BimsyncBCF.Models.BCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BimsyncBCF.Services
{
    public interface IBCFService
    {
        Task<List<IssueBoard>> GetIssueBoardsAsync(string bimsync_project_id);
        Task<List<Topic>> GetTopicsAsync(string project_id);
        Task<List<Comment>> GetComments(string project_id, string topic_guid);
        Task<List<Viewpoint>> GetViewpoints(string project_id, string topic_guid);
        Task<Viewpoint> GetViewpoint(string project_id, string topic_guid, string viewpoint_guid);
        Task<ImageSource> GetSnapshot(string project_id, string topic_guid, string viewpoint_guid);

    }
}
