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
    
    void Awake()
    {
        inputMap = playerInput.actions.FindActionMap("UI");        
    }

    // Connect the interaction method with an Action
    private void OnEnable() 
    {
        inputMap.FindAction("Interact").performed += MakeInteraction;
    }

    private void OnDisable() 
    {
        inputMap.FindAction("Interact").performed -= MakeInteraction;
    }

    // Check if the object is currently selected to make sure the interaction will be with the correct object
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentSelected = false;
    }

    private void MakeInteraction(InputAction.CallbackContext context)
    {
        // Run checks before invoking the event to make sure everything is in place    
        if (!currentSelected) return;
        if(!context.ReadValueAsButton()) return;
        
        // Determine which button is pressed to invoke the matching interaction
        switch(context.control.name)
        {
            case "buttonNorth":
                interactionNorthEvent?.Invoke();
                Debug.Log("Interacted North");
                break;

            case "buttonEast":
                interactionEastEvent?.Invoke();
                Debug.Log("Interacted East");
                break;

            case "buttonSouth":
                interactionSouthEvent?.Invoke();
                Debug.Log("Interacted South");
                break;

            case "buttonWest":
                interactionWestEvent?.Invoke();
                Debug.Log("Interacted West");
                break;

            default:
                Debug.LogError("Could not find a button to interact, see the InputActionMap and the corresponding script to check if everything is connected as it should be");
                break;
        } 

    }

}
