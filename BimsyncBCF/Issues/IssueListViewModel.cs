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

        public IssueListViewModel()
        {
        }

        public async void LoadTopics()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            Issues = new ObservableCollection<Topic>(await _BCFService.GetTopicsAsync("b6392340-e5e4-430a-9732-eeb9e4d5732e"));

        }

        public ObservableCollection<Topic> Issues
        {
            get { return _issues; }
            set { SetProperty(ref _issues, value); }
        }

    }
}
