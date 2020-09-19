using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float jumpHeight = 30;
    [SerializeField] string groundLayer = "Ground";
    

    private Rigidbody rb;
    private bool isJumping = false;
    private bool isGrounded = true;
    private Vector3 move = Vector3.zero;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        isJumping = Input.GetButtonDown("Jump");

        move = Vector3.zero;
        move.x = x;
        move.z = z;
        move = move.normalized;

        if (move.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move.x < 0 && isFacingRight)
        {
            Flip();
        }

        if (isJumping && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(groundLayer) && !isGrounded)
        {
            isGrounded = true;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
