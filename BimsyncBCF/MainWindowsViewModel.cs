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

namespace BimsyncBCF
{
    class MainWindowsViewModel : BindableBase
    {
        private Issues.IssueListViewModel _issueListViewModel = new Issues.IssueListViewModel();
        private Issues.IssueDetailsViewModel _issueDetailsViewModel = new Issues.IssueDetailsViewModel();

        private BindableBase _currentViewModel;

        private ObservableCollection<Project> _projects;
        private Project _selectedProject;

        private ObservableCollection<IssueBoard> _issueBoards;
        private IssueBoard _selectedIssueBoard;

        private BimsyncService _bimsyncService = new BimsyncService();
        private BimsyncBCFService _BCFService = new BimsyncBCFService();

        public MainWindowsViewModel()
        {
            CurrentViewModel = _issueListViewModel;
            _issueListViewModel.TopicSelected += NavToDetailsView;
            _issueDetailsViewModel.BackToListView += NavToListView;
        }

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); LoadBoards(); }
        }

        public ObservableCollection<IssueBoard> IssueBoards
        {
            get { return _issueBoards; }
            set { SetProperty(ref _issueBoards, value); }
        }

        public IssueBoard SelectedIssueBoard
        {
            get { return _selectedIssueBoard; }
            set
            {
                SetProperty(ref _selectedIssueBoard, value);
                _issueListViewModel.SelectedIssueBoard = _selectedIssueBoard;
                if (CurrentViewModel != _issueListViewModel) { CurrentViewModel = _issueListViewModel; }
            }
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

        public async void LoadProjects()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            Projects = new ObservableCollection<Project>(await _bimsyncService.GetProjects());
            SelectedProject = Projects.FirstOrDefault();

        }

        public async void LoadBoards()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;
            if (SelectedProject != null)
            {
                IssueBoards = new ObservableCollection<IssueBoard>(await _BCFService.GetIssueBoardsAsync(SelectedProject.id));
                SelectedIssueBoard = IssueBoards.FirstOrDefault();
            }

            
            // _issueListViewModel.SelectedIssueBoard = SelectedIssueBoard;

        }

    }
}
