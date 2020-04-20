using UnityEngine;


public partial class PlayerController : MonoBehaviour
{

    
    public bool CanClimb
    {
        get
        {
            return canClimb;
        }

        set
        {
            canClimb = value;
            if (value == true)
            {

                //playerRb.gravityScale = climbGravityScale;
                playerCollider.isTrigger = true;
            }
            else
            {
                //playerRb.gravityScale = gravityScale;
                playerCollider.isTrigger = false;
            }
        }
    }
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

    public Transform ActiveInteraction
    {
        get
        {
            return activeInteraction;
        }

        set
        {
            activeInteraction = value;
            if(value == null)
            {
                charAnim.SetBool("HasInteractable", false);
            }
            else
            {
                charAnim.SetBool("HasInteractable", true);
            }
        }
    }

    private void StopInteractedCoolDown()
    {
        interactedCoolDown = false;
    }



    [SerializeField] private Transform activeInteraction = null;
    [SerializeField] private float gravityScale = 3f;
    [SerializeField] private float climbGravityScale = 1f;
    [SerializeField] private bool canClimb = false;
   

    [SerializeField] private bool interactedCoolDown = false;
  

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private Transform bottomPont;
    [SerializeField] private Transform upperPoint;
    [SerializeField] private Animator charAnim;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private CircleCollider2D playerCollider;


    [SerializeField] private bool IsGrounded;
    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .01f;

    [SerializeField] private bool Jumped;
    private bool facingRight = true;
    private GameManager gameManager;

    [SerializeField] private Transform handHolder;

private void Start()
    {
        gameManager = GameManager.Instance;

        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(!gameManager.GameOverBool)
        {
            if (!Jumped)
            {
                Jumped = Input.GetButtonDown("Jump");
               
            }

            if (Input.GetButtonDown("Interact") && !InteractedCoolDown)
            {
                InteractWIthSurroundings();
                InteractedCoolDown = true;
            }
        }
    }

    
    private void FixedUpdate()
    {
        if(!gameManager.GameOverBool)
        {
            float v = 0f;

            if (CanClimb)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    playerRb.velocity = new Vector2(0f, Input.GetAxis("Vertical") * maxSpeed);
                    //target.transform.position = Vector3.Lerp(target.transform.position, upperPoint.position, Time.deltaTime * 1f);
                }
                else if (Input.GetAxis("Vertical") < 0)
                {
                    playerRb.velocity = new Vector2(0f, Input.GetAxis("Vertical") * maxSpeed);
                    //target.transform.position = Vector3.Lerp(target.transform.position, bottomPoint.position, Time.deltaTime * 1f);
                }
            }

            float h = Input.GetAxis("Horizontal");
            Move(h, Jumped, v);
            Jumped = false;
            IsGrounded = false;


            Collider2D[] colliders = Physics2D.OverlapCircleAll(bottomPont.position, k_GroundedRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    IsGrounded = true;
            }
            charAnim.SetBool("Grounded", IsGrounded);
        }
       

    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.transform.CompareTag("Floor"))
    //    {
    //        AudioManager.Instance.PlaySound("Jump");
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.GetComponent<IInteractable>() != null)
    //    {
    //        GameManager.Instance.MoveSelectionCanvas(collision.transform);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<IInteractable>() != null)
    //    {
    //        GameManager.Instance.DisableSelectionCanvas();
    //    }
    //}


    public void InteractWIthSurroundings()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.7f);

        Transform lastInteraction = DropActiveInteraction();

        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log("Interacted " + colliders[i].gameObject.name);
            if (colliders[i].gameObject != gameObject && colliders[i].gameObject.GetComponent<IInteractable>() != null && IsFacingCheck(colliders[i].transform) 
                && lastInteraction != colliders[i].transform)
            {
                if(colliders[i].transform != ActiveInteraction)
                {
                    colliders[i].gameObject.GetComponent<IInteractable>().Interact();
                    ActiveInteraction = colliders[i].transform;
                    return;
                }
            }
             
        }
    }

    private Transform DropActiveInteraction()
    {
        if (ActiveInteraction != null)
        {
            Transform returnValue = ActiveInteraction;
            ActiveInteraction.GetComponent<IInteractable>().Interact();
            ActiveInteraction = null;
           
            return returnValue;
        }
        return null;
    }

    private bool IsFacingCheck(Transform target)
    {
        Debug.Log(target.gameObject.name + " : " + Vector2.Dot(transform.right * transform.localScale.x, (target.transform.position - transform.position).normalized));
        return Vector2.Dot(transform.right * transform.localScale.x, (target.transform.position - transform.position).normalized) > 0;
    }

    public void Move(float speed, bool jump, float verticalSpeed = 0f)
    {
        if (speed > 0 && !facingRight)
        {
            Flip();
        }
        else if (speed < 0 && facingRight)
        {
            Flip();
        }

        //if (verticalSpeed != 0f)
        //{
        //    playerRb.velocity = new Vector2(0f, verticalSpeed*maxSpeed);
        //}


        playerRb.velocity = new Vector2(speed * maxSpeed, playerRb.velocity.y);
        charAnim.SetFloat("Speed", Mathf.Abs(speed));

        if (IsGrounded && jump && charAnim.GetBool("Grounded") && !CanClimb)
        {
            IsGrounded = false;
            charAnim.SetBool("Grounded", false);
            playerRb.AddForce(new Vector2(0f, jumpForce));
            //AudioManager.Instance.PlaySound("jump");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

 
    

   
}
