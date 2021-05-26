using UnityEngine;
using UnityEngine.Playables;

public class ScoreCamera : MonoBehaviour
{
    PlayableDirector playableDirector;

    private void OnEnable()
    {
        EventBroker.OnScoreCountPoint += PlayScoreCameraAnimation;
    }

    private void OnDisable()
    {
        EventBroker.OnScoreCountPoint -= PlayScoreCameraAnimation;        
    }

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void PlayScoreCameraAnimation()
    {
        playableDirector.Play();
    }
}
