using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationSize : MonoBehaviour
{
    public bool pressedpres = true;
    // Start is called before the first frame update
    void Start()
    {

        //switch to 1900 x 1200 fullscreen

    }

    // Update is called once per frame
    void Update()
    {
        if (pressedpres)
        {
            Screen.SetResolution(1900, 1200, true);
            Debug.Log("Presentation Size");
        }        
    }
}
