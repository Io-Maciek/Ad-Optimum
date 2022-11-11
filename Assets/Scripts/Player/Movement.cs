using System;
using System.Collections;
using System.Collections.Generic;
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
        var speedOfThatBoy = normalSpeed + runAddition * Input.GetAxis("Sprint");
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (speedOfThatBoy);

        rb.velocity = transform.rotation * new Vector3(newVelocity.x, rb.velocity.y, newVelocity.y);
        if (OnGround && Input.GetAxis("Jump")>0.0)
        {
            OnGround = false;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
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

}
