using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequence/Play Sound", fileName = "Play Sound")]
public class SequencerActionSound : SequencerAction
{
    [SerializeField] private AudioClip soundToPlay;
    public override IEnumerator StartSequence(Sequencer context)
    {
        AudioSource.PlayClipAtPoint(soundToPlay, Camera.main.transform.position, 1f);
        yield return null;
    }
}
