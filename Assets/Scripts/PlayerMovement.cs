using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //vitesse de deplacement
    public float climbSpeed;

    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public float jumpForce;

    public Transform groundCheck;
    public float groundCheckRaduis;
    public LayerMask collisonLayer;



    public Animator animator;
    public SpriteRenderer spriteRenderer;


    public Rigidbody2D rb; // fait reference au rigid body du personnage
    public CapsuleCollider2D playerCollider;
    private Vector3 velocity = Vector3.zero;

    private float horizontalMovement;
    private float verticalMovement;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'une instance de PlayerMovement dans la sc�ne");
            return;
        }

        instance = this;
        Debug.Log(instance);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRaduis, collisonLayer);
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);//vers la ou on va
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);//deplace le personnage
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);//vers la ou on va
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);//deplace le personnage
        }

    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;
        //calcule le mouvement horizontalement du personnage grace a ca vitesse et aussi longtemps que le joueur appuie sur le bouton (Time.deltaTime)
        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("IsClimbing", isClimbing);
       
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce)); //fait sauter le personnage
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRaduis);
    }

}

