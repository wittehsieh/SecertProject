using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventChannel : Singleton<EventChannel> 
{
	public Dictionary<string, Action> _channelDic = new Dictionary<string, Action>();

	/// <summary>
	/// Attach the listener
	/// </summary>
	/// <param name="channel">cahnnel name, please create and use events from EventCenter</param>
	/// <param name="act">Act.</param>
	public void AttachListener(string channel, Action act)
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

	/// <summary>
	/// Detach the listener
	/// </summary>
	/// <param name="channel">cahnnel name, please create and use events from EventCenter</param>
	/// <param name="act">Act.</param>
	public void DetachListner(string channel, Action act)
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

	/// <summary>
	/// Invoke channel event
	/// </summary>
	/// <param name="channel">cahnnel name, please create and use events from EventCenter</param>
	public void Invoke(string channel)
	{
		if(_channelDic.ContainsKey(channel))
		{
			_channelDic[channel].Invoke();
		}
	}
}