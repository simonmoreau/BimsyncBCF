using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimsyncBCF.Models.BCF
{
    public class IssueBoard
    {
        public string project_id { get; set; }
        public string name { get; set; }
        public string bimsync_project_name { get; set; }
        public string bimsync_project_id { get; set; }
    }

    public class IssueBoardExtension
    {
        public List<string> topic_type { get; set; }
        public List<string> topic_status { get; set; }
        public List<string> user_id_type { get; set; }
    }

    public class Topic
    {
        public string guid { get; set; }
        public string topic_type { get; set; }
        public string topic_status { get; set; }
        public string title { get; set; }
        public List<string> labels { get; set; }
        public DateTime creation_date { get; set; }
        public string creation_author { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_author { get; set; }
        public string assigned_to { get; set; }
        public string description { get; set; }
        public int bimsync_issue_number { get; set; }
        public List<Comment> Comments { get; set;  }
    }

    public class Comment
    {
        public string guid { get; set; }
        public string verbal_status { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
        public string author { get; set; }
        public string comment { get; set; }
        public string topic_guid { get; set; }
        public string viewpoint_guid { get; set; }
        public Viewpoint viewpoint { get; set; }
    }

    public class Viewpoint
    {
        public string guid { get; set; }
        public PerspectiveCamera perspective_camera { get; set; }
        public Lines lines { get; set; }
        public ClippingPlanes clipping_planes { get; set; }
    }

    public class Vector
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class PerspectiveCamera
    {
        public Vector camera_view_point { get; set; }
        public Vector camera_direction { get; set; }
        public Vector camera_up_vector { get; set; }
        public double field_of_view { get; set; }
    }

    public class Lines
    {
        public List<object> line { get; set; }
    }

    public class ClippingPlanes
    {
        public List<ClippingPlane> clipping_plane { get; set; }
    }

    public class ClippingPlane
    {
        public Vector location { get; set; }
        public Vector direction { get; set; }
    }
}
