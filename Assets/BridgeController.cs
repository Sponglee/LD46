using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public void ToggleBridge()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
