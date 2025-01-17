using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionDrag : MonoBehaviour , IInteractable, IDragHandler
{
    [SerializeField] private string interactionButtonText = "Move";
    private bool isDraggable = false;
    private GameObject draggingGameobject;
    private RectTransform dragRectTransform;    
    private Canvas canvas;

    public void Interact(GameObject gameObject)
    {
        draggingGameobject = gameObject;
        GetReferences();
        SetSiblingPosition();
        isDraggable = !isDraggable;
    }
    private void Update() 
    {
        
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!isDraggable) return;

       // dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    private void GetReferences()
    {
        dragRectTransform = draggingGameobject.transform.parent.GetComponent<RectTransform>();
        canvas = draggingGameobject.transform.parent.GetComponent<Canvas>();
    }

    private void SetSiblingPosition()
    {
        dragRectTransform.SetAsLastSibling();
        // We set the object that we drag to as the previos to last sibling because the last sibling is the cursor
        dragRectTransform.SetSiblingIndex(dragRectTransform.GetSiblingIndex() - 1);        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
        if (!isDraggable) return;

        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
