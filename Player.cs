using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;

    bool isJump;

    Rigidbody rigid;

    Vector3 moveVec;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();
        Move();
        Turn();
        Jump();

    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButton("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown?0.3f:1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }
    
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
//        rotationSpeedx = Input.GetAxis("Mouse X") * rotationSpeedx * Time.deltaTime;
//        rotationSpeedy = Input.GetAxis("Mouse Y") * rotationSpeedy * Time.deltaTime;
    }

    void Jump()
    {
        if(jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            // anim.SetBool("isJump", true);
            // anim.SetTrigger("doJump");
            isJump = true;

        }


    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.gameObject.tag == "Floor")
    //     {
    //      isJump = false;

    //     }
    // }
}