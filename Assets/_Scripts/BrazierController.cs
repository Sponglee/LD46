using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazierController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Torch"))
        {
            collision.GetComponent<TorchController>().LightTheTorch();
        }
    }
}
