using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Player Input")]    
    [SerializeField]
    PlayerInput playerInput;
    InputAction navigateUi;

    [Header("Controller Deadzone")]
    [SerializeField] private float deadzoneMin = 0.10f;
    [SerializeField] private Slider deadzoneMinSlider;
    [SerializeField] private TextMeshProUGUI deadzoneMinText;
    [SerializeField] private float deadzoneMax = 0.90f;
    [SerializeField] private Slider deadzoneMaxSlider;
    [SerializeField] private TextMeshProUGUI deadzoneMaxText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   // Cache references
        navigateUi = playerInput.actions.FindActionMap("Gameplay").FindAction("Navigate");

        // Check for playerprefs settings
        CheckPlayerPrefs();

        // Set values and update visuals on launch
        ChangeDeadzoneMax(deadzoneMax);
        ChangeDeadzoneMin(deadzoneMin);
    }

    private void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("StickDeadzone:max") == false) PlayerPrefs.SetFloat("StickDeadzone:max", deadzoneMax);
        else deadzoneMax = PlayerPrefs.GetFloat("StickDeadzone:max");        
        if (PlayerPrefs.HasKey("StickDeadzone:min") == false) PlayerPrefs.SetFloat("StickDeadzone:min", deadzoneMin);
        else deadzoneMin = PlayerPrefs.GetFloat("StickDeadzone:min");        
    }
    
    public void ChangeDeadzoneMax(float value)
    {
        deadzoneMax = value;
        navigateUi.ApplyParameterOverride("StickDeadzone:max", deadzoneMax);
        UpdateSlider(deadzoneMaxSlider, deadzoneMax, deadzoneMaxText);
        PlayerPrefs.SetFloat("StickDeadzone:max", deadzoneMax);
    }

    public void ChangeDeadzoneMin(float value)
    {
        deadzoneMin = value;
        navigateUi.ApplyParameterOverride("StickDeadzone:min", deadzoneMin);
        UpdateSlider(deadzoneMinSlider, deadzoneMin, deadzoneMinText);
        PlayerPrefs.SetFloat("StickDeadzone:min", deadzoneMin);
    }

    private void UpdateSlider(Slider slider, float value, TextMeshProUGUI text)
    {
        slider.value = value;
        text.text = value.ToString("0.00");
    }


}
