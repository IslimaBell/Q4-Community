using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //switch to 1900 x 1200 fullscreen
        Screen.SetResolution(1900, 1200, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
