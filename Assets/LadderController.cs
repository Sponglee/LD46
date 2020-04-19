using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
  
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("HEY");
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
        }
        else if(other.CompareTag("LilPlayer"))
        {
            other.GetComponent<LilPlayerController>().CanClimb = false;
        }

    }
    
}
