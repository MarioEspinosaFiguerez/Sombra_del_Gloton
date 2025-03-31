using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;

    // -1 -> Left / 1 -> Right
    private float direction;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }

    public void SetDirection(bool directionRight)
    {
        this.direction = directionRight ? 1 : -1;
    }

    private void MoveBullet() => transform.Translate(Vector3.right * direction * bulletSpeed * Time.deltaTime);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Wall"))
        {
            Debug.LogWarning("Collision with wall");
            Destroy(gameObject);
        }
    }
}