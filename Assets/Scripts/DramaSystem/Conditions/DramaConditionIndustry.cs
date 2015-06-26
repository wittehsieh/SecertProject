using UnityEngine;
using System.Collections;
using LitJson;

namespace Cameo
{
	public static class DramaConditionIndustry 
	{
		public static BaseDramaCondition CreateCondition(JsonData conditionData)
		{
			BaseDramaCondition condition = null;

			string conditionName = conditionData["Name"].ToString();

			if(conditionName == "ReachTimeCondition")
			{
				condition = new ReachTimeCondition(conditionData);
			}

			return condition;
		}
	}
}