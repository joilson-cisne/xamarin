using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace ProjectTracker
{
	public class ProjectTrackerViewModel : BindableObjectBase
	{
		private readonly ITimer _timer;
		private Project _currentProject;
		private ObservableCollection<Project> _projects = new ObservableCollection<Project>();
		private string _newProjectName;

		public ProjectTrackerViewModel(ITimer timer)
		{
			_timer = timer;

			AddCommand = new Command(addProject);
			//ResetCommand = new Command(reset);
			//ToggleCommand = new Command<Project>(toggleProject);

			_timer.Start(TimeSpan.FromSeconds(1));
			_timer.Elapsed += (sender, tickUtc) => _currentProject.Tick(tickUtc);
		}

		public ICommand AddCommand { get; }
		public ICommand ResetCommand { get; }
		public ICommand ToggleCommand { get; }

		public ObservableCollection<Project> Projects { 
			get { return _projects; }
			set
			{
				_projects = value;
				RaisePropertyChanged(nameof(Projects));
			}
		}

		public string NewProjectName
		{
			get { return _newProjectName; }
			set
			{
				_newProjectName = value;
				RaisePropertyChanged(nameof(NewProjectName));
			}
		}

		private void addProject()
		{
			if (string.IsNullOrWhiteSpace(NewProjectName))
			{
				return;
			}

			Projects.Add(new Project(NewProjectName));
			NewProjectName = string.Empty;
		}
	}
}

