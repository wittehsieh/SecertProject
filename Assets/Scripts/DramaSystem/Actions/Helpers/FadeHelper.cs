using UnityEngine;
using System.Collections;

namespace Cameo
{
	public class FadeHelper : MonoBehaviour 
	{	
		public delegate void OnFadeFinishedDelegate();

		private float _curTime;
		private float _during;
		private Color _oriColor;
		private Color _targetColor;

		private bool _isExcuting;
		private bool _isFadeIn;

		private OnFadeFinishedDelegate OnFadeFinished = delegate {};

		public static void AttachFadeHelper(GameObject actor, float during, bool isFadeIn, OnFadeFinishedDelegate onFadeFinished)
		{
			FadeHelper helper = actor.GetComponent<FadeHelper>();

			if(helper == null)
			{
				helper = actor.AddComponent<FadeHelper>();
			}

			helper.Initialize(during, isFadeIn, onFadeFinished);
		}

		private void Initialize(float during, bool isFadeIn, OnFadeFinishedDelegate onFadeFinished)
		{
			if(_isExcuting)
			{
				OnFadeFinished();
				OnFadeFinished = delegate {};
			}

			gameObject.SetActive(true);

			_isFadeIn = isFadeIn;
			float targetAlpha = (_isFadeIn) ? 1 : 0;

			_oriColor = renderer.material.color;
			_targetColor = new Color(_oriColor.r, _oriColor.g, _oriColor.b, targetAlpha);

			_during = during;
			_curTime = 0;
			_isExcuting = true;

			OnFadeFinished += onFadeFinished;

			if(_during == 0)
			{
				renderer.material.color = _targetColor;
				Finished();
			}
		}

		private void Finished()
		{
			if(!_isFadeIn)
			{
				gameObject.SetActive(false);
			}

			OnFadeFinished();
			Component.Destroy(this);
		}

		void Update () 
		{
			if(_isExcuting)
			{
				if(_curTime >= _during)
				{
					Finished();
				}
				else
				{
					renderer.material.color = Color.Lerp(_oriColor, _targetColor, _curTime/_during);
					_curTime += Time.deltaTime;
				}
			}
		}
	}

}

