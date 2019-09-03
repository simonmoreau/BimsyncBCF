using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BimsyncBCF.Services;
using BimsyncBCF.Models.BCF;

namespace BimsyncBCF.Issues
{
    public class IssueListViewModel : BindableBase
    {
        private BimsyncBCFService _BCFService = new BimsyncBCFService();
        private ObservableCollection<Topic> _issues;
        private Topic _selectedTopic;

        public IssueListViewModel()
        {
        }

        public async void LoadTopics()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            Issues = new ObservableCollection<Topic>(await _BCFService.GetTopicsAsync("665ed058-3bfb-436f-b17c-3d3bc82da309"));

        }

        public ObservableCollection<Topic> Issues
        {
            get { return _issues; }
            set { SetProperty(ref _issues, value); }
        }

        public Topic SelectedTopic
        {
            get { return _selectedTopic; }
            set  {SetProperty(ref _selectedTopic, value); OnSelectTopic(); }
        }

        public event Action<Topic> TopicSelected = delegate { };

        private void OnSelectTopic()
        {
            if (_selectedTopic != null)
            {
                TopicSelected(_selectedTopic);
            }
            _selectedTopic = null;
        }
    }
}
