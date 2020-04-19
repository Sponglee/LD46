using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{

    [SerializeField] private Transform bottomPoint;
    [SerializeField] private Transform upperPoint;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetAxis("Vertical") != 0)
        {
            //Debug.Log("HEY");
            other.GetComponent<PlayerController>().CanClimb = true;
        }
        else if (other.CompareTag("LilPlayer"))
        {
            other.GetComponent<LilPlayerController>().CanClimb = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().CanClimb = false;
            //ClimbTheLadder(other.GetComponent<Rigidbody2D>());
        }
        else if(other.CompareTag("LilPlayer"))
        {
            other.GetComponent<LilPlayerController>().CanClimb = false;
        }

    }


    public void ClimbTheLadder(Rigidbody2D target)
    {
       
    }
    
}
