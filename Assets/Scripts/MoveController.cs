using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float MoveSpeed;
    public float RunSpeed;
    [HideInInspector]public float buf;

    private CharacterController ch;
    private Vector3 Movement = Vector3.zero;

    public bool crouch = false;
    public bool run = false;
    public bool jump = false;
    public bool move = false;

    public float gravity = -9.8f;
    private float JumpSpeed = 0;

    List<Action> funcs = new List<Action>();

    bool flag = false;

    public Animation anim;

    private void Start()
    {
        buf = MoveSpeed;

        ch = GetComponent<CharacterController>();

        funcs.Add(Run);     
        funcs.Add(Jump);
        funcs.Add(Crouch);
    }

    private void Run() // Run
    {
        if (Input.GetKey(KeyCode.LeftShift) && !jump && !crouch) // Run
        {
            if (ch.isGrounded)
            {
                run = true;
                MoveSpeed = Mathf.Lerp(MoveSpeed, RunSpeed, 0.140f);
            }
        }
        else
        {
            run = false;
            MoveSpeed = Mathf.Lerp(buf, RunSpeed, 0.100f);
        }
    }

    private void Crouch() // Crouch
    {       
        if (Input.GetKeyDown(KeyCode.LeftControl) && ch.isGrounded && !run && !jump)
        {
            crouch = true;

            buf /= 2;
            //transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.5f, Time.deltaTime * 50f), transform.localScale.z);
            anim.Play("CameraDown");
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && crouch)
        {
            crouch = false;

            buf *= 2;
            MoveSpeed = buf;
            //transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.5f, Time.deltaTime * 50f), transform.localScale.z);
            anim.Play("CameraUp");
        }

        if (!Input.GetKey(KeyCode.LeftControl) && crouch)
        {
            crouch = false;

            buf *= 2;
            MoveSpeed = buf;
            //transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, 1f, Time.deltaTime * 50f), transform.localScale.z);
            anim.Play("CameraUp");
        }
    }

    private void Jump() // Jump
    {
        if (ch.isGrounded) // Jump
        {
            if (Input.GetButtonDown("Jump") && !crouch)
            {
                jump = true;
                JumpSpeed = 5f;
            }
            else
            {
                jump = false;
                JumpSpeed = gravity * Time.deltaTime;
            }
        }
        else
        {
            JumpSpeed += gravity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Movement.z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        Movement.x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        if (Movement.z != 0 || Movement.x != 0)
            move = true;
        else
            move = false;

        foreach(var func in funcs)
        {
            func.Invoke();
        }


        Movement *= Time.deltaTime;
        Movement = transform.TransformDirection(Movement);

        Movement.y += JumpSpeed;      
       
        ch.Move(Movement * Time.deltaTime);

    }
}
