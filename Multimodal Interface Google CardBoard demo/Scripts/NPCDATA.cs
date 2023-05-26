using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int MaxTokens = 64;
    public float TopP = 1;
    public float FrequencyPenalty = 0.5f;
    public float PresencePenalty = 0.1f;

    public string[] StopSequences;

    [TextArea(10, 100)]
    public string Description;

    [TextArea(10, 100)]
    public string Examples;
        
}
