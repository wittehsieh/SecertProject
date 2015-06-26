using UnityEngine;
using System.Collections;
using LitJson;

namespace Cameo
{
	/// <summary>
	/// {
	///		"Name": "ShowSubtitleAction",
	///		"Text": "Subtitle1",
	///		"During": "2"
	///	}
	/// Text: Content
	/// During: Text display time. If set -1, it will still display until next subtitle setting
	/// </summary>
	public class ShowSubtitleAction : BaseDramaAction 
	{
		private string _text;
		private float _during;

		public ShowSubtitleAction(JsonData jsonAction)
		{
			_text = jsonAction["Text"].ToString();
			_during = float.Parse(jsonAction["During"].ToString());
		}

		public override string ToString ()
		{
			return string.Format ("[ShowSubtitleAction] Text: {0}", _text);
		}

		public override void Excute (OnActionFinishedDelegate onActionFinished)
		{
			base.Excute(onActionFinished);
			SubtitleHandler.Instance.SetSubtitle(_text, _during, OnSubtitleDisplayFinished);
		}

		private void OnSubtitleDisplayFinished()
		{
			OnActionFinished();
		}
	}
}