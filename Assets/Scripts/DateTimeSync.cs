using System;
using UnityEngine;
using UnityEngine.UI;

public class DateTimeUpdater : MonoBehaviour
{
    public Text dateTimeText; // Reference to the Text UI component

    private void Update()
    {
        // Get current date and time
        DateTime currentDateTime = DateTime.Now;

        // Update the Text UI component with the current date and time
        dateTimeText.text = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
