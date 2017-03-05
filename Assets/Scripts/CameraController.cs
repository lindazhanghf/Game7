using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed = 3;
    public float x;
    public float z;
    private Rigidbody body;
    //private Collider collider;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        //collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > -4.5)
            x = -2;
        else
            x = 0;

        if (transform.localPosition.z != 0)
            z = - transform.localPosition.z / Mathf.Abs(transform.localPosition.z);
        else
            z = 0;

        body.AddForce(new Vector3(x, 0, z) * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    //void OnCollisionStay(Collision c)
    //{
    //    transform = new Vector3(-4, 1.5);
    //}
}
