using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        // (1,0)
        // new Vector2(1, 0)
        transform.Translate(moveInput * Vector2.right * moveSpeed * Time.deltaTime);
        // (5,0) (-5,0)
    }

    private void Jump()
    {
        // (0,1)
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}