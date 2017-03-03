using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public bool isArrowKey = false;
    public List<GameObject> objects;
    public List<GameObject> inventory;
    public List<GameObject> temp_list = new List<GameObject>();

    public float speed = 6.0F;
    public float rotate_speed = 80.0F;
    private float forward = 0F;
    private float rotation = 0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        movement();
        if ((isArrowKey && Input.GetKeyDown(KeyCode.Return)) || (!isArrowKey && Input.GetKeyDown(KeyCode.Space)))
        {
            foreach (GameObject obj in objects)
            {
                obj.GetComponent<ObjectController>().change_color();
            }
        }

        if (isArrowKey && Input.GetKeyDown(KeyCode.RightShift) || !isArrowKey && Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach (GameObject obj in objects)
            {
                Debug.Log(obj.tag);
                if (obj.tag == "Pickup")
                {
                    temp_list.Add(obj);
                }
            }

            foreach (GameObject obj in temp_list)
                interact(obj);
        }
    }

    /* Pick up or put down objects */
    public void interact(GameObject obj)
    {
        obj.SetActive(false);
        inventory.Add(obj);
        leave_object(obj);
    }

    public void touch_object(GameObject obj)
    {
        objects.Add(obj);
    }

    public void leave_object(GameObject obj)
    {
        objects.Remove(obj);
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
