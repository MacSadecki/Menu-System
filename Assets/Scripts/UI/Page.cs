using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class Page : MonoBehaviour
{
    private AudioSource audioSource;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject firstFocusItem;
    private GameObject lastFocusItem = null;
    [SerializeField]
    private float animationSpeed = 1f;
    public bool exitOnNewPagePush = false;
    [SerializeField]
    private AudioClip entryClip;
    [SerializeField]
    private AudioClip exitClip;
    [SerializeField]
    private EntryMode entryMode = EntryMode.Do_nothing;
    [SerializeField]
    private Direction entryDirection = Direction.None;
    [SerializeField]
    private EntryMode exitMode = EntryMode.Do_nothing;
    [SerializeField]
    private Direction exitDirection = Direction.None;
    [SerializeField]
    private UnityEvent prePushAction;
    [SerializeField]
    private UnityEvent postPushAction;
    [SerializeField]
    private UnityEvent prePopAction;
    [SerializeField]
    private UnityEvent postPopAction;

    

    private Coroutine animationCoroutine;
    private Coroutine audioCoroutine;

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();

        //Prepare the audio source for later use
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0;
        audioSource.enabled = false;
    }

    // Call when page enters the player view
    public void Enter(bool playAudio)
    {
        // Call other actions that should happen before page enter
        prePushAction?.Invoke();

        // Play coresponding animation
        switch(entryMode)
        {
            case EntryMode.Slide:
                SlideIn(playAudio);
                break;
            case EntryMode.Zoom:
                ZoomIn(playAudio);
                break;
            case EntryMode.Fade:
                FadeIn(playAudio);
                break;
        }
    }

    // Call when pages exits the player view
    public void Exit(bool playAudio)
    {
        // Call other actions that should happen before page exit
        prePopAction?.Invoke();

        // Play coresponding animation
        switch(exitMode)
        {
            case EntryMode.Slide:
                SlideOut(playAudio);
                break;
            case EntryMode.Zoom:
                ZoomOut(playAudio);
                break;
            case EntryMode.Fade:
                FadeOut(playAudio);
                break;
        }
    }

    #region Animations
    private void SlideIn(bool playAudio)
    {
        CheckIfAnimationCoroutineIsRunning();

        animationCoroutine = StartCoroutine(AnimationHelper.SlideIn(rectTransform, entryDirection, animationSpeed, postPushAction));

        PlayEntryClip(playAudio);
    }

    private void SlideOut(bool playAudio)
    {
        CheckIfAnimationCoroutineIsRunning();

        animationCoroutine = StartCoroutine(AnimationHelper.SlideOut(rectTransform, exitDirection, animationSpeed, postPopAction));

        PlayExitClip(playAudio);
    }

    private void ZoomIn(bool playAudio)
    {
        CheckIfAnimationCoroutineIsRunning();

        animationCoroutine = StartCoroutine(AnimationHelper.ZoomIn(rectTransform, animationSpeed, postPushAction));

        PlayEntryClip(playAudio);
    }

    private void ZoomOut(bool playAudio)
    {
        CheckIfAnimationCoroutineIsRunning();

        animationCoroutine = StartCoroutine(AnimationHelper.ZoomOut(rectTransform, animationSpeed, postPopAction));

        PlayExitClip(playAudio);
    }

    private void FadeIn(bool playAudio)
    {
        CheckIfAnimationCoroutineIsRunning();

        animationCoroutine = StartCoroutine(AnimationHelper.FadeIn(canvasGroup, animationSpeed, postPushAction));

        PlayEntryClip(playAudio);
    }

    private void FadeOut(bool playAudio)
    {
        CheckIfAnimationCoroutineIsRunning();

        animationCoroutine = StartCoroutine(AnimationHelper.FadeOut(canvasGroup, animationSpeed, postPopAction));

        PlayExitClip(playAudio);
    }

    private void CheckIfAnimationCoroutineIsRunning()
    {
        if(animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
    }

    #endregion 

    #region Audio

    private void PlayEntryClip(bool playAudio)
    {
        if(playAudio && entryClip != null && audioSource != null)
        {
            if(audioCoroutine != null)
            {
                StopCoroutine(audioCoroutine);
            }
        }

        audioCoroutine = StartCoroutine(PlayClip(entryClip));
    }

    private void PlayExitClip(bool playAudio)
    {
        if(playAudio && exitClip != null && audioSource != null)
        {
            if(audioCoroutine != null)
            {
                StopCoroutine(audioCoroutine);
            }
        }

        audioCoroutine = StartCoroutine(PlayClip(exitClip));
    }

    #endregion Audio

    // Pass the audio clip to play it
    private IEnumerator PlayClip(AudioClip clip)
    {
        audioSource.enabled = true;

        WaitForSeconds wait = new WaitForSeconds(clip.length);

        audioSource.PlayOneShot(clip);

        yield return wait;

        audioSource.enabled = false;
    }

    // Return item set as first to focus
    public GameObject GetFirstFocusItem() => firstFocusItem;

    public GameObject GetLastFocusItem() => lastFocusItem;

    // Set the last item that was in focus
    public void SetLastFocusItem(GameObject item)
    {
        lastFocusItem = item;
    }

}
