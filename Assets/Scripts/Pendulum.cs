using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float minRangeRotation = 0;
    [SerializeField] private float maxRangeRotation = 0;
    float x = 0.7f;
    private int direction = 1;
    [SerializeField] private float rotationSpeed = 0;
    private BoxCollider[] colliders;

    public float currentRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(transform.localRotation.x);
        // currentRotation = transform.localRotation.x;
        // x = transform.localRotation.x;
        x = 90f;
        colliders = gameObject.GetComponents<BoxCollider>();

        Debug.Log(colliders.Length);
        
        
    }

    // Update is called once per frame
    void Update()
    {       
            // transform.localRotation = Quaternion.Euler(currentRotation,0f,0f);
            // currentRotation -=0.0001f;
            // x = 0.7f;
            // x += Time.deltaTime * 30;
            // Debug.Log(x);
            // Debug.Log(transform.localRotation.x);

            if(x>= maxRangeRotation){
                direction = -1;
            }
            if(x<= minRangeRotation){
                direction = 1;
            }

            x = x + Time.deltaTime * rotationSpeed * direction;



            transform.localRotation = Quaternion.Euler(x,90,0);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
}
