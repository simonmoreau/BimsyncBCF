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
        public IssueDetailsViewModel()
        {
            BackCommand = new RelayCommand(OnBack);
        }

        private Topic _selectedTopic;

        public Topic SelectedTopic
        {
            get { return _selectedTopic; }
            set { SetProperty(ref _selectedTopic, value); }
        }

        public event Action BackToListView = delegate { };

        public RelayCommand BackCommand { get; private set; }

        private void OnBack()
        {
            BackToListView();
        }
    }
}
