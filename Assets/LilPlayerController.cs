using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilPlayerController : GrabableObject
{
    [SerializeField] private float lilSpeed = 10f;


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
                grabCollider.isTrigger = true;
                grabRb.gravityScale = 0f;
            }
            else
            {
                grabCollider.isTrigger = false;
                grabRb.gravityScale = 1f;
            }
        }
    }




    public override void ActivateGrab()
    {
        grabPoint = GameObject.FindGameObjectWithTag("HandHolder").transform;
        grabPoint.GetComponent<RelativeJoint2D>().connectedBody = grabRb;
    }

    public override void DeactivateGrab()
    {
        grabPoint.GetComponent<RelativeJoint2D>().connectedBody = null;  
        grabPoint = null;

    }


    private void FixedUpdate()
    {
       
       
        Quaternion lookRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        float step = 1f * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }
}

    



