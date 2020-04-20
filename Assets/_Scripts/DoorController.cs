using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private bool diamondInRange =false;
    [SerializeField] private bool lilPlayerInRange = false;


    private GameObject playerReference;
    private GameObject lilPlayerReference;
    private GameObject gemReference;

    public bool LilPlayerInRange
    {
        get
        {
            return lilPlayerInRange;
        }

        set
        {
            if(value != lilPlayerInRange)
                GameManager.Instance.TargetInDoor(GameManager.Instance.lilPanel, value);
            lilPlayerInRange = value;
        }
    }

    public bool DiamondInRange
    {
        get
        {
            return diamondInRange;
        }

        set
        {

            if(value != diamondInRange)
                GameManager.Instance.TargetInDoor(GameManager.Instance.gemPanel, value);
            diamondInRange = value;
        }
    }

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

        if (collision.CompareTag("LilPlayer"))
        {
            LilPlayerInRange = true;
        }

        if (collision.CompareTag("Diamond"))
        {
            DiamondInRange = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LilPlayer"))
        {
            LilPlayerInRange = false;
           
        }

        if (collision.CompareTag("Diamond"))
        {
            DiamondInRange = false;
       
        }
    }
    public void DoorCheckForWin(Transform target)
    {
        
        if(DiamondInRange && LilPlayerInRange)
        {
            GameManager.Instance.WinSequence();
        }
        else if(!DiamondInRange)
        {
            GameManager.Instance.GlowGemPanel(GameManager.Instance.gemReference);
        }
        else if(!LilPlayerInRange)
        {
            GameManager.Instance.GlowGemPanel(GameManager.Instance.lilPlayerReference);
        }
    }


}

