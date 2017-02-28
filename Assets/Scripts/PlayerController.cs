using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

public bool isArrowKey = false;
    public List<GameObject> objects;
    public List<GameObject> getObjectsW;
    public List<GameObject> getObjectsB;

    //public float jumpSpeed = 8.0F;
    //public float gravity = 20.0F;
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

        if (isArrowKey && Input.GetKeyDown(KeyCode.RightShift))
        {
            foreach (GameObject obj in objects)
            {
                if (obj.CompareTag("Pickup"))
                {
                    obj.SetActive(false);
                    getObjectsW.Add(obj);
                }
            }
        }

        if (!isArrowKey && Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach (GameObject obj in objects)
            {
                if(obj.CompareTag("Pickup"))
                {
                    obj.SetActive(false);
                    getObjectsB.Add(obj);
                }
            }
        }
  
        
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
        //if (controller.isGrounded)
        //{
        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;
        //    if (Input.GetButton("Jump"))
        //        moveDirection.y = jumpSpeed;
        //}
        //moveDirection.y -= gravity * Time.deltaTime;
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
