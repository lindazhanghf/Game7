using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public bool isWhite = true; // true for white, false for black
    public Material white;
    public Material black;
    private GameObject player;
    private PlayerController player_controller;
    //private GameObject game;
    public bool isTree = false;
    private bool curr_color = true; // true for white, false for black
    private Material[] mesh;

    public void Initialize()
    {
        player = transform.parent.FindChild("Player").gameObject;
        player_controller = player.GetComponent<PlayerController>();
        //game = GameObject.Find("GameController");
        change_material();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player_controller.touch_object(transform.gameObject);
            if (isTree)
            {
                if (transform.FindChild("Hanger0").childCount > 0)
                    player_controller.touch_object(transform.FindChild("Hanger0").GetChild(0).gameObject);
                if (transform.FindChild("Hanger1").childCount > 0)
                    player_controller.touch_object(transform.FindChild("Hanger1").GetChild(0).gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player_controller.leave_object(transform.gameObject);
            if (isTree)
            {
                if (transform.FindChild("Hanger0").childCount > 0)
                    player_controller.leave_object(transform.FindChild("Hanger0").GetChild(0).gameObject);
                if (transform.FindChild("Hanger1").childCount > 0)
                    player_controller.leave_object(transform.FindChild("Hanger1").GetChild(0).gameObject);
            }
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
        //if (isWhite)
        //    GetComponent<MeshRenderer>().material = white;
        //else
        //    GetComponent<MeshRenderer>().material = black;
        curr_color = isWhite;
        mesh = GetComponent<MeshRenderer>().materials;
        for (int i = 0; i < mesh.Length; i++)
        {
            if (curr_color)
                mesh[i] = white;
            else
                mesh[i] = black;

            curr_color = !curr_color;
        }

        GetComponent<MeshRenderer>().materials = mesh;
    }
}
