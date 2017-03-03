using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour {

    public Transform other_teleporter;

    private bool isArrowKey;
    private GameObject player;
    private PlayerController player_controller;
    private GameObject obj;
    //private GameObject game;

    // Use this for initialization
    void Start()
    {
        player = transform.parent.FindChild("Player").gameObject;
        player_controller = player.GetComponent<PlayerController>();
        isArrowKey = player_controller.isArrowKey;
        //isArrowKey = player.GetComponent<PlayerController>().isArrowKey;
        //game = GameObject.Find("GameController");
    }

    void onTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        player_controller.enter_teleport_zone();
    }

    void onTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        player_controller.exit_teleport_zone();
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("In trigger");
        if ((isArrowKey && Input.GetKeyUp(KeyCode.RightShift) || !isArrowKey && Input.GetKeyUp(KeyCode.LeftShift)))
        {
            Debug.Log("Player interact with Teleporter");
            List<GameObject> inventory = player_controller.inventory;
            if (transform.childCount == 0 && inventory.Count > 0)
            {
                obj = inventory[0];
                put_on_teleporter(obj);
                inventory.Remove(obj);
            }
            else if (transform.childCount > 0 && inventory.Count == 0)
            {
                obj = transform.GetChild(0).gameObject;
                //get_from_teleporter(obj);
                //inventory.Add(obj);
                player_controller.pick_up(obj);
            }
        }

        if ((isArrowKey && Input.GetKeyDown(KeyCode.Return)) || (!isArrowKey && Input.GetKeyDown(KeyCode.Space)))
        {
            if (transform.childCount > 0) // There is object on teleporter
            {
                teleport(transform.GetChild(0).gameObject);
            }
        }
    }

    //void get_from_teleporter(GameObject obj)
    //{
    //    obj.transform.SetParent(transform.parent);
    //    obj.SetActive(false);
    //}

    void put_on_teleporter(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = new Vector3(0, 1, 0);
        obj.SetActive(true);
    }

    void teleport(GameObject obj)
    {
        obj.transform.SetParent(other_teleporter);
        obj.transform.localPosition = new Vector3(0, 1, 0);
    }
}
