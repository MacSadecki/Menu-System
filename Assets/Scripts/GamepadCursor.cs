using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private float padding = 50f;

    private bool previousMouseState;
    private Mouse virtualMouse;
    private Mouse currentMouse;
    private Camera mainCamera;

    private string previousControlScheme = "";
    private const string gamepadScheme = "Gamepad";
    private const string mouseScheme = "Keyboard&Mouse";

    private void OnEnable() 
    {
        mainCamera = Camera.main;
        currentMouse = Mouse.current;

        // We check if vistual mouse is present
        if(virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if(!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        // We connect virutal mouse to the player input so the player can controll the virtual mouse
        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        // We set the virutal mouse position to the position of the cursor gameobject so the two would match
        if(cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
        playerInput.onControlsChanged += OnControlsChanged;
    }


    private void OnDisable() 
    {
        if(virtualMouse != null && virtualMouse.added) InputSystem.onAfterUpdate -= UpdateMotion;
        playerInput.onControlsChanged -= OnControlsChanged;

        // We delete the virtual mouse here, otherwise virtual mouses would stockpile in devices
        InputSystem.RemoveDevice(virtualMouse);
    }

    private void UpdateMotion()
    {
        // We check if virtual mouse or gamepad is present so the function would not run and potentialy break other stuff
        if(virtualMouse == null || Gamepad.current == null) return;

        Debug.Log(playerInput.actions.FindActionMap("UI").FindAction("Navigate").ReadValue<Vector2>());
        // We make the cursor movement framerate independent
        Vector2 deltaValue = playerInput.actions.FindActionMap("UI").FindAction("Navigate").ReadValue<Vector2>();
        //Vector2 deltaValue = Gamepad.current.leftStick.ReadValue(); // To take the value straight from the gamepad
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        // We make sure so the cursor doesn't go off screen so the player wouldn't lose it
        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        // We make a state check so the input system would react properly to the virtual mouse click input
        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if(previousMouseState != aButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }        

        AnchorCursor(newPosition);
    }

    // Set actual cursor image to move in conjunction with the virutal mouse
    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition; 
    }


    // When changing the controller from mouse to gamepad and the other way around, we match the position of the gamepad cursos and mouse cursor so the transition would be seamless
    private void OnControlsChanged(PlayerInput input)
    {
        if(playerInput.currentControlScheme == mouseScheme && previousControlScheme != mouseScheme)
        {
            cursorTransform.gameObject.SetActive(false);
            Cursor.visible = true;
            currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
            previousControlScheme = mouseScheme;
        }
        else if(playerInput.currentControlScheme == gamepadScheme && previousControlScheme != gamepadScheme)
        {
            cursorTransform.gameObject.SetActive(true);
            Cursor.visible = false;
            InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
            AnchorCursor(currentMouse.position.ReadValue());
            previousControlScheme = gamepadScheme;
        }
    }

}
