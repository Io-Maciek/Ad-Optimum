using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;

    public float speed = 10.0f;
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
        //TODO movement
        //rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed* Time.deltaTime;
        if (OnGround)
        {
            //TODO jumping
        }
    }




    /// <summary>
    /// Returns true if capsule collider <see cref="collider"/> is touching ground.
    /// </summary>
    private bool IsOnGround()
    {
        float capsuleHeight = Mathf.Max(collider.radius * 2f, collider.height);
        Vector3 capsuleBottom = transform.TransformPoint(collider.center - Vector3.up * capsuleHeight / 2f);
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
