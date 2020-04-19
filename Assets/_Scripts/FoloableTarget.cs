using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoloableTarget : MonoBehaviour, IInteractable
{
    [SerializeField] private bool targetAround = false;

    [SerializeField] protected float stopRange = 0.5f;
    [SerializeField] protected float followSpeed = 10f;
    [SerializeField] protected Transform followTarget;
    [SerializeField] protected Rigidbody2D followRb;
    [SerializeField] protected Collider2D followCollider;


    private void FixedUpdate()
    {
        if (followTarget != null)
        {
            if (Vector3.Distance(followTarget.position, transform.position) <= stopRange)
            {
                targetAround = true;
            }
            else
            {
                targetAround = false;
            }


            if (!targetAround)
            {
               
                followRb.velocity = new Vector2(((followTarget.transform.position - transform.position).normalized * followSpeed).x, followRb.velocity.y);
            }
        }


    }


    public void Interact()
    {
        Debug.Log("FOLLOW INTERACT");
        if (followTarget == null)
        {
            ActivateFollow();
        }
        else
        {
            DeactivateFollow();
        }

    }

    public void ActivateFollow()
    {
        if (followTarget == null)
        {
            followTarget = GameObject.FindGameObjectWithTag("HandHolder").transform;
        }
    }

    public void DeactivateFollow()
    {
        if (followTarget != null)
        {
            followTarget = null;
        }
    }
}
