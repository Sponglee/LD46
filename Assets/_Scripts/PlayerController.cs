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

                playerRb.gravityScale = climbGravityScale;
                playerCollider.isTrigger = true;
            }
            else
            {
                playerRb.gravityScale = gravityScale;
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

    private void StopInteractedCoolDown()
    {
        interactedCoolDown = false;
    }

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

    [SerializeField] private Transform handHolder;

private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
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


    public void InteractWIthSurroundings()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.7f);
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log("Interacted " + colliders[i].gameObject.name);
            if (colliders[i].gameObject != gameObject && colliders[i].gameObject.GetComponent<IInteractable>() != null)
            {
                colliders[i].gameObject.GetComponent<IInteractable>().Interact(handHolder);
                return;
            }
             
        }
    }


    private void FixedUpdate()
    {
        float v = 0f;

        if(CanClimb)
        {
            v = Input.GetAxis("Vertical");
        }
       
        float h = Input.GetAxis("Horizontal");
        Move(h,Jumped,v);
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

        if (verticalSpeed != 0f)
        {
            playerRb.velocity = new Vector2(0f, verticalSpeed*maxSpeed);
        }


        playerRb.velocity = new Vector2(speed * maxSpeed, playerRb.velocity.y);
        charAnim.SetFloat("Speed", Mathf.Abs(speed));

        if (IsGrounded && jump && charAnim.GetBool("Grounded"))
        {
            IsGrounded = false;
            charAnim.SetBool("Grounded", false);
            playerRb.AddForce(new Vector2(0f, jumpForce));
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
