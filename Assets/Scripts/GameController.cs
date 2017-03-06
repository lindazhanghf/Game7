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
    public List<string> color_pairs;

    private GameObject new_obj;
    private float x;
    private float z;
    private bool color_1;// True for white, false for black
    private bool color_2;

    void Start()
    {
        won = false;
        color_pairs = new List<string>();

        //spawn objects
        //foreach (GameObject obj in models)
        for (int i = 0; i < models.Length; i++)
        {
            random_color();
            x = Random.Range(-8f, 8f);
            z = Random.Range(2f, 18f);
            new_obj = (GameObject) Instantiate(models[i], new Vector3(), Quaternion.identity);
            new_obj.transform.position = new Vector3(x, 0, z);
            new_obj.transform.SetParent(black_world);
            new_obj.GetComponent<ObjectController>().isWhite = color_1;
            new_obj.GetComponent<ObjectController>().Initialize();

            new_obj = (GameObject)Instantiate(models[i], new Vector3(), Quaternion.identity);
            new_obj.transform.position = new Vector3(-x, 0, -z);
            new_obj.transform.SetParent(white_world);
            new_obj.GetComponent<ObjectController>().isWhite = color_2;
            new_obj.GetComponent<ObjectController>().Initialize();
        }
    }

    void Update()
    {
        if (won && glass.position.y > - 6)
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
                color_pairs.Add(obj.gameObject.name + obj.GetComponent<ObjectController>().isWhite.ToString()); // Stringify the object to "ObjectName+color"
            }
        }
        foreach (Transform obj in black_world)
        {
            if (obj.gameObject.tag == "Object")
            {
                if (color_pairs.Contains(obj.gameObject.name + obj.GetComponent<ObjectController>().isWhite.ToString())) // If "ObjectName+color" already exist in the list, then they have the same color, test fail
                {
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
