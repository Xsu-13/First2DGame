using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class KelliView : MonoBehaviour
{
    public KelliModel model;
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset run, idle, attack, jump, sqade;

    State previousState;

    void Start()
    {
        if (skeletonAnimation == null) return;
    }

    
    void Update()
    {
        if (skeletonAnimation == null) return;
        if (model == null) return;

        if ((skeletonAnimation.skeleton.ScaleX < 0) != model.facingLeft)
        {   
            Turn(model.facingLeft);
        }


        var currentModelState = model.state;

        
        if (previousState != currentModelState)
        {
            PlayNewStableAnimation();
        }

        previousState = currentModelState;
    }


    void PlayNewStableAnimation()
    {
        var newModelState = model.state;
        AnimationReferenceAsset nextAnimation;

        // Add conditionals to not interrupt transient animations.

        if (previousState == State.Jumping && newModelState != State.Jumping)
        {
            return; // Sound
        }

        if (newModelState == State.Jumping)
        { 
            nextAnimation = jump;
        }
        else
        {
            if (newModelState == State.Running)
            {
                nextAnimation = run;
            }
            else
            {
                nextAnimation = idle;
            }
        }

        skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
    }

    public void Turn(bool facingLeft)
    {
        skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
    }
}
