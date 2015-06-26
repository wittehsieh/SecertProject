using UnityEngine;
using System.Collections;

namespace Cameo
{
	public class MoveHelper : MonoBehaviour 
	{	
		public delegate void OnMoveFinishedDelegate();
		
		private float _curTime;
		private float _during;

		private Vector3 _oriPos;
		private Vector3 _targetPos;
		
		private bool _isExcuting;
		
		private OnMoveFinishedDelegate OnMoveFinished = delegate {};
		
		public static void AttachMoveHelper(GameObject actor, float during, Vector3 targetPos, OnMoveFinishedDelegate onMoveFinished)
		{
			MoveHelper helper = actor.GetComponent<MoveHelper>();
			
			if(helper == null)
			{
				helper = actor.AddComponent<MoveHelper>();
			}
			
			helper.Initialize(during, targetPos, onMoveFinished);
		}
		
		private void Initialize(float during, Vector3 targetPos, OnMoveFinishedDelegate onMoveFinished)
		{
			if(_isExcuting)
			{
				OnMoveFinished();
				OnMoveFinished = delegate {};
			}

			_oriPos = gameObject.transform.localPosition;
			_targetPos = targetPos;

			_during = during;
			_curTime = 0;
			_isExcuting = true;
			
			OnMoveFinished += onMoveFinished;
			
			if(_during == 0)
			{
				gameObject.transform.localPosition = _targetPos;
				Finished();
			}
		}
		
		private void Finished()
		{
			OnMoveFinished();
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
					gameObject.transform.localPosition = Vector3.Lerp(_oriPos, _targetPos, _curTime/_during);
					_curTime += Time.deltaTime;
				}
			}
		}
	}

}
