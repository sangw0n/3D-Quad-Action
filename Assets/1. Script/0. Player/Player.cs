using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private float hAxis;
    private float vAxis;

    private Vector3 moveVec;
    private Vector3 dodgeVec;

    private bool isWalkDown;
    private bool isJumpDown;

    private bool isJump;
    private bool isDodge;

    private Rigidbody rigid;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical");
        isWalkDown = Input.GetButton("Walk");
        isJumpDown = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge) moveVec = dodgeVec;
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
        if(isJumpDown && moveVec == Vector3.zero && !isJump && !isDodge) 
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    private void Dodge() // È¸ÇÇ
    {
        if (isJumpDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.65f);
        }
    }

    private void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
}
