using System;
using Xunit;

namespace ProjectTracker.Tests
{
	public class ProjectTests
	{
		[Fact]
		public void MustBeTrue()
		{
			Assert.Equal(1,1);
		}

		[Fact]
		public void ProjectBeginInactive()
		{
			// ARRANGE
			var project = new Project("Name");

			// ACT

			// ASSERT
			Assert.False(project.IsActive);
		}

		[Fact]
		public void StartTrackingSetsActive()
		{
			// ARRANGE
			var project = new Project("Name");

			// ACT
			project.StartTracking(DateTime.UtcNow);

			// ASSERT
			Assert.True(project.IsActive);
		}

		[Fact]
		public void StopTrackingSetsInactive()
		{
			// ARRANGE
			var project = new Project("A Project");
			project.StartTracking(DateTime.UtcNow);

			// ACT
			project.StopTracking(DateTime.UtcNow);

			// ASSERT
			Assert.False(project.IsActive);
		}

		[Fact]
		public void TickSegmentInProgressDurationBasedOnCurrentTime()
		{
			// ARRANGE
			var startingTime = DateTime.UtcNow;
			var secondsToAdvance = 10;
			var project = new Project("A Project");
			Assert.Equal(0, project.TotalDurationSeconds); // TODO: Keep one assert per test. Create a seperated test.

			// ACT
			project.StartTracking(startingTime);
			project.Tick(startingTime.AddSeconds(secondsToAdvance));

			// ASSERT
			Assert.Equal(secondsToAdvance, project.TotalDurationSeconds);
		}

		[Fact]
		public void TickSegmentNotInProgressDurationBasedOnStoredTime()
		{
			// ARRANGE
			var project = new Project("A Project");
			var startingTime = DateTime.UtcNow;
			var secondsToAdvance = 10;
			var stopTime = startingTime.AddSeconds(secondsToAdvance);

			project.StartTracking(startingTime);
			project.StopTracking(stopTime);

			// ACT
			project.Tick(stopTime.AddMinutes(1));

			// ASSERT
			Assert.Equal(secondsToAdvance, project.TotalDurationSeconds);
		}
	}
}

