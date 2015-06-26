using UnityEngine;
using System.Collections;
using LitJson;

namespace Cameo
{
	public static class DramaActionIndustry 
	{
		public static BaseDramaAction CreateAction(JsonData actionData)
		{
			BaseDramaAction action = null;

			string actionName = actionData["Name"].ToString();

			if(actionName == "ShowSubtitleAction")
			{
				action = new ShowSubtitleAction(actionData);
			}
			else if(actionName == "FadeAction")
			{
				action = new FadeAction(actionData);
			}
			else if(actionName == "MoveAction")
			{
				action = new MoveAction(actionData);
			}

			return action;
		}
	}
}