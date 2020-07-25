using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "NPC Actions/sAction", order = 1)]
public class sAction : ScriptableObject
{
    [TextArea(2, 5)]
    public string actionText;

    public int groupID;

    public int groupSequenceID;
}
