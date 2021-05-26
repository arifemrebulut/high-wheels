using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private bool onPlaying = true;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("OnPlaying");
    }

    #region Subscribe and Unsubscribe to events

    private void OnEnable()
    {
        EventBroker.OnEndGame += SwitchCamera;
    }

    private void OnDisable()
    {
        EventBroker.OnEndGame -= SwitchCamera;   
    }

    #endregion

    private void SwitchCamera()
    {
        if (onPlaying)
        {
            animator.Play("OnEndGame");
        }
    }
}
