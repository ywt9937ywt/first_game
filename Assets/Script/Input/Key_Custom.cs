using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Custom : MonoBehaviour
{
    // Start is called before the first frame update
    public static Key_Custom key_bounding;

    public KeyCode forward { get; set;}
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }

    public KeyCode rotate_left { get; set; }
    public KeyCode rotate_right { get; set; }
    void Awake()
    {
        if(key_bounding == null)
        {
            key_bounding = this;
        }
        else
        {
            Destroy(this);
        }

        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "D"));
        rotate_left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "Q"));
        rotate_right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "E"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
