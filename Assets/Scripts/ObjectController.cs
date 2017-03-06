using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public bool isWhite = true; // true for white, false for black
    public Material white;
    public Material black;
    private GameObject player;
    public GameObject game;
    private bool curr_color = true; // true for white, false for black

    public void Initialize()
    {
        player = transform.parent.FindChild("Player").gameObject;
        game = GameObject.Find("GameController");
        change_material();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.ToString() + " entered the trigger");
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().touch_object(transform.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.ToString() + " exited the trigger");
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().leave_object(transform.gameObject);
        }
    }

    public void change_color()
    {
        if (transform.parent.name == "Teleporter")
            return;


        isWhite = !isWhite;
        change_material();
    }

    public void change_material()
    {
        if (isWhite)
            GetComponent<MeshRenderer>().material = white;
        else
            GetComponent<MeshRenderer>().material = black;

        curr_color = isWhite;
        Material[] mesh = GetComponent<MeshRenderer>().materials;
        for (int i = 0; i < mesh.Length; i++)
        {
            Debug.Log(mesh[i]);
            if (curr_color)
                mesh[i] = white;
            else
                mesh[i] = black;

            curr_color = !curr_color;
        }
    }
}
