using UnityEngine;
using JacobHomanics.Essentials.RPGController;

public class PenelopeAnimator : MonoBehaviour
{

    public Animation anim;

    public PlayerMotor controller;

    public float damping = 0.05f;

    public string horizontal = "X";
    public string vertical = "Z";

    public string jump = "Jump";
    public bool triggerBasedJump;

    public bool isUsingIsMoving;
    public string isMoving = "IsMoving";

    public void Jump()
    {
        anim.Play("jump");
        // if (triggerBasedJump)
        //     anim.SetTrigger(jump);
        // else
        //     anim.Play(jump, 1, 0f);
    }


    void Update()
    {
        if (!controller.characterController.isGrounded)
            return;

        var localized = controller.NormalizedInputMoveDirection;

        // if (isUsingIsMoving)
        // anim.SetBool(isMoving, localized.x != 0 || localized.z != 0);



        if (localized.z > 0)
        {
            anim.Play("run");
        }
        else if (localized.z < 0)
        {
            anim.Play("runback");
        }
        else if (localized.x > 0)
        {
            anim.Play("runright");
        }
        else if (localized.x < 0)
        {
            anim.Play("runleft");
        }
        else
        {
            anim.Play("idle");
            // anim.SetFloat(vertical, 0f, damping, Time.deltaTime);
        }
    }
}
