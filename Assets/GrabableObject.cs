using UnityEngine;

public class GrabableObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected Rigidbody2D grabRb;
    [SerializeField] protected CapsuleCollider2D grabCollider;
    [SerializeField] protected Transform grabPoint = null;

    public void Interact(Transform target)
    {
        Debug.Log("INTERACT");
        if (grabPoint == null)
        {
            ActivateGrab();
        }
        else
        {
            DeactivateGrab();
        }

    }

    public virtual void ActivateGrab()
    {
        if(grabPoint == null)
        {
            grabPoint = GameObject.FindGameObjectWithTag("GrabHolder").transform;
            grabPoint.GetComponent<FixedJoint2D>().connectedBody = transform.GetComponent<Rigidbody2D>();
            grabCollider.isTrigger = true;
            grabRb.constraints = RigidbodyConstraints2D.None;
        }
           
    }
    
    public virtual void DeactivateGrab()
    {
        if(grabPoint != null)
        {
            grabPoint.GetComponent<FixedJoint2D>().connectedBody = null;
            grabRb.constraints = RigidbodyConstraints2D.None;
            grabCollider.isTrigger = false;
            grabRb.gravityScale = 1f;
            grabPoint = null;
        }
    }


}
