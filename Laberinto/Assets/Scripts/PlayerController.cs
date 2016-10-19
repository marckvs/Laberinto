using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 40;
    Rigidbody rb;
    private CharacterController controller;
    private Quaternion targetRotation;
    private Camera cam;

    void Start () {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        //rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        ControlMouse();
        //ControlWASD();

	}

    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        Debug.DrawRay(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y), new Vector3(mousePos.x, mousePos.y, transform.position.y).normalized, new Vector4(0f, 256f,0f,1f));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0f, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);


        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;
        controller.Move(motion * Time.deltaTime);
    }

    void ControlWASD()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;
        controller.Move(motion * Time.deltaTime);
    }
    /*void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 move = input.normalized * walkSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + move);
    }*/
}
