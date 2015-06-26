using UnityEngine;
using System.Collections;
using LitJson;

namespace Cameo
{
	public class ReachTimeCondition : BaseDramaCondition 
	{
		private float _timeFromStart = 0;

		public ReachTimeCondition(JsonData conditionData)
		{
			_timeFromStart = StringConvertToTime(conditionData["Time"].ToString());

			TimeHandler.Instance.AddTimePoint(_timeFromStart, OnTimeReached);
		}

		//Convert string "00:00:00" to second
		private int StringConvertToTime(string timeStr)
		{
			string[] formatTimeStr = timeStr.Split(':');
			int[] formatTime = new int[formatTimeStr.Length];
			
			for(int i=0; i<formatTime.Length; ++i)
			{
				formatTime[i] = int.Parse(formatTimeStr[i]);
			}
			
			return formatTime[0] * 60 * 60 + formatTime[1] * 60 + formatTime[2];
		}

		private void OnTimeReached()
		{
			Debug.Log("[ReachTimeCondition] Time " + _timeFromStart + " Reached!");
			OnFinished();
		}
	}
}

