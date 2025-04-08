using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private CharacterController characterController;
    public float jumpSpeed;
    public float ySpeed;
    public float rotation;
    private float? lastGrounded;
    private float? jumpButtonPresssTime;
    public float jumpButtonPeriod;

    private float dashTimer = 0;
    public float dashCooldown;
    
    public float accelerationDash;
    private bool hasDashed = false;

    public bool onWall;
    public LayerMask layerMask;
    public Vector3 boxSizes = new Vector3(0.5f,0.5f,0.5f);

    public bool canWallJump = false;
    private Vector3 wallJumpNormal;

    public bool hasWallJumped = false;

    public bool isGrounded;
    // private Animator animator;

    public bool goRight = true;
    [Header("Shoot")]
    public ShootController shootController;
    public float timeBetweenShoots;
    private float lastShootTime;



    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
        shootController = GetComponent<ShootController>();
        // animator = GetComponent<Animator>();
        goRight = true;
        lastShootTime = Time.time - timeBetweenShoots;
    }

    // Update is called once per frame
    void Update()
    {
        

        isGrounded = characterController.isGrounded;
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0){
            goRight = horizontal > 0;
        }
       
        Vector3 moveDirection = new Vector3(horizontal, 0, 0);
        moveDirection.Normalize();
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (Time.time > timeBetweenShoots + lastShootTime)
            {
                lastShootTime = Time.time;
                shootController.Shoot(goRight);
            }
        }

        if(Input.GetKeyDown(KeyCode.Z) && characterController.isGrounded){
            if(dashTimer<=0){
                dashTimer = dashCooldown;
                magnitude *= accelerationDash;
                hasDashed = true;
            }
        }
        if(dashTimer>0){
            dashTimer -= Time.deltaTime;
            magnitude *= accelerationDash;
        }else if(hasDashed){
            hasDashed = false;
            magnitude /= accelerationDash;
        }
        
        characterController.Move(moveDirection * speed * Time.deltaTime * magnitude);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        
        Vector3 velocity = moveDirection * magnitude;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);
        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection , Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotation * Time.deltaTime );
        }
        else{            
            Quaternion toRotation = Quaternion.Euler(0,goRight ? 90 :-90 ,0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotation * Time.deltaTime );
        }


        if(characterController.isGrounded)
        {
            canWallJump = true;
            hasWallJumped = false;
            lastGrounded = Time.time;
            if(Input.GetButtonDown("Jump"))
            {
                jumpButtonPresssTime = Time.time;
            }
        }
       
                
        if(Time.time - lastGrounded <= 0.001f)
        {
            ySpeed = -0.5f;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);

            if(Time.time - jumpButtonPresssTime <= jumpButtonPeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPresssTime = null;
                lastGrounded = null;
                hasWallJumped = false;
            }

        }


        if(characterController.collisionFlags == CollisionFlags.Sides && !characterController.isGrounded && characterController.collisionFlags != CollisionFlags.Above){
            if(!hasWallJumped){
                canWallJump = true;
                if(Input.GetButtonDown("Jump")){
                    ySpeed = jumpSpeed;
                    velocity= wallJumpNormal*speed;
                    characterController.Move(-velocity*Time.deltaTime);
                    hasWallJumped = true;
                }
            }
        }        
    }
}
