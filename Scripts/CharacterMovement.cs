using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private DataManager dataManager;
    private CameraDraw cameraDraw;
    public Rigidbody2D rb2D;

    public float moveSpeed;

    public float buildSpeed;
    [SerializeField] 
    private bool isBuilding = false;


    public GameObject digLocation;
    public float digSpeed;
    [SerializeField]
    public bool isDigging = false;

    public GameObject testraySlope;
    public bool slopeHit;

    public GameObject testrayFront;
    public GameObject testrayGround;

    public GameObject testrayBackGround;
    public GameObject testrayFrontGround;

    public bool frontHit;
    public bool grounded;
    public bool onFloor;
    public bool spawnGroundCheck = false;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private int facingDirection = 1; // 1 for right, -1 for left

    public SpawnBridge spawnBridge;

    //animation
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        onFloor = true;
        isBuilding = false;
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            cameraDraw = mainCamera.GetComponent<CameraDraw>();

            if (cameraDraw == null)
            {
                Debug.LogError("Main camera does not have a CameraDraw component.");
            }
        }
        else
        {
            Debug.LogError("Main camera not found in the scene.");
        }

        animator = GetComponent<Animator>();
        GameObject gameManagerObject = GameObject.Find("GameManager");


        if (gameManagerObject != null)
        {
            // Get the DataManager component from the GameManager object
            dataManager = gameManagerObject.GetComponent<DataManager>();

            if (dataManager == null)
            {
                Debug.LogError("GameManager does not have a DataManager component.");
            }
        }
        else
        {
            Debug.LogError("GameManager object not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (grounded)
        {
            spawnGroundCheck = true;
        }

        Vector3 groundObjectTest = testrayGround.transform.position;

        Ray ray = new Ray(groundObjectTest, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has a name
            if (hit.collider.gameObject.name != null)
            {
                // Print the name of the hit object
                //Debug.Log("Hit object name: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "Background")
                {
                    onFloor = true;
                }
                else
                {
                    onFloor = false;
                }
            }
            else
            {
                // Print a message if the hit object doesn't have a name
                Debug.Log("Hit object has no name.");
            }
        }



        slopeHit = cameraDraw.CheckRay(testraySlope.transform.position, onFloor && facingDirection == -1);
        frontHit = cameraDraw.CheckRay(testrayFront.transform.position, onFloor && facingDirection == -1);
        //grounded = cameraDraw.CheckRay(testrayGround.transform.position, onFloor && facingDirection == -1);
        grounded = cameraDraw.CheckRay(testrayGround.transform.position, onFloor && facingDirection == -1) ||
           cameraDraw.CheckRay(testrayFrontGround.transform.position, onFloor && facingDirection == -1) ||
           cameraDraw.CheckRay(testrayBackGround.transform.position, onFloor && facingDirection == -1);
        //Debug.Log(onFloor && facingDirection == -1);

        if (grounded == true && rb2D.velocity.y <= 0)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;
        }
        else
        {
            rb2D.gravityScale = 1;
        }

            if (!isDigging)
        {
            // Move forward until front check hits an object
            if (!frontHit)
            {
                if (!isBuilding)
                {
                    Move(moveSpeed * facingDirection);
                    animator.SetBool("IsWalking", true);
                }
                else
                {
                    Move(buildSpeed * facingDirection);
                    animator.SetBool("IsWalking", true);
                }
            }
            else
            {
                // Turn around when front check hits an object
                spawnBridge.StopBuilding();
                isBuilding = false;
                FlipDirection();
                animator.SetBool("IsWalking", false);

            }
        }
        else
        {

            Move(digSpeed * facingDirection);
            //transform.Translate(0, -digSpeed * Time.deltaTime, 0);
            cameraDraw.PaintRay(digLocation.transform.position, 100);
            
            animator.SetBool("IsWalking", true);

        }
    }

    public bool IsBuilding()
    {
        return isBuilding;
    }

    public void Building()
    {
        if (!isBuilding)
        {
            Debug.Log("Building: True");
            isBuilding = true;
        }
        else
        {
            Debug.Log("Building: False");
            isBuilding = false;
        }
    }

    public bool IsDigging()
    {
        return isBuilding;
    }

    public void Digging()
    {
        if (!isDigging)
        {
            Debug.Log("Digging: True");
            isDigging = true;
        }
        else
        {
            Debug.Log("Digging: False");
            isDigging = false;
        }
    }


    void FlipDirection()
    {
        facingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Move(float speed)
    {
        if (spawnGroundCheck)
        {
            if (slopeHit == true)
            {
                transform.Translate(speed * Time.deltaTime, 0.05f, 0);
            }
            else
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "danger" tag
        if (other.CompareTag("Danger"))
        {
            // Destroy the game object
            Destroy(gameObject);
            dataManager.AddDeadLing();
        }

        if (other.CompareTag("Goal"))
        {
            // Destroy the game object
            Destroy(gameObject);
            dataManager.AddSurvivedLings();
        }
    }

    public int WalkingDirection() {  return facingDirection; }
}

