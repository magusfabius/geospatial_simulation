using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

[Serializable]
public class ReincarnationData
{
    public int reincarnationCount;
    public string currentState;
}

public class ReincarnationManager : MonoBehaviour
{
    public Text reincarnationText;

    private ReincarnationData data;
    private string databaseURL = "https://your-database-url.com/data.json"; // Replace with your actual database URL

    private void Start()
    {
        StartCoroutine(GetReincarnationData());
    }

    private IEnumerator GetReincarnationData()
    {
        UnityWebRequest request = UnityWebRequest.Get(databaseURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            data = JsonUtility.FromJson<ReincarnationData>(request.downloadHandler.text);
            UpdateReincarnationState();
        }
        else
        {
            Debug.LogError("Failed to retrieve data: " + request.error);
        }
    }

    public void OnDeath()
    {
        data.reincarnationCount++;
        UpdateReincarnationState();

        // Save updated data to the online database
        StartCoroutine(UpdateDatabase());
    }

    private void UpdateReincarnationState()
    {
        if (data.reincarnationCount % 100 == 1)
        {
            data.currentState = "Animal";
        }
        else if (data.reincarnationCount % 100 == 2)
        {
            data.currentState = "Person";
        }
        else if (data.reincarnationCount % 100 == 3)
        {
            data.currentState = "Semi-God";
        }
        else if (data.reincarnationCount % 100 == 4)
        {
            data.currentState = "God";
        }

        reincarnationText.text = "Reincarnation Count: " + data.reincarnationCount + "\nCurrent State: " + data.currentState;
    }

    private IEnumerator UpdateDatabase()
    {
        string json = JsonUtility.ToJson(data);
        UnityWebRequest request = UnityWebRequest.Put(databaseURL, json);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to update database: " + request.error);
        }
    }
}
