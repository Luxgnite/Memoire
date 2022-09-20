using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MovementController : MonoBehaviour
{
    public Animator characterAnimator;

    public float speedModifier = 1f;
    public float jumpForce = 1f;
    public float radiusGroundDetection = 1f;
    [Range(0, .3f)]
    public float movementSmoothing = .05f;

    private Rigidbody rb;

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public bool canJump = true;
    public bool canMove = true;

    private Controls controls;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    [Header("Interaction")]
    public LayerMask interactibleLayer;
    public string interactibleTag;
    public float interactibleRange = 5f;

    private bool displayedHelp;
    private GameObject interactibleGO;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controls = new Controls();
        controls.Enable();

        InterfaceManager.Instance.startDialogue.AddListener(OnStartDialogue);
        InterfaceManager.Instance.endDialogue.AddListener(OnEndDialogue);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheck.position, radiusGroundDetection, whatIsGround).Length != 0 && rb.velocity.y <= 0)
            isGrounded = true;
        else
            isGrounded = false;
        
        if(isGrounded) { canJump = true; }

        if(canMove) 
        { 
            Move(); 
            characterAnimator.SetFloat("speed", Mathf.Abs(controls.Player.Movement.ReadValue<float>())); 
        }


        if (controls.Player.Jump.triggered && canJump && canMove)
        {
            Jump();
        }

        characterAnimator.SetBool("isGrounded", isGrounded);
        
    }

    void Update()
    {
        interactibleGO = null;
        foreach (Collider coll in Physics.OverlapSphere(transform.position, interactibleRange, interactibleLayer))
        {
            if (coll.transform.tag.Equals(interactibleTag))
                interactibleGO = coll.gameObject;
        }

        if (interactibleGO != null && !displayedHelp)
        {
            DisplayHelp(true);
            displayedHelp = true;
        }
        else if (interactibleGO == null && displayedHelp)
        {
            DisplayHelp(false);
            displayedHelp = false;
        }

        if(interactibleGO != null && controls.Player.Interact.triggered)
        {
            interactibleGO.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canJump = false;
    }

    private void Move()
    {
        Vector3 targetVelocity = new Vector2(controls.Player.Movement.ReadValue<float>() * speedModifier, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (controls.Player.Movement.ReadValue<float>() > 0 && !facingRight || controls.Player.Movement.ReadValue<float>() < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        //transform.DOLookAt(Vector3.right * controls.Player.Movement.ReadValue<float>(), 0.5f, AxisConstraint.Y);
        transform.Rotate(new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z));
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    void DisplayHelp(bool show)
    {
        InterfaceManager.Instance.DisplayHelpInteract(show);
    }

    void OnStartDialogue()
    {
        canMove = false;
    }

    void OnEndDialogue()
    {
        canMove = true;
    }

    #region Gizmos
    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireSphere(groundCheck.position, radiusGroundDetection);

        Gizmos.color = Color.yellow;
        //Interactible range
        Gizmos.DrawWireSphere(this.transform.position, interactibleRange);

    }
# endregion
}
