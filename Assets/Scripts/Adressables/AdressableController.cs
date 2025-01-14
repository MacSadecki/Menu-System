using UnityEngine;
using UnityEngine.AddressableAssets;

public class AdressableController : MonoBehaviour
{
    [SerializeField]
    private bool isAllowedToInstantiateAssets = false;
    [SerializeField]
    private bool isAllowedToDestuctAssets = false;
    [SerializeField]
    AssetReferenceGameObject evidenceWindowReference;
    [SerializeField]
    AdressableInstantiator instantiator;
    GameObject instanceReference;

    
    private void Awake() 
    {
        // If a reference to the instantiator is missing, try go grab it via code
        if (instantiator == null) FindFirstObjectByType<AdressableInstantiator>();
    }

    // Call the instantiator method with parameters to instantiate object and pass a gameobject to set a parent and coordinates
    public void InstantiateAsset(GameObject passedGameObject)
    {
        if (!isAllowedToInstantiateAssets)
        {
            Debug.LogWarning("This controller is not allowed to instantiate assets! Check is everything is set up correctly!");
            return;
        }         
        instantiator.LoadAsset(evidenceWindowReference, passedGameObject);
    }

    // Work in progress
    // Delete a gameobject and call the instantiator to release the adressable asset from the memory
    public void DestructAsset()
    {
        if (!isAllowedToDestuctAssets)
        {
            Debug.LogWarning("This controller is not allowed to destruct assets! Check is everything is set up correctly!");
            return;
        }    
        //instantiator.ReleaseAsset()
    }
}
