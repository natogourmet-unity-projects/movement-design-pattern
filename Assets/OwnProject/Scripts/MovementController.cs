using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMovementHandler
{
    [SerializeField] private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] public float speed;

    [SerializeField] public float jumpStrength;

    [SerializeField] public bool grounded;

    [Header("Dash")]
    [SerializeField] public float dashStrength;

    [SerializeField] public float dashTime;

    [SerializeField] public float dragMultiplier;

    [SerializeField] public bool dashed;

    [SerializeField] public bool dashing;

    [Header("Wall Grab n Slide")]
    [SerializeField] public bool onWall;

    [SerializeField] public bool grabbing;

    [SerializeField] public float slideSpeed;

    [Header("Swimming")]
    [SerializeField] public float inWaterDrag;

    [SerializeField] public float inWaterFallSpeed;

    [SerializeField] private bool inWater;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.drag > ((inWater) ? inWaterDrag : 0)) rb.drag -= Time.deltaTime * dragMultiplier * 2;
        if (onWall && !grounded && !grabbing) WallSlide();
        if (inWater && !grounded && !grabbing) WaterFall();
    }

    public void OnHorizontalMove(float x)
    {
        if (dashing) return;
        Vector3 _velocity = rb.velocity;
        _velocity.x = x * speed;
        rb.velocity = _velocity;
    }

    public void OnVerticalMove(float y)
    {
        if (!inWater || y == 0) return;
        Vector3 _velocity = rb.velocity;
        _velocity.y = y * speed;
        rb.velocity = _velocity;
    }

    public void OnJump()
    {
        if (grounded)
        {
            grounded = false;
            Vector3 _velocity = rb.velocity;
            _velocity.y = jumpStrength;
            rb.velocity = _velocity;
        }
    }

    public void OnDash(float x, float y)
    {
        if (dashed) return;
        Vector3 _dir = new Vector3(x, y, 0);
        rb.velocity = _dir * dashStrength;
        dashing = true;
        StartCoroutine(AfterDash());
        if (!grounded) dashed = true;
    }

    IEnumerator AfterDash()
    {
        yield return new WaitForSeconds(dashTime);
        rb.drag = dragMultiplier;
        dashing = false;
    }

    public void OnWallGrab(float y)
    {
        grabbing = true;
        if (onWall) rb.velocity = new Vector3(0, y * speed, 0);
    }

    public void WallSlide()
    {
        rb.velocity = new Vector3(rb.velocity.x, -slideSpeed, 0);
    }

    public void WaterFall()
    {
        rb.velocity = new Vector3(rb.velocity.x, -inWaterFallSpeed, 0);
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            grounded = true;
            dashed = false;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            onWall = true;
            rb.useGravity = false;
        }
    }

    private void OnCollisionExit(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (dashing) dashed = true;
            grounded = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            onWall = false;
            grabbing = false;
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            rb.useGravity = false;
            inWater = true;
            rb.drag = inWaterDrag;
        }

    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            if (!onWall) rb.useGravity = true;
            inWater = false;
            if (!dashing) rb.drag = 0;
        }

    }

}
