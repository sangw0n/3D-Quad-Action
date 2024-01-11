using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    private bool isWalkDown;
    private bool isJump;

    private Rigidbody2D rigid;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
        Move();
        Turn();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical");
        isWalkDown = Input.GetButton("Walk");
        isJump = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * (isWalkDown ? 0.3f : 1.0f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", isWalkDown);
    }

    private void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    private void Jump()
    {
        if(isJump) 
        {

        }
    }   
}
