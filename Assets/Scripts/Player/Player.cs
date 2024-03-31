using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigidiBody;

    public float speed = 10;

    public Vector2 friction = new Vector2(-.3f, 0);
    public float jumpForce = 15;

    private void Awake()
    {
        _myRigidiBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();

    }

    private void HandleMovement()
    {

        if (Input.GetKey(KeyCode.A))
            _myRigidiBody.velocity = new Vector2(-speed, _myRigidiBody.velocity.y);
        else if (Input.GetKey(KeyCode.D))
            _myRigidiBody.velocity = new Vector2(speed, _myRigidiBody.velocity.y);

        if (_myRigidiBody.velocity.x > 0)
            _myRigidiBody.velocity += friction;
        else if (_myRigidiBody.velocity.x < 0)
            _myRigidiBody.velocity -= friction;

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _myRigidiBody.velocity = Vector2.up * jumpForce; 
    }
}
