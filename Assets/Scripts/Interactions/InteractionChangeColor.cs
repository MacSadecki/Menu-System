using UnityEngine;
using UnityEngine.UI;

public class InteractionChangeColor : MonoBehaviour, IInteractable
{    
    public string interactionButtonText = "Change color";

    [SerializeField]
    private Color[] colors;
    private int currentColorIndex = 0;

    public void Interact(GameObject gameObject)
    {
        // Check if colors array has something in it, otherwise there would not be any colors to pick from
        if (colors.Length == 0)
        {
            Debug.LogError("The colors array is empty, please add at least one color to the array");
            return;
        }
        // Check if currentColorIndex is not exceeding the array size so the whole thing would not explode
        if (currentColorIndex >= colors.Length) currentColorIndex = 0;

        gameObject.GetComponent<Image>().color = colors[currentColorIndex];        
        
        currentColorIndex ++;
    }
}
