using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionTest : MonoBehaviour
{
    public Transform target;
    public Material m;
    void Update()
    {
        
        Vector3 relativePos = (target.position + new Vector3(0, 1f, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.rotation = Quaternion.Lerp(current, rotation, Time.deltaTime);
        //transform.Translate(0, 0, 3 * Time.deltaTime);
    }
}
