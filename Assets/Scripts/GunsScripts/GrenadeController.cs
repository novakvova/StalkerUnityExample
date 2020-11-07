using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private int grenadeCount = 0;

    public GameObject GrenadePrefab;

    public int maxCount = 3;
    public float force;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && grenadeCount > 0)
        {
            Throw();
            grenadeCount--;
        }
    }

    private void Throw()
    {
        GameObject a = Instantiate(GrenadePrefab, transform.position + transform.forward * 3f, transform.rotation);

        a.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);
    }

    public bool AddGrenade()
    {
        if (grenadeCount >= maxCount)
            return false;

        grenadeCount++;
        return true;

    }
    
}
