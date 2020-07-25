using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    // set up sequenced actions and their variations manually in the editor
    [SerializeField] private List<sActionGroups> actionGroups = new List<sActionGroups>();

    List<sAction> currentActionGroup = new List<sAction>();

    [SerializeField] private int currentGroup = 1;
    [SerializeField] private int currentSequenceGroup = 1;

    // grab the next relevant action in the sequence with slight randomization within the sequence  
    public string GrabNextAction()
    {
        // if there are no more sequences left in the action group, move to the next action group
        if (currentSequenceGroup > actionGroups[currentGroup].sequences)
        {
            currentGroup++;

            if (currentGroup >= actionGroups.Count)
            {
                currentGroup = 0;
            }

            currentSequenceGroup = 1;
        }


        // find the relative action group
        currentActionGroup = actionGroups[currentGroup].actions;

        // sort through the current action group, and set aside the appropriate sequence groups
        List<sAction> chosenActions = new List<sAction>();

        foreach (sAction action in currentActionGroup)
        {
            if (action.groupSequenceID == currentSequenceGroup)
            {
                chosenActions.Add(action);
            }
        }

        // select a random action group 
        int randomIndex = Random.Range(0, chosenActions.Count);

        string output = chosenActions[randomIndex].actionText;

        // increase the sequence group number
        currentSequenceGroup++;

        // TODO: how can I detect when I am at the end of the groups sequences, and then move to the next group?

        return output;
    }
}
