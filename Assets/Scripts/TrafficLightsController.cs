using System.Collections;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public TrafficLight[] trafficLights; // Array of TrafficLight objects

    private void Start()
    {
        // Start the coroutine to control the traffic lights
        StartCoroutine(ControlTrafficLights());
    }

    private IEnumerator ControlTrafficLights()
    {
        while (true)
        {
            foreach (var trafficLight in trafficLights)
            {
                // Turn the current light green for 10 seconds
                trafficLight.SetLightState(TrafficLight.LightState.Green);
                yield return new WaitForSeconds(10);

                // Turn the current light yellow for 3 seconds
                trafficLight.SetLightState(TrafficLight.LightState.Yellow);
                yield return new WaitForSeconds(3);

                // Turn the current light red
                trafficLight.SetLightState(TrafficLight.LightState.Red);
            }
        }
    }
}

public class TrafficLight : MonoBehaviour
{
    public enum LightState
    {
        Red,
        Green,
        Yellow
    }

    private LightState currentLightState;

    public void SetLightState(LightState newState)
    {
        currentLightState = newState;
        UpdateTrafficLightColor();
    }

    private void UpdateTrafficLightColor()
    {
        Color colorToSet = Color.red;

        switch (currentLightState)
        {
            case LightState.Green:
                colorToSet = Color.green;
                break;
            case LightState.Yellow:
                colorToSet = Color.yellow;
                break;
        }

        // Assuming the traffic light object has a Renderer component
        // Change the material color to the specified color
        GetComponent<Renderer>().material.color = colorToSet;
    }
}
