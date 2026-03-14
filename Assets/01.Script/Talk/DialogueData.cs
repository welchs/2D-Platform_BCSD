using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Game/Dialogue")]
public class DialogueData : ScriptableObject
{
    public List<DialogueState> states;
}

[System.Serializable]
public class DialogueState
{
    public List<string> lines;
}
