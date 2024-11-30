using System.Collections.Generic;
using UnityEngine;

public class InteractionOpen : MonoBehaviour, IInteractable
{
    public string interactionButtonText = "Open";
    [SerializeField]
    public GameObject instantiatee;    
    public void Interact(GameObject gameObject)
    {   
        Instantiate(instantiatee, gameObject.GetComponent<RectTransform>().position, Quaternion.identity, gameObject.GetComponentInParent<Canvas>().transform);        
    }

    public void SetInstantiatee(GameObject objectToOpen)
    {
        instantiatee = objectToOpen;
    }

}
