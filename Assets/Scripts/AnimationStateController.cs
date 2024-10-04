using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    //optimization
    int isRunningHash;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
     animator = GetComponent<Animator>();

    isRunningHash = Animator.StringToHash("isRunning");
    isJumpingHash = Animator.StringToHash("isJumping");
    }

    public void isRunning(bool value)
    {
        animator.SetBool(isRunningHash, value);
    }

    public void isJumping(bool value)
    {
        animator.SetBool(isJumpingHash, value);
    }
}
