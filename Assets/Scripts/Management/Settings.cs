using System;
using TMPro;
using UnityEditor.Overlays;
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

    [Header("Controller Vibration")]
    [SerializeField] private float vibrationIntensity = 0.10f;
    [SerializeField] private Slider vibrationSlider;
    [SerializeField] private TextMeshProUGUI vibrationText;
    
    #region Management

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {   // Cache references
        navigateUi = playerInput.actions.FindActionMap("Gameplay").FindAction("Navigate");

        // Check for playerprefs settings
        //CheckPlayerPrefs();

        // Set values and update visuals on launch
       // UIChangeDeadzoneMax(deadzoneMax);
       // UIChangeDeadzoneMin(deadzoneMin);
    }
    
    
    // Call data loading at enable
    private void OnEnable() 
    {
        LoadData();
    }

    // Call data saving when disabling
    private void OnDisable()
    {
        SaveData();
    }

    #endregion



    #region UI Handling

    private void UpdateUI()
    {
        UIChangeDeadzoneMax(deadzoneMax); 
        UIChangeDeadzoneMin(deadzoneMin);
        UIChangeVibrationIntensity(vibrationIntensity);
    }

    public void UIChangeDeadzoneMax(float value)
    {
        deadzoneMax = value;        
        UpdateSlider(deadzoneMaxSlider, deadzoneMax, deadzoneMaxText);        
    }

    public void UIChangeDeadzoneMin(float value)
    {
        deadzoneMin = value;        
        UpdateSlider(deadzoneMinSlider, deadzoneMin, deadzoneMinText);        
    }

    public void UIChangeVibrationIntensity(float value)
    {
        vibrationIntensity = value;
        UpdateSlider(vibrationSlider, vibrationIntensity, vibrationText);        
    }

    private void UpdateSlider(Slider slider, float value, TextMeshProUGUI text)
    {
        slider.value = value;
        text.text = value.ToString("0.00");
    }

    #endregion

    

    #region Saving and Loading

    // ----- Loading -----
    
    private void LoadData()
    {
        CheckPlayerPrefs();        
    }

    private void CheckPlayerPrefs()
    {
        // Deadzone Max
        if (PlayerPrefs.HasKey("StickDeadzone:max") == true) deadzoneMax = PlayerPrefs.GetFloat("StickDeadzone:max");         

        // Deadzone Min       
        if (PlayerPrefs.HasKey("StickDeadzone:min") == true) deadzoneMin = PlayerPrefs.GetFloat("StickDeadzone:min");        

        // Vibration
        if (PlayerPrefs.HasKey("VibrationIntensity") == true) vibrationIntensity = PlayerPrefs.GetFloat("VibrationIntensity");

        UpdateUI();
    } 

    // ----- Saving -----

    private void SaveData()
    {
        ApplyParameters();
        SaveToPrefs();
    }

    // Apply paramenets to the scene and objects
    private void ApplyParameters()
    {
        navigateUi.ApplyParameterOverride("StickDeadzone:max", deadzoneMax);
        navigateUi.ApplyParameterOverride("StickDeadzone:min", deadzoneMin);
    }

    // Save parameters to PlayerPrefs for storage and future loading
    private void SaveToPrefs()
    {
        PlayerPrefs.SetFloat("StickDeadzone:max", deadzoneMax);
        PlayerPrefs.SetFloat("StickDeadzone:min", deadzoneMin);
        PlayerPrefs.SetFloat("VibrationIntensity", vibrationIntensity);        
    } 

    #endregion

}
