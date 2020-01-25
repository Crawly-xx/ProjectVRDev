using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{

    public float speed;
    private Rigidbody body;
    private Vector3 inputs = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0){ speed = 1.0f; }
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
            transform.forward = inputs;

      //  if (Input.GetButtonDown("Dash"))
       // {
       //     Vector3 dashVelocity = Vector3.Scale(transform.forward, speed * new Vector3((Mathf.Log(1f / (Time.deltaTime * body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * body.drag + 1)) / -Time.deltaTime)));
       //     body.AddForce(dashVelocity, ForceMode.VelocityChange);
       // }
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + inputs * speed * Time.fixedDeltaTime);
    }
}
