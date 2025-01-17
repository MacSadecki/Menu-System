using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadUserSettings : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    InputAction navigateUi;
    private void Awake() 
    {
        navigateUi = playerInput.actions.FindActionMap("UI").FindAction("Navigate");

        LoadControllerSettings();
    }

    // Loads settings for the controller from PlayerPrefs
    private void LoadControllerSettings()
    {
        navigateUi.ApplyParameterOverride("StickDeadzone:max", PlayerPrefs.GetFloat("StickDeadzone:max"));
        navigateUi.ApplyParameterOverride("StickDeadzone:min", PlayerPrefs.GetFloat("StickDeadzone:min"));
    }
}
