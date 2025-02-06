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

    [Header("Controller Axis")]
    [SerializeField] private bool isXAxisInverted = false;
    [SerializeField] private Toggle xAxisToggle;
    [SerializeField] private bool isYAxisInverted = false;
    [SerializeField] private Toggle yAxisToggle;

    [Header("Controller Vibration")]
    [SerializeField] private float vibrationIntensity = 0.10f;
    [SerializeField] private Slider vibrationSlider;
    [SerializeField] private TextMeshProUGUI vibrationText;

    [Header("Sound")]
    [SerializeField] private SoundMixerManager soundMixerManager;
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TextMeshProUGUI masterText;
    [SerializeField] private float soundFXVolume = 1f;
    [SerializeField] private Slider soundFXSlider;
    [SerializeField] private TextMeshProUGUI soundFXText;
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;

    
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

       // Mixer settings souldn't be called in OnEnable, this is specified by Unity itself
       LoadData();
    }
    
    
    // Call data loading at enable
    private void OnEnable() 
    {
       // LoadData();
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
        UIChangeXInversion(isXAxisInverted);
        UIChangeYInversion(isYAxisInverted);
        UIChangeVibrationIntensity(vibrationIntensity);
        UIChangeMainVolume(masterVolume);
        UIChangeSoundFXVolume(soundFXVolume);
        UIChangeMusicVolume(musicVolume);
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

    public void UIChangeXInversion(bool value)
    {
        isXAxisInverted = value;
        xAxisToggle.isOn = value;        
    }

    public void UIChangeYInversion(bool value)
    {
        isYAxisInverted = value;
        yAxisToggle.isOn = value;  
    }

    public void UIChangeVibrationIntensity(float value)
    {
        vibrationIntensity = value;
        UpdateSlider(vibrationSlider, vibrationIntensity, vibrationText);        
    }

    public void UIChangeMainVolume(float value)
    {
        masterVolume = value;
        soundMixerManager.SetMasterVolume(masterVolume);
        UpdateSlider(masterSlider, masterVolume, masterText);        
    }
    public void UIChangeSoundFXVolume(float value)
    {
        soundFXVolume = value;
        soundMixerManager.SetSoundFXVolume(soundFXVolume);
        UpdateSlider(soundFXSlider, soundFXVolume, soundFXText);        
    }
    public void UIChangeMusicVolume(float value)
    {
        musicVolume = value;
        soundMixerManager.SetMusicVolume(musicVolume);
        UpdateSlider(musicSlider, musicVolume, musicText);        
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

        // Axis X Inversion
        if (PlayerPrefs.HasKey("InvertVector2:invertX") == true) isXAxisInverted = PlayerPrefs.GetInt("InvertVector2:invertX") == 1; 

        // Axis Y Inversion
        if (PlayerPrefs.HasKey("InvertVector2:invertY") == true) isYAxisInverted = PlayerPrefs.GetInt("InvertVector2:invertY") == 1; 

        // Vibration
        if (PlayerPrefs.HasKey("VibrationIntensity") == true) vibrationIntensity = PlayerPrefs.GetFloat("VibrationIntensity");

        // MainVolume
        if (PlayerPrefs.HasKey("masterVolume") == true) masterVolume = PlayerPrefs.GetFloat("masterVolume");                

        // SoundFXVolume
        if (PlayerPrefs.HasKey("soundFXVolume") == true) soundFXVolume = PlayerPrefs.GetFloat("soundFXVolume");

        // MusicVolume
        if (PlayerPrefs.HasKey("musicVolume") == true) musicVolume = PlayerPrefs.GetFloat("musicVolume");

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
        navigateUi.ApplyParameterOverride("InvertVector2:invertX", isXAxisInverted);
        navigateUi.ApplyParameterOverride("InvertVector2:invertY", isYAxisInverted);
    }

    // Save parameters to PlayerPrefs for storage and future loading
    private void SaveToPrefs()
    {
        PlayerPrefs.SetFloat("StickDeadzone:max", deadzoneMax);
        PlayerPrefs.SetFloat("StickDeadzone:min", deadzoneMin);        
        PlayerPrefs.SetInt("InvertVector2:invertX", isXAxisInverted ? 1 : 0);
        PlayerPrefs.SetInt("InvertVector2:invertY", isYAxisInverted ? 1 : 0);
        PlayerPrefs.SetFloat("VibrationIntensity", vibrationIntensity);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("soundFXVolume", soundFXVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);

        PlayerPrefs.Save();      
    } 

    #endregion

}
