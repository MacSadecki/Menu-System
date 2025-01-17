using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerInput playerInput;
    private InputActionMap inputMap;
    private InputAction action;

    [Header("Press Interactions")]
    [SerializeField] private UnityEvent pressNorthEvent;
    [SerializeField] private UnityEvent pressSouthEvent;
    [SerializeField] private UnityEvent pressEastEvent;
    [SerializeField] private UnityEvent pressWestEvent;

    [Header("Hold Interactions")]
    [SerializeField] private UnityEvent holdNorthEvent;
    [SerializeField] private UnityEvent holdSouthEvent;
    [SerializeField] private UnityEvent holdEastEvent;
    [SerializeField] private UnityEvent holdWestEvent; 

    bool currentSelected = false;      
    
    void Awake()
    {
        // Cache the ActionMap and Action references for later use
        if (playerInput == null) playerInput = FindFirstObjectByType<PlayerInput>();        
        inputMap = playerInput.actions.FindActionMap("UI");
        action = inputMap.FindAction("Interact");
    }
    
    private void Start() 
    {
        // Return when the action does not contain both interaction types 
        if (!(action.interactions.Contains("Hold") && action.interactions.Contains("Press"))) return;        

        action.performed += context => {
            if (!currentSelected) return;

            if(context.interaction is HoldInteraction) 
            {
                MakeHoldInteraction(context);
                Debug.Log("Stared hold interaction");
            }
            else if(context.interaction is PressInteraction) 
            {
                MakePressInteraction(context);
                Debug.Log("Stared press interaction"); 
            }
        };        
    }

    private void OnDisable()
    {       
        // Remove all listeners to avoid calling unexisting methods
        RemoveListeners();

        // Uncheck the current selection from this gameObject, just in case
        currentSelected = false;   
    }

    #region Interactions

    private void MakePressInteraction(InputAction.CallbackContext context)
    {
        // Run checks before invoking the event to make sure everything is in place   
        if (!currentSelected) return;
        //if(!context.ReadValueAsButton()) return;         
        
        // Determine which button is pressed to invoke the matching interaction
        switch(context.control.name)
        {
            case "buttonNorth":
                pressNorthEvent?.Invoke();
                Debug.Log("Interacted North Press");
                break;

            case "buttonEast":
                pressEastEvent?.Invoke();
                Debug.Log("Interacted East Press");
                break;

            case "buttonSouth":
                pressSouthEvent?.Invoke();
                Debug.Log("Interacted South Press");
                break;

            case "buttonWest":
                pressWestEvent?.Invoke();
                Debug.Log("Interacted West Press");
                break;

            default:
                Debug.LogError("Could not find a button to interact, see the InputActionMap and the corresponding script to check if everything is connected as it should be");
                break;
        } 

    }

    private void MakeHoldInteraction(InputAction.CallbackContext context)
    {
        // Run checks before invoking the event to make sure everything is in place         
        if (!currentSelected) return;       
        //if(!context.ReadValueAsButton()) return; 
        
        // Determine which button is pressed to invoke the matching interaction
        switch(context.control.name)
        {
            case "buttonNorth":
                holdNorthEvent?.Invoke();
                Debug.Log("Interacted North Hold");
                break;

            case "buttonEast":
                holdEastEvent?.Invoke();
                Debug.Log("Interacted East Hold");
                break;

            case "buttonSouth":
                holdSouthEvent?.Invoke();
                Debug.Log("Interacted South Hold");
                break;

            case "buttonWest":
                holdWestEvent?.Invoke();
                Debug.Log("Interacted West Hold");
                break;

            default:
                Debug.LogError("Could not find a button to interact, see the InputActionMap and the corresponding script to check if everything is connected as it should be");
                break;
        }

    }

    #endregion

    #region Event Handling

    // Check if the object is currently selected to make sure the interaction will be with the correct object
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentSelected = false;
    }

    // Removes all listeners from methods
    private void RemoveListeners()
    {
        pressNorthEvent.RemoveAllListeners();
        pressSouthEvent.RemoveAllListeners();
        pressEastEvent.RemoveAllListeners();
        pressWestEvent.RemoveAllListeners();

        holdNorthEvent.RemoveAllListeners();
        holdSouthEvent.RemoveAllListeners();
        holdEastEvent.RemoveAllListeners();
        holdWestEvent.RemoveAllListeners();
    }

    #endregion

}
