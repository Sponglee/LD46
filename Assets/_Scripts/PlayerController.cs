using UnityEngine;


    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private float jumpForce = 400f;
        [SerializeField] private Transform bottomPont;
        [SerializeField] private Transform upperPoint;
        [SerializeField] private Animator charAnim;
        [SerializeField] private Rigidbody2D m_Rigidbody2D;


        [SerializeField] private bool IsGrounded;
        const float k_GroundedRadius = .2f;
        const float k_CeilingRadius = .01f;

        [SerializeField] private bool Jumped;
        private bool facingRight = true;


        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!Jumped)
            {
                Jumped = Input.GetButtonDown("Jump");
            }
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            Move(h, Jumped);
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

        public void Move(float speed, bool jump)
        {
            if (IsGrounded)
            {
                charAnim.SetFloat("Speed", Mathf.Abs(speed));
                m_Rigidbody2D.velocity = new Vector2(speed * maxSpeed, m_Rigidbody2D.velocity.y);
                if (speed > 0 && !facingRight)
                {
                    Flip();
                }
                else if (speed < 0 && facingRight)
                {
                    Flip();
                }
            }

            if (IsGrounded && jump && charAnim.GetBool("Grounded"))
            {
                IsGrounded = false;
                charAnim.SetBool("Grounded", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
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
