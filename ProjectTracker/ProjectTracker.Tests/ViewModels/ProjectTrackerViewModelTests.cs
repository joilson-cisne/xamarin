using Xunit;

namespace ProjectTracker.Tests
{
	public class ProjectTrackerViewModelTests
	{
		private readonly MockTimer _timer;

		public ProjectTrackerViewModelTests()
		{
			_timer = new MockTimer();
		}

		[Fact]
		public void ConstructorStartsTimerWithOneSencondInterval()
		{
			var viewModel = getViewModel("My Project");
			Assert.Equal(1, _timer.Interval.TotalSeconds);
		}

		private ProjectTrackerViewModel getViewModel(params string[] projectNames)
		{
			var viewModel = new ProjectTrackerViewModel(_timer);

			foreach (var projectName in projectNames)
			{
				viewModel.NewProjectName = projectName;
				viewModel.AddCommand.Execute(null);
			}

			return viewModel;
		}
	}
}

