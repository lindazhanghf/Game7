using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public bool isWhite = true; //  true for white, false for black
    public Material white;
    public Material black;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = transform.parent.FindChild("Player").gameObject;
    }

    //// Update is called once per frame
    //void Update () {

    //}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.ToString() + " entered the trigger");
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().touch_object(transform.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.ToString() + " exited the trigger");
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().leave_object(transform.gameObject);
        }
    }

    public void change_color()
    {
        isWhite = !isWhite;
        if (isWhite)
        {
            GetComponent<MeshRenderer>().material = white;
        }
        else
        {
            GetComponent<MeshRenderer>().material = black;
        }
    }
}
