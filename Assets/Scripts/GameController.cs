using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool won;

    public Transform glass;
    public Transform white_world;
    public Transform black_world;
    public Material color_white;
    public Material color_black;
    public GameObject[] models;
    public GameObject apple;
    public List<string> color_pairs;

    private GameObject obj_b;
    private GameObject obj_w;
    private float x;
    private float z;
    private bool color_1;// True for white, false for black
    private bool color_2;

    void Start()
    {
        won = false;
        color_pairs = new List<string>();

        //spawn objects
        for (int i = 0; i < models.Length; i++)
        {
            random_color();
            x = Random.Range(-8, 9);
            z = Random.Range(2, 19);
            obj_b = (GameObject)Instantiate(models[i], new Vector3(x, 0, z), Quaternion.identity);
            obj_b.transform.SetParent(black_world);
            obj_b.GetComponent<ObjectController>().isWhite = color_1;
            obj_b.GetComponent<ObjectController>().Initialize(i > 6 ? true : false);

            obj_w = (GameObject)Instantiate(models[i], new Vector3(-x, 0, -z), Quaternion.identity);
            obj_w.transform.SetParent(white_world);
            obj_w.GetComponent<ObjectController>().isWhite = color_2;
            obj_w.GetComponent<ObjectController>().Initialize(i > 6 ? true : false);

            if (i == 6) // Rotate bench model to the right side up
            {
                obj_b.transform.Rotate(Vector3.left * 90);
                obj_w.transform.Rotate(Vector3.left * 90);
            }
        }

        // Spawn Apples
        GameObject apple0 = (GameObject)Instantiate(apple, Vector3.zero, Quaternion.identity);
        GameObject apple1 = (GameObject)Instantiate(apple, Vector3.zero, Quaternion.identity);
        if (Random.value < 0.5) // Both apple in black world
        {
            apple0.transform.SetParent(obj_b.transform.FindChild("Hanger0"));
            apple0.GetComponent<ObjectController>().isWhite = color_1;

            apple1.transform.SetParent(obj_b.transform.FindChild("Hanger1"));
            apple1.GetComponent<ObjectController>().isWhite = color_1;
        }
        else // Both apples in white world
        {
            apple0.transform.SetParent(obj_w.transform.FindChild("Hanger0"));
            apple0.GetComponent<ObjectController>().isWhite = color_2;

            apple1.transform.SetParent(obj_w.transform.FindChild("Hanger1"));
            apple1.GetComponent<ObjectController>().isWhite = color_2;
        }
        apple0.transform.localPosition = Vector3.zero;
        apple1.transform.localPosition = Vector3.zero;
        apple0.GetComponent<ObjectController>().change_material();
        apple1.GetComponent<ObjectController>().change_material();
    }

    void Update()
    {
        if (won && glass.position.y > -6)
        {
            glass.position = glass.position + new Vector3(0, -5) * Time.deltaTime;
        }
    }

    public bool check_game()
    {
        color_pairs.Clear();
        foreach (Transform obj in white_world)
        {
            if (obj.gameObject.tag == "Object") // TODO pickup color_pairs
            {
                ObjectController contrl = obj.GetComponent<ObjectController>();
                color_pairs.Add(obj.gameObject.name + contrl.isWhite.ToString()); // Stringify the object to "ObjectName+color"
                if (contrl.isTree)
                {
                    Debug.Log(contrl.ToString() + " is a tree");
                    if (obj.transform.FindChild("Hanger0").childCount > 0)
                        color_pairs.Add(obj.gameObject.name + "Apple" + obj.transform.FindChild("Hanger0").GetChild(0).GetComponent<ObjectController>().isWhite.ToString());
                    if (obj.transform.FindChild("Hanger1").childCount > 0)
                        color_pairs.Add(obj.gameObject.name + "Apple" + obj.transform.FindChild("Hanger1").GetChild(0).GetComponent<ObjectController>().isWhite.ToString());
                }
            }
        }
        foreach (Transform obj in black_world)
        {
            if (obj.gameObject.tag == "Object")
            {
                ObjectController contrl = obj.GetComponent<ObjectController>();
                if (color_pairs.Contains(obj.gameObject.name + contrl.isWhite.ToString())) // If "ObjectName+color" already exist in the list, then they have the same color, test fail
                    return false;

                if (contrl.isTree)
                {
                    if (obj.transform.FindChild("Hanger0").childCount > 0
                        && color_pairs.Contains(obj.gameObject.name + obj.transform.FindChild("Hanger0").GetChild(0).GetComponent<ObjectController>().isWhite.ToString()))
                        return false;
                    if (obj.transform.FindChild("Hanger1").childCount > 0
                        && color_pairs.Contains(obj.gameObject.name + obj.transform.FindChild("Hanger1").GetChild(0).GetComponent<ObjectController>().isWhite.ToString()))
                        return false;
                }
            }
        }
        Debug.Log("Puzzle solved!!!!!");
        glass.GetComponent<AudioSource>().Play();
        won = true;
        return true;
    }

    private void random_color()
    {
        int rand = Random.Range(1, 3);
        switch (rand)
        {
            case 1:
                color_1 = true;
                color_2 = true;
                break;
            case 2:
                color_1 = false;
                color_2 = false;
                break;
            default:
                color_1 = false;
                color_2 = true;
                break;
        }
    }
}
