using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSP : MonoBehaviour
{

    private Rigidbody rigidbody;
    private float velocity = 5f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move(rigidbody, velocity);
    }
        
    public void MoveDirection(Rigidbody rigidbody, float velocity)
    {
        rigidbody.MovePosition(rigidbody.position + transform.forward * velocity * Time.deltaTime);
    }

    private void SetRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput));
    }

    public void Move(Rigidbody rigidbody, float velocity)
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            return;

        SetRotation();
        MoveDirection(rigidbody, velocity);
    }
}
