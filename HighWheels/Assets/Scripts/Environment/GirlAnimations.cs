using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAnimations : MonoBehaviour
{
    Animator animator;

    private void OnEnable()
    {
        EventBroker.OnPickUpSuspension += PlayClapAnimation;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpSuspension -= PlayClapAnimation;    
    }

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void PlayClapAnimation()
    {
        animator.SetBool("shouldClap", true);
    }
}
