using UnityEngine;
using System.Collections;
using LitJson;

namespace Cameo
{
	/// <summary>
	/// {
	///		"Name": "FadeAction",
	/// 	"Actor": "Fly",
	///		"FadeIn": "True",
	///		"During": "2"
	///	}
	/// Actor: ActorName
	/// FadeIn: True: fade in /False: fade out
	/// During: Fade time
	/// </summary>
	public class FadeAction : BaseDramaAction 
	{	
		private string _targetName;
		private bool _isFadeIn;
		private float _during;

		public FadeAction(JsonData jsonAction)
		{
			_targetName = jsonAction["Actor"].ToString();
			_during = float.Parse(jsonAction["During"].ToString());
			_isFadeIn = bool.Parse(jsonAction["FadeIn"].ToString());
		}

		public override void Excute (OnActionFinishedDelegate onActionFinished)
		{
			base.Excute (onActionFinished);

			GameObject targetActor = ActorHandler.Instance.GetActor(_targetName);

			if(targetActor != null)
			{
				FadeHelper.AttachFadeHelper(targetActor, _during, _isFadeIn, OnFadeFinished);
			}
		}

		private void OnFadeFinished()
		{
			OnActionFinished();
		}
	}
}

