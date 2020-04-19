using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {



   

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
        if (collision.CompareTag("Torch") || collision.CompareTag("Wall"))
        {
            transform.Rotate(Vector2.up, 180f);
        }
    }


}
