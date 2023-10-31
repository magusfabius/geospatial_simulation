using UnityEngine;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public string playerName = "DefaultPlayer";
    public int reincarnationLevel = 0;

    public void CreateNewAccount(string newPlayerName)
    {
        playerName = newPlayerName;

        PlayerData playerData = new PlayerData
        {
            Name = playerName,
            ReincarnationLevel = reincarnationLevel
        };

        string json = JsonUtility.ToJson(playerData);
        string fileName = $"{playerName}_Soul_{reincarnationLevel}.json";
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json);
    }

    [System.Serializable]
    public class PlayerData
    {
        public string Name;
        public int ReincarnationLevel;
    }
}
