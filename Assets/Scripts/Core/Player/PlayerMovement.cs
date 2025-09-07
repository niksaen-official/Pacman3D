using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Player movement settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [HideInInspector] public float HorizontalInput;
    [HideInInspector] public float VerticalInput;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(HorizontalInput, 0f, VerticalInput);
        movement = movement.normalized * moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    public void Stop()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
