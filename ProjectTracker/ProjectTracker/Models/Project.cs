using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracker
{
	public class Project : BindableObjectBase
	{
		private bool _isActive = false;
		private readonly IList<Segment> _segments = new List<Segment>();
		private Segment _activeSegment;
		private int _totalDurationSeconds = 0;

		public string Name { get; }

		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				RaisePropertyChanged(nameof(IsActive));
			}
		}

		public int TotalDurationSeconds
		{
			get
			{
				return _totalDurationSeconds;
			}
			set
			{
				_totalDurationSeconds = value;
				RaisePropertyChanged(nameof(TotalDurationSeconds));
			}
		}

		public Project(string name)
		{
			Name = name;
		}

		public void StartTracking(DateTime startTimeUtc)
		{
			if (_activeSegment != null)
			{
				StopTracking(DateTime.UtcNow);
			}

			_activeSegment = new Segment(startTimeUtc);
			_segments.Add(_activeSegment);

			IsActive = true;
		}

		public void StopTracking(DateTime stopTimeUtc)
		{
			if (_activeSegment == null)
			{
				return;
			}

			_activeSegment.EndTimeUtc = stopTimeUtc;
			_activeSegment = null; // todo: review this code

			IsActive = false;
		}

		public void Tick(DateTime utcNow)
		{
			TotalDurationSeconds =
				(int)_segments.Sum(segment =>
								   (segment.EndTimeUtc ?? utcNow)
								   .Subtract(segment.StartTimeUtc)
								   .TotalSeconds);
		}
}
}

