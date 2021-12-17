// Some stupid rigidbody based movement by Dani

using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder, mask;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    public bool underWater;
    public bool grounded;

    float verticalLookRotation, horizontalLookRotation;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    public PostProcessVolume ppv;

    float lowerLookRange;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Jump();

        if (underWater)
        {
            Physics.gravity = Vector3.down;
            ppv.isGlobal = true;
            RenderSettings.fog = true;
            lowerLookRange = -15f;
            LookWater();
            MoveWater();
            sprintSpeed = 3;
        }
        else
        {
            Physics.gravity = Vector3.down * 9.81f;
            ppv.isGlobal = false;
            RenderSettings.fog = false;
            lowerLookRange = -45f;
            Look();
            Move();
            sprintSpeed = 6;
        }

        mask.SetActive(underWater);
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, lowerLookRange, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void LookWater()
    {
        cameraHolder.transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, lowerLookRange, 90f);

        horizontalLookRotation += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        horizontalLookRotation = Mathf.Clamp(horizontalLookRotation, -45f, 45f);

        cameraHolder.transform.localEulerAngles = new Vector3(-verticalLookRotation, horizontalLookRotation, 0);
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void MoveWater()
    {
        Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * mouseSensitivity / 20);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !underWater)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Space) && underWater)
        {
            rb.AddForce(transform.up * jumpForce / 50f);
        }
        if (Input.GetKey(KeyCode.LeftControl) && underWater)
        {
            rb.AddForce(-transform.up * jumpForce / 50f);
        }
    }
}
