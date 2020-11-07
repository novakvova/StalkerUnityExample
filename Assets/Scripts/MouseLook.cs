using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;

    public float sensivityHor = 9f;
    public float sensivityVert = 9f;

    public float minimumVert = -70f;
    public float maximumVert = 70f;

    private float _rotationX = 0;
    private float _rotationY = 0;

    private float offsetX;
    private float offsetY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor + offsetY, 0);
            offsetX = 0;
            offsetY = 0;
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityHor + offsetX;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            offsetY = 0;
            offsetX = 0;

            _rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityHor + offsetY;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            _rotationY = Input.GetAxis("Mouse X") * sensivityHor + transform.localEulerAngles.y + offsetX;
         
            offsetY = 0;
            offsetX = 0;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }

    public void Otdacha(float X,float Y)
    {
        offsetX = X;
        offsetX = Y;
    }
        

}
