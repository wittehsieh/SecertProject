using UnityEngine;
using System;
using System.Collections;
using LitJson;

namespace Cameo
{
	public class BaseDramaAction
	{
		public delegate void OnActionFinishedDelegate();
		public OnActionFinishedDelegate OnActionFinished = delegate {};

		public virtual void Excute(OnActionFinishedDelegate onActionFinished)
		{
			OnActionFinished += onActionFinished;
		}
	}
}
