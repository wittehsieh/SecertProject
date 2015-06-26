using UnityEngine;
using System.Collections;

namespace Cameo
{
	public class BaseSubtitleDisplayer : MonoBehaviour 
	{
		void Start () 
		{
			SubtitleHandler.Instance.OnSetSubtitle += OnSetSubtitle;
			SubtitleHandler.Instance.OnClearSubtitle += OnClear;
		}

		void OnDestroy()
		{
			if(SubtitleHandler.IsExistInstance)
			{
				SubtitleHandler.Instance.OnSetSubtitle -= OnSetSubtitle;
				SubtitleHandler.Instance.OnClearSubtitle -= OnClear;
			}
		}

		public virtual void OnSetSubtitle(string text)
		{
			Debug.Log("[BaseSubtitleDisplayer.OnSetSubtitle] Show: " + text);
		}

		public virtual void OnClear()
		{
			Debug.Log("[BaseSubtitleDisplayer.Clear]");
		}
	}
}