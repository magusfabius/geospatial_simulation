using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        // Logic to mark the quest as complete and maybe reward the player
    }
}

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted;
}
