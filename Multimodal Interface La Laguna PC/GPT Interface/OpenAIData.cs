namespace UnityLibrary
{
    /// <summary>
    /// This file contains different classes that are used to store the data of the
    /// requests and responses that can be performed and obtained when communicating with the OpenAI web API
    /// </summary>
    /// 

    [System.Serializable]
    public class CompletionRequestData
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
    public class CompletionResponse
    {
        public string id;
        public string @object;
        public int created;
        public string model;
        public Choice[] choices;
        public Usage usage;
    }
    
    [System.Serializable]
    public class ChatRequestData
    {
        public string model;
        public NamelessMessage[] messages;
        public float temperature;
        public int max_tokens;
        public float top_p;
        public float frequency_penalty;
        public float presence_penalty;
        public string[] stop;
    }

    [System.Serializable]
    public class ChatResponse
    {
        public string id;
        public string @object;
        public int created;
        public string model;
        public ChatChoice[] choices;
        public Usage usage;
    }

    [System.Serializable]
    public class ChatChoice
    {
        public string text;
        public int index;
        public NamelessMessage message;
        public object logprobs;
        public string finish_reason;
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

    [System.Serializable]
    
    public class NamelessMessage
    {
        public string role;
        public string content;

        public NamelessMessage(string role_, string content_)
        {
            if (role_ != "user" && role_ != "system" && role_ != "assistant")
            {
                throw new System.ArgumentException("Invalid role: " + role_);
            }
            
            this.role = role_;
            
            this.content = content_;
        }

        public string GetRole()
        {
            return role;
        }

        public void SetRole(string role)
        {
            if (role != "user" && role != "system" && role != "assistant")
            {
                throw new System.ArgumentException("Invalid role: " + role);
            }
            this.role = role;
        }
    }

    public class Message : NamelessMessage
    {
        public string name;
        
        public Message(string role, string content, string name = "") : base(role, content)
        {
            this.name = name;
        }
    }
}