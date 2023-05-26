using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityLibrary;


public class NPC : MonoBehaviour
{
    public UnityEvent talkEvent;
    public UnityEvent notTalkEvent;
    public NPCDATA data;
    
    /// <summary>
    /// This method is called by the NPC collider when the player is next to it
    /// </summary>
    /// 
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //we load this npc's data into the openai controller
            OpenAI.Instance.loadedNpc = data;

            //we start to listen
            talkEvent?.Invoke();
        }
        
    }

    /// <summary>
    /// This method is called by the NPC collider when the player is away
    /// </summary>
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //we stop listening for user's speech input
            notTalkEvent?.Invoke();
        }
    }
}
