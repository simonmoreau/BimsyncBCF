using BimsyncBCF.Models.BCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimsyncBCF
{
    class MainWindowsViewModel : BindableBase
    {
        private Issues.IssueListViewModel _issueListViewModel = new Issues.IssueListViewModel();
        private Issues.IssueDetailsViewModel _issueDetailsViewModel = new Issues.IssueDetailsViewModel();

        private BindableBase _currentViewModel;

        public MainWindowsViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            CurrentViewModel = _issueListViewModel;
            _issueListViewModel.TopicSelected += NavToDetailsView;
        }

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "issueList":
                    CurrentViewModel = _issueListViewModel;
                    break;
                case "issueDetails":
                default:
                    CurrentViewModel = _issueDetailsViewModel;
                    break;
            }
        }

        private void NavToDetailsView(Topic topic)
        {
            _issueDetailsViewModel.SelectedTopic = topic;
            CurrentViewModel = _issueDetailsViewModel;
        }

    }
}
