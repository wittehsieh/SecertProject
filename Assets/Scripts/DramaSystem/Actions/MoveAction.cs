using UnityEngine;
using System.Collections;
using LitJson;

namespace Cameo
{
	/// <summary>
	/// {
	///		"Name": "MoveAction",
	/// 	"Actor": "Fly",
	///		"MoveTo": "(0, 10, 0)",
	///		"During": "2"
	///	}
	/// Actor: ActorName
	/// MoveTo: TargetPosition
	/// During: Move time
	/// </summary>
	public class MoveAction : BaseDramaAction 
	{	
		private string _targetName;
		private Vector3 _targetPosition;
		private float _during;
		
		public MoveAction(JsonData jsonAction)
		{
			_targetName = jsonAction["Actor"].ToString();
			_during = float.Parse(jsonAction["During"].ToString());

			_targetPosition = StringToVector3(jsonAction["MoveTo"].ToString());
		}
		
		public override void Excute (OnActionFinishedDelegate onActionFinished)
		{
			base.Excute (onActionFinished);
			
			GameObject targetActor = ActorHandler.Instance.GetActor(_targetName);
			
			if(targetActor != null)
			{
				MoveHelper.AttachMoveHelper(targetActor, _during, _targetPosition, OnMoveFinished);
			}
		}

		private Vector3 StringToVector3(string strVec3)
		{
			strVec3 = strVec3.Substring(1, strVec3.Length - 2);
			string[] strNums = strVec3.Split(',');

			Vector3 vec3 = Vector3.zero;
			vec3.x = float.Parse(strNums[0]);
			vec3.y = float.Parse(strNums[1]);
			vec3.z = float.Parse(strNums[2]);

			return vec3;
		}

		private void OnMoveFinished()
		{
			OnActionFinished();
		}
	}
}