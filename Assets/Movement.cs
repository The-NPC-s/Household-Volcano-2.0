using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float stoppingSpeed = 5f; // Adjust the stopping speed as needed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        moveDirection.Normalize();

        // Apply movement
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

        // Gradually decrease velocity when no input is detected
        if (magnitude == 0)
        {
            Vector3 currentVelocity = rb.velocity;
            Vector3 newVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, stoppingSpeed * Time.deltaTime);
            rb.velocity = newVelocity;
        }
    }
}
