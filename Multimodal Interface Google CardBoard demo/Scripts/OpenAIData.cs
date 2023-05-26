namespace UnityLibrary
{
    /// <summary>
    /// This file contains different classes that are used to store the data of the
    /// requests and responses that can be performed and obtained when communicating with the OpenAI web API
    /// </summary>
    /// 

    [System.Serializable]
    public class RequestData
    {
        public string model;
        public string prompt;
        public float temperature;
        public int max_tokens;
        public float top_p;
        public float frequency_penalty;
        public float presence_penalty;
        public string[] stop;
    }

    [System.Serializable]
    public class OpenAIAPI
    {
        public string id;
        public string @object;
        public int created;
        public string model;
        public Choice[] choices;
        public Usage usage;
    }

    [System.Serializable]
    public class Choice
    {
        public string text;
        public int index;
        public object logprobs;
        public string finish_reason;
    }

    [System.Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }
}