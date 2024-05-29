using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }
    public void PlayTargetAnimation(string TargetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", true);
        animator.CrossFade(TargetAnimation, 0.2f);
    }
    public void UpdateAnimatorValues (float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //animation snapping
        float snappedHorizontalMovement;
        float snappedVerticalMovement;

        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement > 0.55)
        {
            snappedHorizontalMovement = 0.5f;
        }

        else if(horizontalMovement > 0.55f)
        {
            snappedHorizontalMovement = 1f;
        }
        else if (horizontalMovement < 0f && horizontalMovement > -0.55)
        {
            snappedHorizontalMovement = -0.5f;
        }
        else if (horizontalMovement < -0.55)
        {
            snappedHorizontalMovement = -1f;
        }
        else
        {
            snappedHorizontalMovement = 0f;
        }
        #endregion
        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement > 0.55)
        {
            snappedVerticalMovement = 0.5f;
        }

        else if (verticalMovement > 0.55f)
        {
            snappedVerticalMovement = 1f;
        }
        else if (verticalMovement < 0f && verticalMovement > -0.55)
        {
            snappedVerticalMovement = -0.5f;
        }
        else if (verticalMovement < -0.55)
        {
            snappedVerticalMovement = -1f;
        }
        else
        {
            snappedVerticalMovement = 0f;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontalMovement = horizontalMovement;
            snappedVerticalMovement = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVerticalMovement, 0.1f, Time.deltaTime);
    }
}
