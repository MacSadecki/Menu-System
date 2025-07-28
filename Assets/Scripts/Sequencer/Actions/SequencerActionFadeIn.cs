using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Sequence/Fade In", fileName = "Fade In")]
public class SequencerActionFadeIn : SequencerAction
{
    [SerializeField] float fadeTime = 1f;
    private GameObject fadePanel;

    public override void Initialize(GameObject obj)
    {
        fadePanel = obj;           
    }

    public override IEnumerator StartSequence(Sequencer context)
    {
        Vector3 startScale = fadePanel.transform.localScale;
        Vector3 endScale = new Vector3(0, 0, 0);

        // Start fading
        float elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            elapsedTime +=  Time.deltaTime;

            Vector3 newScale = Vector3.Lerp(startScale, endScale,(elapsedTime / fadeTime));
            fadePanel.transform.localScale = newScale;

            yield return null;
        }
    }
}
