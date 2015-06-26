using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cameo
{
	public class TimeEvent
	{
		public float TimePoint;
		public Action TimeDelegate;

		public TimeEvent(float time, Action timeDelegate)
		{
			TimePoint = time;
			TimeDelegate = timeDelegate;
		}
	}

	public class TimeHandler : Singleton<TimeHandler>
	{
		private float _currentTime;
		private bool _isCounting = false;
		private List<TimeEvent> _timeEvents;

		void Update()
		{
			if(_isCounting)
			{
				_currentTime += Time.deltaTime;
				CheckTimeEvents();
			}
		}

		public void Initialize()
		{
			_timeEvents = new List<TimeEvent>();
			_currentTime = 0;
			_isCounting = false;
		}

		public void StartCounting()
		{
			_isCounting = true;
		}

		public void AddTimePoint(float time, Action timeDelegate)
		{
			TimeEvent timeEvt = new TimeEvent(time, timeDelegate);

			bool isInserted =  false;

			for(int i=0; i<_timeEvents.Count; ++i)
			{
				if(timeEvt.TimePoint < _timeEvents[i].TimePoint)
				{
					_timeEvents.Insert(i, timeEvt);
					isInserted = true;
				}
			}

			if(!isInserted)
			{
				_timeEvents.Add(timeEvt);
			}
		}

		private void CheckTimeEvents()
		{
			for(int i=0; i<_timeEvents.Count; ++i)
			{
				if(_currentTime >= _timeEvents[i].TimePoint)
				{
					TimeEvent timeEvt = _timeEvents[i];

					_timeEvents.Remove(timeEvt);

					timeEvt.TimeDelegate();
				}
				else
				{
					break;
				}
			}
		}
	}
}

