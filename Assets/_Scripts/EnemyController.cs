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
            Invoke(nameof(StopInteractedCoolDown), 0.2f);
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


    private void Start()
    {
        StartCoroutine(MovingSequence());
    }

    private IEnumerator MovingSequence()
    {
        while(true)
        {
            transform.Translate(transform.right * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("LilPlayer")) && !InteractedCoolDown)
        {
            StopAllCoroutines();

            if (!IsFacingCheck(collision.transform))
            {
                transform.Rotate(Vector2.up, 180f);
            }

            enemyAnim.SetTrigger("EnemyAttack");
            InteractedCoolDown = true;
            GameManager.Instance.TargetEaten(collision.transform);

        }
        else if (!collision.CompareTag("Floor") && !collision.CompareTag("Ladder") && !collision.CompareTag("Puzzle") && !InteractedCoolDown && IsFacingCheck(collision.transform))
        {
            ChangeDirection();
        }
        else if(collision.CompareTag("Torch") && IsFacingCheck(collision.transform))
        {
            if (!collision.GetComponent<TorchController>().IsLit)
            {
                return;
            }
            else
            {
                ChangeDirection();
            }
        }

    }

    private void ChangeDirection()
    {
        InteractedCoolDown = true;
        transform.Rotate(Vector2.up, 180f);
    }

    private bool IsFacingCheck(Transform target)
    {
        //Debug.Log(target.gameObject.name + " : " + Vector2.Dot(transform.right*transform.localScale.x, (target.transform.position - transform.position).normalized));
        return Vector2.Dot(transform.right*transform.localScale.x, (target.transform.position - transform.position).normalized) > 0;
    }
}
