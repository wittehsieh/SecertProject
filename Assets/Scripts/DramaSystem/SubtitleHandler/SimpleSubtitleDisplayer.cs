using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Cameo
{
	public class SimpleSubtitleDisplayer : BaseSubtitleDisplayer 
	{	
		public UnityEngine.UI.Text Displayer;

		public override void OnSetSubtitle (string text)
		{
			if(Displayer != null)
			{
				Displayer.text = text;
			}
		}

		public override void OnClear ()
		{
			if(Displayer != null)
			{
				Displayer.text = "";
			}
		}
	}
}

