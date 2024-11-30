using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private PlayerInput playerInput;
    private InputActionMap inputMap;

    [Header("Interactions")]
    [SerializeField]
    private UnityEvent interactionNorthEvent;
    [SerializeField]
    private UnityEvent interactionSouthEvent;
    [SerializeField]
    private UnityEvent interactionEastEvent;
    [SerializeField]
    private UnityEvent interactionWestEvent;

    bool currentSelected = false;

    // This whole connection to the input system is messier than I would like it to be, but it works so for now I'm leaving it as it is
    void Awake()
    {
        inputMap = playerInput.actions.FindActionMap("UI");        
    }
    
    private void OnEnable() 
    {
        inputMap.FindAction("InteractNorth").performed += InteractNorth;
        inputMap.FindAction("InteractSouth").performed += InteractSouth;
        inputMap.FindAction("InteractEast").performed += InteractEast;
        inputMap.FindAction("InteractWest").performed += InteractWest;
    }

    private void OnDisable() 
    {
        inputMap.FindAction("InteractNorth").performed -= InteractNorth;
        inputMap.FindAction("InteractSouth").performed -= InteractSouth;
        inputMap.FindAction("InteractEast").performed -= InteractEast;
        inputMap.FindAction("InteractWest").performed -= InteractWest;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentSelected = false;
    }

    // This whole part needs to be optimized and in best case scenario reduced to one singular method
    private void InteractNorth(InputAction.CallbackContext context)
    {   
        // Run checks before invoking the event to make sure everything is in place     
        if (!currentSelected) return;
        if(!context.ReadValueAsButton()) return;
        if (interactionNorthEvent.GetPersistentEventCount() < 1) 
        {   
            Debug.LogError("Missing listeners on the event that you're tying to invoke. Please add listeners in the inspector or disable this interaction.");
            return;
        }
        
        
        interactionNorthEvent?.Invoke();
        Debug.Log("Interacted North");
    }

    private void InteractSouth(InputAction.CallbackContext context)
    {        
        if (!currentSelected) return;
        if(!context.ReadValueAsButton()) return;
        if (interactionSouthEvent.GetPersistentEventCount() < 1) 
        {   
            Debug.LogError("Missing listeners on the event that you're tying to invoke. Please add listeners in the inspector or disable this interaction.");
            return;
        }
        
       
        interactionSouthEvent?.Invoke();
        Debug.Log("Interacted South");
    }

    private void InteractEast(InputAction.CallbackContext context)
    {        
        if (!currentSelected) return;
        if(!context.ReadValueAsButton()) return;
        if (interactionEastEvent.GetPersistentEventCount() < 1) 
        {   
            Debug.LogError("Missing listeners on the event that you're tying to invoke. Please add listeners in the inspector or disable this interaction.");
            return;
        }

        interactionEastEvent?.Invoke();
        Debug.Log("Interacted East");
    }

    private void InteractWest(InputAction.CallbackContext context)
    {        
        if (!currentSelected) return;
        if(!context.ReadValueAsButton()) return;
        if (interactionWestEvent.GetPersistentEventCount() < 1) 
        {   
            Debug.LogError("Missing listeners on the event that you're tying to invoke. Please add listeners in the inspector or disable this interaction.");
            return;
        }

        interactionWestEvent?.Invoke();
        Debug.Log("Interacted West");
    }



}
