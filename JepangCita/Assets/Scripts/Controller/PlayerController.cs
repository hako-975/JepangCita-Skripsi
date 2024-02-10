using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    private float movementSpeed = 4f;
    [SerializeField]
    private float maxFallZone = -5f;

    // -9.81 * 30
    private readonly float gravity = -294.3f;

    Joystick joystick;

    CharacterController cc;

    Animator animator;

    Vector3 move;
    Vector3 velocity;

    float turnSmoothVelocity;
    float horizontal;
    float vertical;

    bool isWalking = false;
    
    private GameObject cam;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera"); 
        cc = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();

        // set posisi awal karakter
        cc.enabled = false;
        cc.transform.position = Vector3.up * 0.5f;
        cc.enabled = true;

        joystick = FindAnyObjectByType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        // cek karakter jatuh
        if (transform.position.y < maxFallZone)
        {
            velocity.y = -2f;
            cc.enabled = false;
            cc.transform.position = Vector3.up * 0.5f;
            cc.enabled = true;
        }

        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;
            vertical = Input.GetAxisRaw("Vertical") + joystick.Vertical;
        }

        move = new Vector3(horizontal, 0f, vertical).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(movementSpeed * Time.deltaTime * moveDirection.normalized);
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        // animator
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;

        animator.SetBool("IsWalking", isWalking);
    }
}