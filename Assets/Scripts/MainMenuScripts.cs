using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScripts : MonoBehaviour
{
    private bool Pause = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!Pause)
            {
                Pause = true;
                Time.timeScale = 0;
            } else
            {
                Pause = false;
                Time.timeScale = 1f;
            }
        }
    }
}
