using System;
namespace ProjectTracker.Tests
{
	public class MockTimer : ITimer
	{
		private DateTime _utcNow;
		public DateTime UtcNow
		{
			get { return _utcNow; }
			set
			{
				_utcNow = value;
				Elapsed.Invoke(this, UtcNow);
			}
		}

		public event EventHandler<DateTime> Elapsed;

		public TimeSpan Interval { get; private set; }

		public void Start(TimeSpan interval)
		{
			Interval = interval;
		}

		public void AdvanceSeconds(int seconds)
		{
			UtcNow = UtcNow.AddSeconds(seconds);
		}
	}
}

