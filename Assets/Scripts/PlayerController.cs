using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isArrowKey = false;
    public List<GameObject> objects;
    public List<GameObject> inventory;

    public GameObject game;
    public bool isTeleporting;
    public GameObject apple_black;
    public GameObject apple_white;

    private AudioSource inventory_zipper;
    private AudioSource magic_color;

    public float speed = 6.0F;
    public float rotate_speed = 80.0F;
    private float forward = 0F;
    private float rotation = 0F;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        game = GameObject.Find("GameController");
        inventory_zipper = GameObject.Find("Inventory_bag").GetComponent<AudioSource>();
        magic_color = GetComponent<AudioSource>();
        isTeleporting = false;
    }

    void Update()
    {
        movement();
        if ((isArrowKey && Input.GetKeyDown(KeyCode.Return)) || (!isArrowKey && Input.GetKeyDown(KeyCode.Space)))
        {
            bool color_changed = false;
            foreach (GameObject obj in objects)
            {
                if (obj.tag == "Object") {
                    obj.GetComponent<ObjectController>().change_color();
                    color_changed = true;
                }
            }
            foreach (GameObject obj in inventory)
            {
                obj.GetComponent<ObjectController>().change_color();
                color_changed = true;
                both_inactive();
                if (obj.GetComponent<ObjectController>().isWhite)
                    apple_white.SetActive(true);
                else
                    apple_black.SetActive(true);
            }
            if (color_changed)
                magic_color.Play();

            game.GetComponent<GameController>().check_game();
        }

        if (!isTeleporting && (isArrowKey && Input.GetKeyDown(KeyCode.RightShift) || !isArrowKey && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            if (inventory.Count > 0)
            {
                foreach (GameObject obj in objects)
                {
                    if (obj.GetComponent<ObjectController>().isTree)
                    {
                        put_back(obj);
                        Debug.Log("Put back " + obj.ToString());
                        game.GetComponent<GameController>().check_game();
                        break;
                    }
                }
                return;
            }

            foreach (GameObject obj in objects)
            {
                if (obj.tag == "Pickup")
                {
                    pick_up(obj);
                    Debug.Log("Picked up " + obj.ToString());
                    break;
                }
            }
        }
    }

    /* Pick up objects */
    public void pick_up(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        inventory.Add(obj);
        if (objects.Contains(obj))
            leave_object(obj);

        // UI - inventory
        both_inactive();
        if (obj.GetComponent<ObjectController>().isWhite)
            apple_white.SetActive(true);
        else
            apple_black.SetActive(true);

        inventory_zipper.Play();
    }

    public void put_back(GameObject tree)
    {
        GameObject apple = inventory[0];
        if (tree.transform.FindChild("Hanger0").childCount == 0)
            apple.transform.SetParent(tree.transform.FindChild("Hanger0"));
        else
            apple.transform.SetParent(tree.transform.FindChild("Hanger1"));

        inventory.Remove(apple);
        objects.Add(apple);
        apple.transform.localPosition = Vector3.zero;
        apple.SetActive(true);

        // UI - inventory
        both_inactive();

        inventory_zipper.Play();
    }

    /* UI - inventory */
    public void both_inactive()
    {
        apple_white.SetActive(false);
        apple_black.SetActive(false);
    }

    public void touch_object(GameObject obj)
    {
        objects.Add(obj);
    }

    public void leave_object(GameObject obj)
    {
        objects.Remove(obj);
    }

    public void enter_teleport_zone()
    {
        isTeleporting = true;
    }

    public void exit_teleport_zone()
    {
        isTeleporting = false;
    }

    void movement()
    {
        CharacterController controller = GetComponent<CharacterController>();
        forward = 0F;
        if ((isArrowKey && Input.GetKey(KeyCode.UpArrow)) || (!isArrowKey && Input.GetKey(KeyCode.W)))
        {
            forward += speed;
        }
        if ((isArrowKey && Input.GetKey(KeyCode.DownArrow)) || (!isArrowKey && Input.GetKey(KeyCode.S)))
        {
            forward -= speed;
        }
        moveDirection = new Vector3(forward, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);

        rotation = 0F;
        if ((isArrowKey && Input.GetKey(KeyCode.LeftArrow)) || (!isArrowKey && Input.GetKey(KeyCode.A)))
        {
            rotation -= rotate_speed;
        }
        if ((isArrowKey && Input.GetKey(KeyCode.RightArrow)) || (!isArrowKey && Input.GetKey(KeyCode.D)))
        {
            rotation += rotate_speed;
        }
        transform.Rotate(Vector3.up * rotation * Time.deltaTime, Space.World);
    }
}
