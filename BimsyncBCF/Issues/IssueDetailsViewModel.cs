using BimsyncBCF.Models.BCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimsyncBCF.Issues
{
    class IssueDetailsViewModel : BindableBase
    {
        private Topic _selectedTopic;

        public Topic SelectedTopic
        {
            get { return _selectedTopic; }
            set { SetProperty(ref _selectedTopic, value); }
        }
    }
}
