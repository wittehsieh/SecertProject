using UnityEngine;
using System.Collections;

namespace Cameo
{
	public class BaseDramaActor : MonoBehaviour 
	{
		public string Name;

		void Start () 
		{
			gameObject.SetActive(false);
			renderer.material.color = new Color(1, 1, 1, 0);
		}
	}
}

