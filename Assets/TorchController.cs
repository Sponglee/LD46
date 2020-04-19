using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : GrabableObject
{
    public float burnoutDelay = 5f;
    public bool IsLit = true;

    [SerializeField] private Transform fire;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Floor"))
        {
            Invoke(nameof(PutFireOut), burnoutDelay);
        }

    }

    private void PutFireOut()
    {
        IsLit = false;
        fire.gameObject.SetActive(false);
    }

    public void LightTheTorch()
    {
        CancelInvoke();
        IsLit = true;
        fire.gameObject.SetActive(true);
    }

    public override void ActivateGrab()
    {
        CancelInvoke();
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        base.ActivateGrab();
    }

    public override void DeactivateGrab()
    {
        base.DeactivateGrab();
    }
}
