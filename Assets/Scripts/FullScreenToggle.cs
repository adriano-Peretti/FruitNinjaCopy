using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{

    public void FullScreen(bool is_fullscreen)
    {
        Screen.fullScreen = is_fullscreen;
        Debug.Log("Fullscreen is " + is_fullscreen);
       
    }
}
