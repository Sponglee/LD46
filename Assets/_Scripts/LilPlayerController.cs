using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilPlayerController : FoloableTarget,IInteractable
{
   
    [SerializeField] private bool canClimb = false;
    [SerializeField] private Animator lilAnim;
   

    public bool CanClimb
    {
        get
        {
            return canClimb;
        }

        set
        {
            canClimb = value;
            if (value == true)
            {
                //Debug.Log("CAN CLIMB");
                followCollider.isTrigger = true;
            }
            else
            {
                //Debug.Log("CANNOT CLIMB");
                followCollider.isTrigger = false;
            }
        }
    }



    private void LateUpdate()
    {

        if (followTarget != null)
        {
            lilAnim.SetBool("Follow",true);   
            if(CanClimb)
            {
                if (followTarget.position.y - transform.position.y > stopRange / 2f)
                {

                    lilAnim.SetBool("Running", true);
                    followRb.velocity = Vector2.up * followSpeed;
                }
                else if (followTarget.position.y - transform.position.y < -stopRange / 2f)
                {

                    lilAnim.SetBool("Running", true);
                    followRb.velocity = Vector2.down * followSpeed;
                }
                else
                {

                    lilAnim.SetBool("Running", false);
                }
            }
            else if (!IsFacingCheck(followTarget.transform))
            {
                transform.Rotate(Vector2.up, 180f);
            }
        }
        else
            lilAnim.SetBool("Follow", false);

        //Quaternion lookRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        //float step = 1f * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }


    private bool IsFacingCheck(Transform target)
    {
        //Debug.Log(target.gameObject.name + " : " + Vector2.Dot(transform.right*transform.localScale.x, (target.transform.position - transform.position).normalized));
        return Vector2.Dot(transform.right * transform.localScale.x, (target.transform.position - transform.position).normalized) > 0;
    }

}





