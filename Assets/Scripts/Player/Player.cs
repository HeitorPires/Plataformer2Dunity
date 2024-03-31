using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigidiBody;

    public float speed = 10;

    private void Awake()
    {
        _myRigidiBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            _myRigidiBody.velocity = new Vector2(-speed, _myRigidiBody.velocity.y);
        else if (Input.GetKey(KeyCode.D))
            _myRigidiBody.velocity = new Vector2(speed, _myRigidiBody.velocity.y);

    }
}
