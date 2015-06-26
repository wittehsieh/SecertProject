using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;

namespace Cameo
{
	[RequireComponent(typeof(AudioSource))]
	public class DramaManager : Singleton<DramaManager> 
	{
		private List<DramaEvent> _dramaEvents;

		private AudioSource _musicPlayer;
		private bool _isExcuting;

		public bool IsExcuting
		{
			get
			{
				return _isExcuting;
			}
		}

		void Start()
		{
			Initialize();
		}

		#region Public Methods
		public void Initialize()
		{
			_dramaEvents = new List<DramaEvent>();
			_musicPlayer = GetComponent<AudioSource>();

			_musicPlayer.Stop();

			TimeHandler.Instance.Initialize();
			SubtitleHandler.Instance.Initialize();
			ActorHandler.Instance.Initialize();

			_isExcuting = false;
		}

		public void Play(string jsonStr)
		{
			if(IsExcuting)
			{
				Initialize();
			}

			Construct(jsonStr);

			_musicPlayer.Play();
			_isExcuting = true;
		}
		#endregion

		private void Construct(string jsonStr)
		{
			JsonData jDrama = JsonMapper.ToObject<JsonData>(jsonStr);
			JsonData jEvents = jDrama["Events"];
			
			for(int i=0; i<jEvents.Count; ++i)
			{
				DramaEvent dEvent = new DramaEvent(jEvents[i]);
				dEvent.OnFinish += RemoveEvent;
				_dramaEvents.Add(dEvent);
			}

			TimeHandler.Instance.StartCounting();
			ActorHandler.Instance.LoadActors(jDrama["Actors"]);
		}

		private void RemoveEvent(DramaEvent dramaEvent)
		{
			if(_dramaEvents.Contains(dramaEvent))
			{
				_dramaEvents.Remove(dramaEvent);
			}

			if(_dramaEvents.Count == 0)
			{
				_isExcuting = false;
				Initialize();
			}
		}
	}
}
