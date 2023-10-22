using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public PlayerController player;
    public QuestManager questManager;
    public Text questText;
    public Text coordinatesText;

    void Update()
    {
        UpdateQuestDisplay();
        UpdateCoordinatesDisplay();
    }

    void UpdateQuestDisplay()
    {
        if (questManager.activeQuests.Count > 0)
        {
            Quest currentQuest = questManager.activeQuests[0]; // Assuming the first quest in the list is the current quest
            questText.text = "Quest: " + currentQuest.questName;
        }
        else
        {
            questText.text = "No active quest.";
        }
    }

    void UpdateCoordinatesDisplay()
    {
        Vector3 playerPosition = player.transform.position;
        coordinatesText.text = "Coordinates: " + playerPosition.x.ToString("F2") + ", " + playerPosition.y.ToString("F2") + ", " + playerPosition.z.ToString("F2");
    }
}
