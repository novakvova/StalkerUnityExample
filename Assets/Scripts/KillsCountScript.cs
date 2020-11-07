using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsCountScript : MonoBehaviour
{
    private int count = 0;
    private string buf;

    private Text KillsText;

    public void Start()
    {
        KillsText = GetComponent<Text>();
        buf = "Kills: " + count.ToString();
        KillsText.text = buf;
    }

    public void AddKill()
    {
        count++;
        buf = "Kills: " + count.ToString();
        KillsText.text = buf;
    }
}
