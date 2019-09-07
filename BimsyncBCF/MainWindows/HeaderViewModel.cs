using BimsyncBCF.Models.BCF;
using BimsyncBCF.Models.Bimsync;
using BimsyncBCF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BimsyncBCF.MainWindows
{
    class HeaderViewModel : BindableBase
    {
        private ObservableCollection<Project> _projects;
        private Project _selectedProject;

        private ObservableCollection<IssueBoard> _issueBoards;
        private IssueBoard _selectedIssueBoard;

        private IBimsyncService _bimsyncService;
        private IBCFService _BCFService;

        public HeaderViewModel(IBimsyncService bimsyncService, IBCFService bCFService)
        {
            _bimsyncService = bimsyncService;
            _BCFService = bCFService;
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
                OnIssueBoardSelected();
            }
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

        public event Action<IssueBoard> IssueBoardSelected = delegate { };

        private void OnIssueBoardSelected()
        {
            IssueBoardSelected(_selectedIssueBoard);
        }
    }
}
