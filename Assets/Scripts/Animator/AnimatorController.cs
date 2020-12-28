using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AnimationId { 
    Idle=0,
    Run=1,
    Attack=2,
    Hurt=3,
    Jump=4
}
public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    public void Play(AnimationId animationId) {
        if (animator == null) {
            animator = GetComponent<Animator>();

        }
        animator.Play(animationId.ToString());
    }
}
