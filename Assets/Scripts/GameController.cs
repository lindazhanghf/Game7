using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Transform glass;
    public Transform white_world;
    public Transform black_world;
    public GameObject[] models;

    private List<string> color_pairs;

    void start()
    {
        color_pairs = new List<string>();
        //spawn objects
    }

    public bool check_game()
    {
        //color_pairs.Clear();
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
                    color_pairs.Clear();
                    return false;
                }
            }
        }
        Debug.Log("Puzzle solved!!!!!");
        color_pairs.Clear();
        return true;
    }
}
