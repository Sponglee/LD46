using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilPlayerController : FoloableTarget,IInteractable
{
   
    [SerializeField] private bool canClimb = false;

   

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
                Debug.Log("CAN CLIMB");
                followCollider.isTrigger = true;
            }
            else
            {
                Debug.Log("CANNOT CLIMB");
                followCollider.isTrigger = false;
            }
        }
    }



    private void LateUpdate()
    {

        if (CanClimb)
        {
            if (followTarget.position.y - transform.position.y > stopRange / 2f)
            {
                followRb.velocity = Vector2.up * followSpeed;
            }
            else if (followTarget.position.y - transform.position.y < -stopRange / 2f)
            {
                followRb.velocity = Vector2.down * followSpeed;
            }
        }

        //Quaternion lookRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        //float step = 1f * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }




}





