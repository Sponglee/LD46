using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private bool interactedCoolDown = false;
    public bool InteractedCoolDown
    {
        get
        {
            return interactedCoolDown;
        }

        set
        {
            interactedCoolDown = value;
            Invoke(nameof(StopInteractedCoolDown), 0.5f);
        }
    }

    private void StopInteractedCoolDown()
    {
        interactedCoolDown = false;
    }



    [SerializeField] private Transform target;
    private bool targetAround = false;


    [SerializeField]private float range = 2f;
    [SerializeField] private Animator enemyAnim;


   
   
    void FixedUpdate ()
    {
        if(target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= range)
            {
                targetAround = true;
            }
            else
            {
                targetAround = false;
            }
        }

        transform.Translate(transform.right * Time.deltaTime,Space.World);


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Torch") || collision.CompareTag("Wall")) && !InteractedCoolDown)
        {
            InteractedCoolDown = true;
            transform.Rotate(Vector2.up, 180f);
        }
        else if ((collision.CompareTag("Player") || collision.CompareTag("LilPlayer")) && !InteractedCoolDown)
        {
            InteractedCoolDown = true;
            GameManager.Instance.RestartScene();
        }
    }


}
