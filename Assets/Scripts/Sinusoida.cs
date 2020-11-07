using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoida : MonoBehaviour
{
    float t;
    public float speedX;
    public float speedY;
    void Update()
    {
        t = Time.time;
        transform.position += Vector3.forward * Time.deltaTime * speedX;
        transform.position += Vector3.up * Mathf.Sin(t) * Time.deltaTime * speedY;
    }
}
