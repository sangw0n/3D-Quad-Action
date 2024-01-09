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

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        isWalkDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * (isWalkDown ? 0.3f : 1.0f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", isWalkDown);

        transform.LookAt(transform.position + moveVec);
        /*
            다음에 이동할 방향 ( transform.position + moveVec )
            transform.position : 자기위치 기준
            내 위치에서 다음 방향 값을 더해 내가 이동할 위치를 구함
            
            Ex ) (0, 0, 0) + (0, 0, 1) -> 오른쪽으로 이동
                 오른쪽으로 회전 
        */

    }
}
