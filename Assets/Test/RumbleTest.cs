using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleTest : MonoBehaviour
{    
    private float rumbleIntensity = 0.25f;
    void OnEnable()
    {
        InputManager.instance.playerInput.actions.FindActionMap("Menus").FindAction("Cancel").performed += context => Rumble();
    }

    void OnDisable()
    {
        InputManager.instance.playerInput.actions.FindActionMap("Menus").FindAction("Cancel").performed -= context => Rumble();
    }

    public void Rumble()    
    {
        if (PlayerPrefs.HasKey("VibrationIntensity") == true) rumbleIntensity = PlayerPrefs.GetFloat("VibrationIntensity");
        RumbleManager.Instance.RumblePulse(rumbleIntensity, rumbleIntensity, 0.25f);
    }
}
