using System;
namespace ProjectTracker
{
	public class Segment
	{
		public DateTime StartTimeUtc { get; }
		public DateTime? EndTimeUtc { get; internal set; }

		public Segment(DateTime startTimeUtc)
		{
			StartTimeUtc = startTimeUtc; 
		}
	}
}

