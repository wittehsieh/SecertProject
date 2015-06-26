using UnityEngine;
using System.Collections;

namespace Cameo
{
	public class SubtitleHandler : Singleton<SubtitleHandler>
	{	
		public delegate void OnSetSubtitleDelegate(string text);
		public delegate void OnClearSubtitleDelegate();
		public delegate void OnSubtitleDisplayFinishedDelegate();

		public OnSetSubtitleDelegate OnSetSubtitle = delegate(string text){};
		public OnClearSubtitleDelegate OnClearSubtitle = delegate(){};
		public OnSubtitleDisplayFinishedDelegate OnSubtitleDisplayFinished = delegate() {};

		private float _remainTime = 0;
		private bool _isRemainInfinity = true;

		public void Initialize()
		{
			ClearSubtitle();
			_remainTime = 0;
		}

		public void SetSubtitle(string text, float during, OnSubtitleDisplayFinishedDelegate onSubtitleDisplayFinished)
		{
			_isRemainInfinity = (during == -1) ? true : false;

			if(_remainTime > 0)
			{
				OnSubtitleDisplayFinished();
				ClearSubtitle();
			}

			_remainTime = during;
			OnSubtitleDisplayFinished = onSubtitleDisplayFinished;

			OnSetSubtitle(text);
		}

		public void ClearSubtitle()
		{
			_isRemainInfinity = true;
			OnSubtitleDisplayFinished = delegate {};
			OnClearSubtitle();
		}

		void Update()
		{
			if(!_isRemainInfinity)
			{
				_remainTime -= Time.deltaTime;
				
				if(_remainTime < 0)
				{
					OnSubtitleDisplayFinished();
					ClearSubtitle();
				}
			}
		}
	}
}