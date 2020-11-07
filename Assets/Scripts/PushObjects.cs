using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushForce = 3f;
    private CharacterController Ch;

    private void Start()
    {
        Ch = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null && !body.isKinematic)
        {       
            body.velocity = hit.moveDirection  * (pushForce * (Ch.velocity.magnitude/2));
        }
    }
}
