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
    bool iDown;
    
    bool isJump;
    bool isDodge;

    Rigidbody rigid;
    Animator anim;
    
    Vector3 moveVec;

    GameObject nearObject;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   

        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Interation();


    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        iDown = Input.GetButton("Interation");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //노멀라이즈 = 대각선도 스피드 동일하게 적용

        transform.position += moveVec * speed * (wDown?0.4f:1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }
    
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);

    }

    void Jump()
    {
        if(jDown && moveVec == Vector3.zero && !isJump && !isDodge)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;

        }



    }
    void Dodge()
    {
        if(jDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f);

        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
         anim.SetBool("isJump", false);
         isJump = false;

        }
    }

    void OnTriggerStay(Collider other)
    {

        if( other.tag == "Weapon")
        {
            nearObject = other.gameObject;

            Debug.Log(nearObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if( other.tag == "Weapon")
        {
            nearObject = null;
        }
    }
    
    void Interation()
    {
        if(iDown && nearObject != null && !isJump && !isDodge)
        {
            
        }
    }
}