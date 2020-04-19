using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : GrabableObject, IInteractable
{
    public override void ActivateGrab()
    {
        GameManager.Instance.GrabGem(transform);
        base.ActivateGrab();
    }

    public override void DeactivateGrab()
    {
        GameManager.Instance.DropGem(transform);
        base.DeactivateGrab();
    }
}
