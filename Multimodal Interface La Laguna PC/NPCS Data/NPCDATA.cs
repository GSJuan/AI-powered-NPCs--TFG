using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLibrary{
    

    /// <summary>
    /// This is de definition class of the NPC scriptable Object, containing all the information related to a NPC
    /// </summary>
    /// 

    [CreateAssetMenu(fileName = "New NPC", menuName = "ScriptableObjects/NPC")]
    public class NPCDATA : ScriptableObject
    {
        public string PlayerName;
        public string CharacterName;
        public float Temperature = 0.7f;
        public int MaxTokens = 128;
        public float TopP = 1;
        public float FrequencyPenalty = 0.5f;
        public float PresencePenalty = 0.1f;

        //private bool initialized = false;

        public List<NamelessMessage> history = new List<NamelessMessage>();

        public string[] StopSequences;

        [TextArea(10, 100)]
        public string Description;

        [TextArea(10, 100)]
        public string Examples;
        
        public void OnEnable()
        {
            //if (Examples != null && Description != null && initialized == false)
            //{
                Initialize();                
            //}
        }

        public void OnDestroy()
        {
            history.Clear();
        }

        void Initialize()
        {
            history = new List<NamelessMessage>();
            history.Add(new NamelessMessage("system", Description));
            //LoadExamples();
            //initialized = true;
        }

        void LoadExamples()
        {
            string role = "";
            string name = "";
            
            string[] examples = Examples.Split(StopSequences[0]);
            foreach (string example in examples)
            {

                if(example.Length == 0)
                {
                    continue;
                }
                                
                string[] parts = example.Split(":");

                string speaker = parts[0].Trim('\n');              

                if (speaker == PlayerName)
                {
                    role = "user";
                    name = PlayerName;
                }
                else if (speaker == CharacterName)
                {
                    role = "assistant";
                    name = CharacterName;
                }
                
                //Debug.Log(role + ": " + parts[1] + " (" + name + ");");
                history.Add(new Message(role, parts[1].Trim('\n'), name));
            }

        }  
        
    }

}