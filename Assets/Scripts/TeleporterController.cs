using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour {

    public Transform other_teleporter;

    private bool isArrowKey;
    private GameObject player;
    private GameObject game;
    private GameObject obj;

    // Use this for initialization
    void Start()
    {
        player = transform.parent.FindChild("Player").gameObject;
        isArrowKey = player.GetComponent<PlayerController>().isArrowKey;
        game = GameObject.Find("GameController");
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("In trigger");
        if ((isArrowKey && Input.GetKeyDown(KeyCode.RightShift) || !isArrowKey && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            List<GameObject> inventory = player.GetComponent<PlayerController>().inventory;
            Debug.Log("Player interact with Teleporter");
            if (inventory.Count > 0)
            {
                obj = inventory[0];
                obj.transform.SetParent(transform);
                obj.transform.localPosition = new Vector3(0, 1, 0);
                obj.SetActive(true);

                inventory.Remove(obj);
            }
        }

        if ((isArrowKey && Input.GetKeyDown(KeyCode.Return)) || (!isArrowKey && Input.GetKeyDown(KeyCode.Space)))
        {
            if (transform.childCount > 0) // There is object on teleporter
            {
                obj = transform.GetChild(0).gameObject;
                obj.transform.SetParent(other_teleporter);
                obj.transform.localPosition = new Vector3(0, 1, 0);
            }
        }
    }
}
