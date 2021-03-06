using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;
	
	protected static readonly object _synObject = new object();
	
	#region Property Message
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				lock (_synObject)
				{
					if (_instance == null)
					{
						_instance = FindObjectOfType<T>();
					}
				}
				
				if (_instance == null)
				{
					Init();
					
					Debug.Log("An instance of " + typeof(T) +
					          " is needed in the scene, but there is none. Created automatically");
				}
			}
			
			return _instance;
		}
	}
	
	public static  bool IsExistInstance	{get {return _instance != null;}}
	#endregion
	
	public static T Init()
	{
		if(_instance == null)
		{
			GameObject obj = new GameObject(typeof(T).ToString());
			_instance = obj.AddComponent<T>();
			DontDestroyOnLoad(obj);
		}
		return _instance;
	}
	
	public void RegisterInstance () 
	{
		if (_instance == null)
		{
			_instance = GetComponent<T>();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}
	//For Editor Use
	public void Destory()
	{
		if (_instance != null)
		{
			DestroyImmediate(_instance.gameObject);
			_instance = null;
		}
		T [] instances = FindObjectsOfType<T>();
		for(int i = 0; i < instances.Length; i++)
		{
			DestroyImmediate(instances[i].gameObject);
		}
	}
}

