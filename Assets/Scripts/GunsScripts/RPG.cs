using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPG : AbstractObject
{
    public float Force;

    public int maxBullets;

    private Text BulletsCountText;
    private Text AllBulletsCountText;

    private bool reload;

    public AudioClip StartClip;
    private AudioSource Source;

    public void Start()
    {
        
        GameObject g = GameObject.Find("BulletsCount");
        BulletsCountText = g.GetComponent<Text>();

        g = GameObject.Find("AllBullets");
        AllBulletsCountText = g.GetComponent<Text>();

        Source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Equiped)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !reload && AllBulletsCount > 0)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {     
        GameObject rocket = GameObject.Find("RPG_Rocket");

        if (rocket == null)
            return;

        Source.Stop();
        Source.PlayOneShot(StartClip);

        rocket.GetComponent<RPGShell>().Shoot();
        rocket.GetComponent<RPGShell>().Missile = true;

        rocket.GetComponent<Rigidbody>().isKinematic = false;
        rocket.GetComponent<Rigidbody>().velocity = (-transform.up) * Force;
        Reload();
    }

    private void Reload()
    {

    }

    public override void AddBullets(int b)
    {
        if (AllBulletsCount > maxBullets)
            return;

        AllBulletsCount++;

        BulletsCountText.text = BulletsCount.ToString();
        AllBulletsCountText.text = AllBulletsCount.ToString();
    }

    public void ReloadStart()
    {
        reload = true;
    }
    public void ReloadEnd()
    {
        reload = false;     
    }
}
