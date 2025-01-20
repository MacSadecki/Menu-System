using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Sequence/Load Scene", fileName = "Load Scene")]
public class SequencerActionLoadScene : SequencerAction
{
    [SerializeField] private int sceneIndex;
    public override IEnumerator StartSequence(Sequencer context)
    {           
        SceneManager.LoadScene(sceneIndex);
        yield return null;
    }
}
