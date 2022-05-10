using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSize : MonoBehaviour
{
    public bool pressedCom = false;
    // Start is called before the first frame update
    void Start()
    {
        //switch to 1900 x 1080 fullscreen


    }

    // Update is called once per frame
    void Update()
    {
        if (pressedCom)
        {
        Screen.SetResolution(1900, 1080, true);
        Debug.Log("Computer Size");
        }        
    }
}
