using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Action Groups", order = 1)]
public class sActionGroups : ScriptableObject
{
    public List<sAction> actions = new List<sAction>();

    public int sequences;
}
