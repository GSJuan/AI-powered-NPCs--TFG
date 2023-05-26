using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


//Code based of https://github.com/unitycoder/UnityOpenAIGPT3/blob/main/Assets/Scripts
namespace UnityLibrary
{
    public class OpenAI : MonoBehaviour
    {
        //public string modelName = "text-davinci-003";
        //const string url = "https://api.openai.com/v1/completions";

        public string modelName = "gpt-3.5-turbo";
        const string url = "https://api.openai.com/v1/chat/completions";


        public NPCDATA loadedNpc;

        public static OpenAI Instance;

        public TMP_Text output;

        string apiKey = null;
        bool isRunning = false;

        //Singleton
        void Awake() 
        {
            
            if (Instance == null)
            {
                Instance = this;
                loadedNpc = null;
            }
            else
            {
                throw (new System.Exception("Only one instance of OpenAI is allowed"));
            }
        }
        
        void Start()
        {
            LoadAPIKey();
        }
        void LoadAPIKey()
        {
            // TODO optionally use from env.variable

            apiKey = "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";
            Debug.Log("API key loaded, len= " + apiKey.Length);
        }

        /// <summary>
        /// This method is called by the execution method to prepare the complete prompt given to GPT3
        /// </summary>
        /// 
        string PreparePromt(string input)
        {
            string finalPrompt = loadedNpc.Description + "\n\n" + loadedNpc.Examples + "\n\n" + loadedNpc.PlayerName + ": " + input + "\n" + loadedNpc.CharacterName + ": ";
            return finalPrompt;
        }

        /// <summary>
        /// This method is called by the voice controller when it has finished processing the user's input voice into text
        /// </summary>
        /// 
        public void Execute(string input)
        {
            //avoid running multiple times
            if (isRunning)
            {
                Debug.LogError("Already running");
                return;
            }
                    
            if (apiKey == null || apiKey.Length == 0 || apiKey == "Replace with your api Key")
            {
                Debug.LogError("API key not set");
                return;
            }
            
            isRunning = true;


            /*
            // fill in request data
            string finalPrompt = PreparePromt(input);
            
            CompletionRequestData requestData = new CompletionRequestData()
            {
                model = modelName,
                prompt = finalPrompt,
                temperature = loadedNpc.Temperature,
                max_tokens = loadedNpc.MaxTokens,
                top_p = loadedNpc.TopP,
                frequency_penalty = loadedNpc.FrequencyPenalty,
                presence_penalty = loadedNpc.PresencePenalty,
                stop = loadedNpc.StopSequences
            };
            */

            loadedNpc.history.Add(new NamelessMessage("user", input));
            ChatRequestData requestData = new ChatRequestData()
            {
                model = modelName,
                messages = loadedNpc.history.ToArray(),
                temperature = loadedNpc.Temperature,
                max_tokens = loadedNpc.MaxTokens,
                top_p = loadedNpc.TopP,
                frequency_penalty = loadedNpc.FrequencyPenalty,
                presence_penalty = loadedNpc.PresencePenalty,
                stop = loadedNpc.StopSequences
            };
            
            
            string jsonData = JsonUtility.ToJson(requestData);

            byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

            UnityWebRequest request = UnityWebRequest.Post(url, jsonData);

            request.uploadHandler.Dispose();
            
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);
            request.disposeUploadHandlerOnDispose = true;
            request.disposeDownloadHandlerOnDispose = true;

            UnityWebRequestAsyncOperation async = request.SendWebRequest();

            async.completed += (op) =>
            {
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError(request.error);
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                    
                    // parse the results to get values 
                    ChatResponse responseData = JsonUtility.FromJson<ChatResponse>(request.downloadHandler.text);
                    //CompletionResponse responseData = JsonUtility.FromJson<CompletionResponse>(request.downloadHandler.text);

                    loadedNpc.history.Add(responseData.choices[0].message);
                    string generatedText = responseData.choices[0].message.content;
                    //string generatedText = responseData.choices[0].text.TrimStart('\n').TrimStart('\n'); // sometimes contains 2 empty lines at start?


                    //update the output text GameObject 
                    output.text = generatedText;

                    //call the voice controller to speak the generated text
                    VoiceController.Instance.StartSpeaking(generatedText);
                }
                
                isRunning = false;
                request.Dispose();                
                
            };

        }
    }
}