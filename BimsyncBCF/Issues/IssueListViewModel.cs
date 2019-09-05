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
        private IssueBoard _selectedIssueBoard;

        public IssueListViewModel()
        {
        }

        public async void LoadTopics()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;
            if (SelectedIssueBoard != null)
            {
                Issues = new ObservableCollection<Topic>(await _BCFService.GetTopicsAsync(SelectedIssueBoard.project_id));
            }
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

        public IssueBoard SelectedIssueBoard
        {
            get { return _selectedIssueBoard; }
            set { SetProperty(ref _selectedIssueBoard, value); LoadTopics(); }
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
