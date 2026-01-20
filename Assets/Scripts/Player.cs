using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    float vAxis;
    public float speed;
    bool jDown;
    bool wDown;
    bool isJump;
    bool isDodge;


    Vector3 moveVec; 
    Vector3 dodgeVec;

    Rigidbody rigid;
    Animator anim;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();

    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");

    }
    void Move()
    {
        if (isDodge)
            moveVec = dodgeVec;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //걷기와 뛰기(삼항연산자 사용)
        if (wDown)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        else
            transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
 
    }
    void Turn()
    {
        //플레이어 회전
        if (moveVec != Vector3.zero)
        {
            Vector3 relativePos = (transform.position + moveVec) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
    }
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge)
        {
            rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge) 
        {
            dodgeVec = moveVec;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            anim.SetBool("isJump", false);
        isJump = false;
    }
}

