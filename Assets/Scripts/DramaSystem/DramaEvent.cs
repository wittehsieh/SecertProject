using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Cameo
{
	public class DramaEvent
	{
		private BaseDramaCondition _condition;
		private List<BaseDramaAction> _actions;
		private int _finishedActionCount = 0;

		public Action<DramaEvent> OnFinish = delegate { };

		public DramaEvent(JsonData jsonData)
		{
			_actions = new List<BaseDramaAction>();

			JsonData condition = jsonData["Condition"];

			_condition = DramaConditionIndustry.CreateCondition(condition);
			if(_condition != null)
			{
				_condition.OnFinished += OnConditionFinished;
			}

			JsonData actions = jsonData["Actions"];
			for(int i=0; i<actions.Count; ++i)
			{
				BaseDramaAction dramaAction = DramaActionIndustry.CreateAction(actions[i]);
				if(dramaAction != null)
				{
					_actions.Add(dramaAction);
				}
			}
		}

		private void OnConditionFinished()
		{
			//Excute actions
			for(int i=0; i<_actions.Count; ++i)
			{
				_actions[i].Excute(OnActionFinished);
			}
		}

		private void OnActionFinished()
		{
			_finishedActionCount++;

			//Finish Event
			if(_finishedActionCount == _actions.Count)
			{
				OnFinish(this);
			}
		}
	}
}

