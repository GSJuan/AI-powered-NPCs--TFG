using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using Whisper;
using Whisper.Utils;



/// <summary>
/// This class implements the use of https://github.com/Macoron/whisper.unity pasckage into the multimodal interface
/// </summary>
/// 
namespace TextSpeech
{
    public class SpeechToText : MonoBehaviour
    {

        public Action<string> onResultCallback;
        public Action<string> onPartialResultsCallback;

        public WhisperManager whisper;
        public MicrophoneRecord microphoneRecord;
        public bool streamSegments = true;

        [Header("UI")]
        public Text timeText;
        public Dropdown languageDropdown;
        public Toggle translateToggle;


        private string _buffer;

        private static SpeechToText _instance;
        public static SpeechToText Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Create if it doesn't exist
                    GameObject go = new GameObject("SpeechToText");
                    _instance = go.AddComponent<SpeechToText>();
                }
                return _instance;
            }
        }
        
        //singleton
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                throw (new System.Exception("Only one instance of SpeechToText is allowed"));
            }
            
            languageDropdown.value = languageDropdown.options.FindIndex(op => op.text == whisper.language);
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

            translateToggle.isOn = whisper.translateToEnglish;
            translateToggle.onValueChanged.AddListener(OnTranslateChanged);
            
            microphoneRecord.OnRecordStop += Transcribe;

            if (streamSegments)
                whisper.OnNewSegment += WhisperOnNewSegment;
        }

        public void SetLanguage(string language)
        {
            
        }
        
        private void OnLanguageChanged(int ind)
        {
            var opt = languageDropdown.options[ind];
            whisper.language = opt.text;
        }

        

        private void OnTranslateChanged(bool translate)
        {
            whisper.translateToEnglish = translate;
        }

        /// <summary>
        /// This method is called by the voice controller when the npc triggers the event of start listening,
        /// which happens when the npc detects tht he is being gazed at by the user.
        /// </summary>
        /// 
        public void StartRecording(string _message = "")
        {
            microphoneRecord.StartRecord();
        }
        
        /// <summary>
        /// This method is called by the voice controller when the npc triggers the event of stop listening,
        /// which happens when the npc detects tht he is not being gazed at by the user.
        /// </summary>
        /// 
        public void StopRecording()
        {
            microphoneRecord.StopRecord();
        }

        private void WhisperOnNewSegment(WhisperSegment segment)
        {
            _buffer += segment.Text;
            onPartialResultsCallback(_buffer + "...");
        }

        private async void Transcribe(float[] data, int frequency, int channels, float length)
        {
            _buffer = "";

            var sw = new Stopwatch();
            sw.Start();

            var res = await whisper.GetTextAsync(data, frequency, channels);

            var time = sw.ElapsedMilliseconds;
            var rate = length / (time * 0.001f);
            timeText.text = $"Time: {time} ms\nRate: {rate:F1}x";
            if (res == null)
                return;

            var text = res.Result;

            onResultCallback(text);
        }


    }
}
