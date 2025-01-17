using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;

    private void Awake() 
    {
        // We get the recttransform in case it was not setup in the inspector
        if (dragRectTransform == null)
        {
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        // We get the canvas in case it was not setup in the inspector
        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }
        
        SetSiblingPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetSiblingPosition();
    }

    private void SetSiblingPosition()
    {
        dragRectTransform.SetAsLastSibling();
        // We set the object that we drag to as the previos to last sibling because the last sibling is the cursor
        dragRectTransform.SetSiblingIndex(dragRectTransform.GetSiblingIndex() - 1);        
    }
}
