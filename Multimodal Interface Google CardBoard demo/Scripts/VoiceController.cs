using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;
using UnityLibrary;
using TMPro;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "es-ES";

    public static VoiceController Instance;

    [SerializeField]
    public TMP_Text input_text;
    public GameObject recordingIcon;
    
    //Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw (new System.Exception("Only one instance of OpenAI is allowed"));
        }


        recordingIcon.SetActive(false);
    }

    
    private void Start()
    {
        Setup(LANG_CODE);
        //we set up the needed callback functions
#if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        CheckPermission();
    }

    /// <summary>
    /// Method to check if the user has given the permission to use the microphone
    /// </summary>
    /// 
    void CheckPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    #region Text To Speech

    /// <summary>
    /// Method called by the OpenAi controller to start the speech based off of the generated response
    /// </summary>
    ///
    public void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    /// <summary>
    /// Method that could be used to stop the speech abruptly
    /// </summary>
    ///
    public void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    /// <summary>
    /// Method called as a callback when the speech recognition starts
    /// </summary>
    ///
    void OnSpeakStart()
    {
        Debug.Log("OnSpeakStart");
    }

    /// <summary>
    /// Method called as a callback when the speech recognition stops
    /// </summary>
    ///
    void OnSpeakStop()
    {
        Debug.Log("OnSpeakStop");
    }
    #endregion

    #region Speech To Text

    /// <summary>
    /// Method called on triggered event (looking at the npc) to start the speech recognition
    /// </summary>
    ///
    public void StartListening()
    {
        input.text = "Listening...";
        SpeechToText.Instance.StartRecording();
        recordingIcon.SetActive(true);
    }

    /// <summary>
    /// Method called on triggered event (not looking at the npc anymore) to stop the speech recognition
    /// </summary>
    ///
    public void StopListening()
    {
        input.text = "Processing...";
        SpeechToText.Instance.StopRecording();
        recordingIcon.SetActive(false);
    }

    /// <summary>
    /// Method called as a callback when the speech engine has processed input as text and is ready to be used
    /// </summary>
    ///
    void OnFinalSpeechResult(string result)
    {        
        input.text = result;
        OpenAI.Instance.Execute(result);
    }

    /// <summary>
    /// Method called as a callback iteratively while the speech engine is processing the input as text
    /// </summary>
    ///
    void OnPartialSpeechResult(string result)
    {
        input.text = result;
    }
    #endregion

    /// <summary>
    /// Method called to setup the STT and TTS engines
    /// </summary>
    ///
    void Setup(string code)
    {
        TextToSpeech.Instance.Setting(code, 1, 1);
        SpeechToText.Instance.Setting(code);
    }   
}
