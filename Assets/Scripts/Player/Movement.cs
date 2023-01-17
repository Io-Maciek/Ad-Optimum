using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(5f, 60f)]
    public float slopeLimit = 45f;

    [Tooltip("Default player speed")]
    public float normalSpeed = 7.0f;
    [Tooltip("*Additional* speed when player is running (Sprint Asix)")]
    public float runAddition = 7.0f;

    public float jumpHeight = 3.5f;

    public float crouchSpeed = 3.5f;

    public float colliderSizeOnCrouch = 1.5f;

    Rigidbody rb;
    new CapsuleCollider collider;

    public bool OnGround { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        OnGround = IsOnGround();
    }

    void Update()
    {
        float speedOfThatBoy;
        if (Input.GetAxis("Crouch") > 0)
        {
            speedOfThatBoy = crouchSpeed;
            collider.height = colliderSizeOnCrouch;
            
        }
        else
        {
            collider.height = 2f;
            speedOfThatBoy = normalSpeed + runAddition * Input.GetAxis("Sprint");
        }
        
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (speedOfThatBoy);

        rb.velocity = transform.rotation * new Vector3(newVelocity.x, rb.velocity.y, newVelocity.y);



        JumpCheck();
    }

    bool jump_button_in_use = false;
    void JumpCheck()
    {
        float jump_axis = Input.GetAxis("Jump");
        if (OnGround && !jump_button_in_use && jump_axis > 0.0f)
        {
            jump_button_in_use = true;
            OnGround = false;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }else if(jump_button_in_use && jump_axis <= 0.0f)
        {
            jump_button_in_use = false;
        }
    }




    /// <summary>
    /// Returns true if capsule collider <see cref="collider"/> is touching ground.
    /// </summary>
    private bool IsOnGround()
    {
        float height = Mathf.Max(collider.radius * 2f, collider.height);
        Vector3 capsuleBottom = transform.TransformPoint(collider.center - Vector3.up * height / 2f);
        float radius = transform.TransformVector(collider.radius, 0f, 0f).magnitude;
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    return true;
            }
        }


        return false;
    }

/*    private void OnTriggerEnter(Collider other)
    {
        OnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        OnGround = false;
    }*/
}
