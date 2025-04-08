using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float rotation;
    public float jump;
    private float ySpeed;
    public float jumpButtonPeriod;
    private float? lastGrounded;
    private float? jumpButtonPresssTime;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirections = new Vector3(horizontalInput, 0, 0);
        float magnitude = movementDirections.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        movementDirections.Normalize();
        ySpeed += Physics.gravity.y * Time.deltaTime;
         Vector3 velocity = movementDirections * magnitude;
        // velocity = slopeAjust(velocity);
        velocity.y += ySpeed;
        characterController.Move(velocity * speed * Time.deltaTime);
        
        if (characterController.isGrounded)
        {
            lastGrounded = Time.time;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPresssTime = Time.time;
        }
        if (Time.time - lastGrounded <= jumpButtonPeriod)
        {
            ySpeed = -0.5f;
            if (Time.time - jumpButtonPresssTime <= jumpButtonPeriod)
            {
                ySpeed = jump;
                jumpButtonPresssTime = null;
                lastGrounded = null;
            }
        }
       
                characterController.SimpleMove(movementDirections * magnitude * speed);

        if (movementDirections != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirections, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotation * Time.deltaTime);
        }
    }
}
