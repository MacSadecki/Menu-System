using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableInstantiator : MonoBehaviour
{
        
    // Start loading and instantiating a gameobject from an adressable
    public void LoadAsset(AssetReferenceGameObject reference, GameObject gameObject)
    {      
        // Instantiate a gameobject from passed adressable reference at a set position with a set parent
        reference.InstantiateAsync(gameObject.transform.position, Quaternion.identity, gameObject.transform.parent.gameObject.transform);      
    }
    
    // Release the passed adressable asset from the memory. Used when deleting or disabling an asset contained in an adressable package
    public void ReleaseAsset(GameObject gameObject)
    {
        Addressables.ReleaseInstance(gameObject);       
    }

    // Currently unused, might come in handy when expanding the functionality of handling addressables
    // When instantiation is complete, grab a reference to the instantiated object for later use, eg. asset management
    /*
    GameObject instanceReference;
    private void OnAdressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded) instanceReference = handle.Result; 
        Debug.Log(instanceReference);       
    }
    */
}
