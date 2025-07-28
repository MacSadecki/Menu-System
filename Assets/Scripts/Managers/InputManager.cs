using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    //[HideInInspector] public Controls controls;
    [HideInInspector] public PlayerInput playerInput;
    
    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }

        //controls = new Controls();  

        playerInput = GetComponent<PlayerInput>();     
    }

    private void OnEnable() 
    {
        playerInput.enabled = true;
    }

    private void OnDisable() 
    {
        playerInput.enabled = false;
    }

}
