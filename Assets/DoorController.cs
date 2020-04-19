using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private GameObject playerReference;
    private GameObject lilPlayerReference;
    private GameObject gemReference;

    private void Start()
    {
        playerReference = GameManager.Instance.playerReference.gameObject;
        lilPlayerReference = GameManager.Instance.lilPlayerReference.gameObject;
        gemReference = GameManager.Instance.gemReference.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DoorCheckForWin(collision.transform);
        }
    }


    public void DoorCheckForWin(Transform target)
    {
        int checkCount = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && (colliders[i].gameObject == playerReference || colliders[i].gameObject == lilPlayerReference || colliders[i].gameObject == gemReference))
            {
                checkCount++;    
            }
        }
         
        if(checkCount>=3)
        {
            GameManager.Instance.WinSequence();
        }
        else
        {
            GameManager.Instance.GlowGemPanel();
        }
    }


}

