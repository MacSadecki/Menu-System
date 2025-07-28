using Unity.Mathematics;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Instantiate the Gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the audioclip
        audioSource.clip = audioClip;

        // Assign Volume
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Get soundclip length
        float clipLength = audioSource.clip.length;

        // Destroy soundclip after being played
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        // Assign a random index
        int rand = UnityEngine.Random.Range(0, audioClip.Length);

        // Instantiate the Gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the audioclip
        audioSource.clip = audioClip[rand];

        // Assign Volume
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Get soundclip length
        float clipLength = audioSource.clip.length;

        // Destroy soundclip after being played
        Destroy(audioSource.gameObject, clipLength);
    }
}
