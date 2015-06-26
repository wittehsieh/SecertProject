using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Cameo
{
	public class ActorHandler : Singleton<ActorHandler> 
	{
		public Dictionary<string, GameObject> _actors;

		public void Initialize()
		{
			if(_actors != null)
			{
				RemoveAllActors();
			}
			else
			{
				_actors = new Dictionary<string, GameObject>();
			}
		}

		public void LoadActors(JsonData actorsData)
		{
			for(int i=0; i<actorsData.Count; ++i)
			{
				JsonData actorData = actorsData[i];
				string actorName = actorData["Name"].ToString();
				string resPath = actorData["Resource"].ToString();

				if(_actors.ContainsKey(actorName))
				{
					Debug.Log("[ActorHandler.LoadActors] Actor " + actorName + " is already loaded");
					continue;
				}

				GameObject actorPrefab = Resources.Load(resPath, typeof(Object)) as GameObject;
				if(actorPrefab.GetComponent<BaseDramaActor>() == null)
				{
					Debug.Log("[ActorHandler.LoadActors] Object " + actorName + " is not an actor, please attach BaseDramaActorComponent");
					continue;
				}

				if(actorPrefab != null)
				{
					GameObject actorObject = Instantiate(actorPrefab) as GameObject;
					actorObject.GetComponent<BaseDramaActor>().Name = actorName;
					_actors.Add(actorName, actorObject);
				}
				else
				{
					Debug.Log("[ActorHandler.LoadActors] Actor " + actorName + "'s resource is not exist");
				}
			}
		}

		public GameObject GetActor(string name)
		{
			if(_actors.ContainsKey(name))
			{
				return _actors[name];
			}
			else
			{
				Debug.Log("[ActorHandler.LoadActors] Actor " + name + " is not exist");
				return null;
			}
		}

		private void RemoveAllActors()
		{
			foreach(GameObject actorObj in _actors.Values)
			{
				Destroy(actorObj);
			}
			_actors.Clear();
		}
	}
}

