using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventChannel : Singleton<EventChannel> 
{
	public Dictionary<string, Action> _channelDic = new Dictionary<string, Action>();
	
	public static void AttachListener(string channel, Action act)
	{
		if(IsExistInstance)
		{
			Instance.attachListener(channel, act);
		}
	}
	
	public static void DetachListner(string channel, Action act)
	{
		if(IsExistInstance)
		{
			Instance.detachListner(channel, act);
		}
	}
	
	void Start()
	{
		RegisterInstance();
	}
	
	private void attachListener(string channel, Action act)
	{
		if(channel == null)
		{
			Debug.LogError("[EventChannel.AttachListener] Channel cannot be null");
			return;
		}
		
		if(!_channelDic.ContainsKey(channel))
		{
			_channelDic.Add(channel, delegate {});
		}
		
		_channelDic[channel] += act;
	}
	
	private void detachListner(string channel, Action act)
	{
		if(channel == null)
		{
			Debug.LogError("[EventChannel.DetachListner] Channel cannot be null");
			return;
		}
		
		if(_channelDic.ContainsKey(channel))
		{
			_channelDic[channel] -= act;
		}
	}
	
	public void Invoke(string channel)
	{
		if(_channelDic.ContainsKey(channel))
		{
			_channelDic[channel].Invoke();
		}
	}
}