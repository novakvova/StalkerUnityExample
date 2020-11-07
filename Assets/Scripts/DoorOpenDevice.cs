using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    bool _open = false;
    Transform deltaTransform;
    Vector3 Down;

    private void Start()
    {
        deltaTransform = transform;

        Down.x = deltaTransform.localPosition.x;
        Down.y = -1.4f;
        Down.z = deltaTransform.position.z;
    }

    public void Operate()
    {
        if (!_open)
        {
            transform.position = new Vector3(deltaTransform.position.x, deltaTransform.position.y - 5f, deltaTransform.position.z);
        }
        else
        {
            transform.position = new Vector3(deltaTransform.position.x, deltaTransform.position.y + 5f, deltaTransform.position.z);

        }

        _open = !_open;
    }
}
