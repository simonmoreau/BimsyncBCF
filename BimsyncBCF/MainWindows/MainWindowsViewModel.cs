using BimsyncBCF.Models.BCF;
using BimsyncBCF.Models.Bimsync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BimsyncBCF.Services;
using Unity;
using System.Net.Http;

namespace BimsyncBCF.MainWindows
{
    class MainWindowsViewModel : BindableBase
    {
        private Issues.IssueListViewModel _issueListViewModel;
        private HeaderViewModel _headerViewModel;
        private Issues.IssueDetailsViewModel _issueDetailsViewModel = new Issues.IssueDetailsViewModel();

        private BindableBase _currentViewModel;
        

        public MainWindowsViewModel()
        {
            _issueListViewModel = ContainerHelper.Container.Resolve<Issues.IssueListViewModel>();

            HttpClientHandler httpClientHandler = new HttpClientHandler();

            _headerViewModel = new HeaderViewModel(
            new BimsyncService(httpClientHandler),
            new BimsyncBCFService(httpClientHandler)
            );
            CurrentViewModel = _issueListViewModel;
            

            _headerViewModel.IssueBoardSelected += SelectIssueBoard;
            _issueListViewModel.TopicSelected += NavToDetailsView;
            _issueDetailsViewModel.BackToListView += NavToListView;
        }

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public HeaderViewModel HeaderViewModel
        {
            get { return _headerViewModel; }
            set { SetProperty(ref _headerViewModel, value); }
        }

        private void NavToDetailsView(Topic topic)
        {
            _issueDetailsViewModel.SelectedTopic = topic;
            CurrentViewModel = _issueDetailsViewModel;
        }

        private void NavToListView()
        {
            CurrentViewModel = _issueListViewModel;
        }

        private void SelectIssueBoard(IssueBoard selectedIssueBoard)
        {
            _issueListViewModel.SelectedIssueBoard = selectedIssueBoard;
            if (CurrentViewModel != _issueListViewModel) { CurrentViewModel = _issueListViewModel; }
        }

    }
}
