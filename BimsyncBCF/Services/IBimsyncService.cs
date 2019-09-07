using BimsyncBCF.Models.Bimsync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimsyncBCF.Services
{
    interface IBimsyncService
    {
        Task<List<Project>> GetProjects();
    }
}
