using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Player;
    //private Collider collider;

    // Use this for initialization
    void Start()
    {
        Player = transform.parent;
        //collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    //void Update () {
    //}

    //void OnCollisionStay(Collision c)
    //{
    //    transform = new Vector3(-4, 1.5);
    //}
}
