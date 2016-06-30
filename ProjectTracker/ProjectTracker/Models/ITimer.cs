using System;
namespace ProjectTracker
{
	public interface ITimer
	{
		DateTime UtcNow { get; }
		void Start(TimeSpan interval);
		event EventHandler<DateTime> Elapsed;
	}
}

