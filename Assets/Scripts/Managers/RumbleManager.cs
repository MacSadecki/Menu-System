using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager Instance;

    private Gamepad pad;

    private Coroutine stopRumbleAfterTimeCoroutine;

    private string currentControlScheme;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }        
    }

    private void Start() 
    {
        InputManager.instance.playerInput.onControlsChanged += SwitchControls;
    }

    private void OnDisable() 
    {
        InputManager.instance.playerInput.onControlsChanged -= SwitchControls;
    }


    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        // Check if current active control scheme is gamepad
        if (currentControlScheme != "Gamepad") return;

        // Get reference to the gamepad
        pad = Gamepad.current;

        // If a gamepad is present
        if (pad != null)
        {
            // Start the rumble
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            // Stop the rumble after specified time passage
            stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration, pad));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0, 0);
    }

    private void SwitchControls(PlayerInput input)
    {
        //Debug.Log("Device is now: " + input.currentControlScheme);
        currentControlScheme = input.currentControlScheme;
    }
}
