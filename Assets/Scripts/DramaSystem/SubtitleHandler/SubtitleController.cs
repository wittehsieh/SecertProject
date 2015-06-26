using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;

namespace Cameo
{
	public class SubtitleController : MonoBehaviour {

		public Text SubtitleDisplayer;
		private TextAsset _subtitleText;
		private JsonData LstSubtitle;
		private bool _isPlaying = false;
		private int _intCurrentSubtitleIndex = 0;

		// Use this for initialization
		void Start () {
			_subtitleText = Resources.Load<TextAsset>(SongManager.Instance.SelectedInfo.SubtitleResourcePath);
			Debug.Log ("[SubtitleController] Start / SubtitleResourcePath: " + SongManager.Instance.SelectedInfo.SubtitleResourcePath);
			LstSubtitle = JsonMapper.ToObject (_subtitleText.ToString());
			SongManager.Instance.OnAudioPlayStarted += OnAudioPlayStarted;
			SongManager.Instance.OnAudioPlayFinished += OnAudioPlayFinished;
		}
	
		// Update is called once per frame
		void Update () {
			if (_isPlaying && _intCurrentSubtitleIndex < LstSubtitle.Count) {
				float floatNextSubtitleShowSecond = StringConvertToTime(LstSubtitle[_intCurrentSubtitleIndex]["Time"].ToString());
				//Debug.Log ("[SubtitleController] Update. AudioPosition " + SongManager.Instance.AudioPosition + " intNextSubtitleShowSecond " + floatNextSubtitleShowSecond);
				if (SongManager.Instance.AudioPosition > floatNextSubtitleShowSecond) {
					string StrSubtitleText = LstSubtitle[_intCurrentSubtitleIndex]["Text"].ToString();
					SubtitleDisplayer.text = StrSubtitleText;
					++_intCurrentSubtitleIndex;
				}
			}
		}

		private void OnAudioPlayStarted() {
			_isPlaying = true;
			_intCurrentSubtitleIndex = 0;
		}
		
		private void OnAudioPlayFinished() {
			_isPlaying = false;
			_intCurrentSubtitleIndex = 0;
			SubtitleDisplayer.text = "";
		}

		void OnDestroy () {
			if (!SongManager.IsExistInstance)
				return;
			SongManager.Instance.OnAudioPlayStarted -= OnAudioPlayStarted;
			SongManager.Instance.OnAudioPlayFinished -= OnAudioPlayFinished;
		}

		//Convert string "00:00:00" to second
		private float StringConvertToTime(string timeStr)
		{
			string[] formatTimeStr = timeStr.Split(':');
			float[] formatTime = new float[formatTimeStr.Length];
			
			for(int i=0; i<formatTime.Length; ++i)
			{
				formatTime[i] = float.Parse(formatTimeStr[i]);
			}
			
			return formatTime[0] * 60 * 60 + formatTime[1] * 60 + formatTime[2];
		}
	}
}
