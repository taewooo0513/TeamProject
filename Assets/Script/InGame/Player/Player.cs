using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player status")]
    public int hp = 3;
    public float speed = 8;

    private Animator anim;

    private float MoveSpeed;
    private float jump;

    public bool isTouch;
    private bool isJump;
    private bool isBind;

    private GameObject Map;
    private MapScroll Mscoll;

    private Vector2 mousePos;
    private int clickCount, curClickCount;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = speed;
        anim = GetComponent<Animator>();
        Mscoll = GameObject.Find("Map").GetComponent<MapScroll>();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 이동
        if (!GameManager.isPlayerDie && !isBind)
        {
            transform.Translate(speed * Time.deltaTime, jump * Time.deltaTime, 0);
        }

        //플레이어 죽음
        if (hp == 0)
        {
            anim.SetBool("IsDie", true);
            GameManager.isPlayerDie = true;
        }

        Touch();
    }

    private void Touch()
    {
        //if (Input.touchCount > 0 && !isTouch && !isBind)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began: //눌렸을 때
        //            mousePos = touch.deltaPosition;
        //            break;

        //        case TouchPhase.Ended: //뗐을 때
        //            if (touch.deltaPosition.y > mousePos.y + 1f) //위로 터치 슬라이드 시 점프
        //            {
        //                isTouch = true;
        //                Jump();
        //            }
        //            else //누르고 뗏을 때 공격
        //            {
        //                isTouch = true;
        //                Attack();
        //            }
        //            break;
        //    }
        //}
        //else if (isBind) //묶여있는 상태
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began && clickCount < 20)
        //    {
        //        clickCount++;
        //        //Debug.Log(clickCount);
        //    }
        //    else if (clickCount >= 20)
        //    {
        //        clickCount = 0;


        //        isBind = false;
        //    }
        //}

        if (!isTouch && !isBind && !anim.GetBool("IsHurt"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isTouch = true;

                if (Input.mousePosition.y > mousePos.y + 5f && !isJump) //위로 터치 슬라이드 시 점프
                {
                    Jump();
                }
                else //누르고 뗐을 때 공격
                {
                    Attack();
                }
            }
        }
        else if (isBind) //묶여있는 상태
        {
            Mscoll.CurSpeed = 0;
            if (Input.GetMouseButtonDown(0) && curClickCount < clickCount)
            {

                curClickCount++;
                GameObject.Find("Main Camera").GetComponent<Camera>().shakePower = 0.03f;
                GameObject.Find("Main Camera").GetComponent<Camera>().shakeTime = 0.3f;
            }
            else if (curClickCount >= clickCount)
            {
                curClickCount = 0;
                Mscoll.CurSpeed = Mscoll.speed;
                
                isBind = false;
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("IsAttack");
        Invoke("StopAtk", 0.8f);
    }

    void Jump()
    {
        anim.SetTrigger("IsJump");
        Invoke("StopJump", 0.4f);

        jump = 8;
    }

    void Hurt()
    {
        anim.SetBool("IsHurt", true);
        Invoke("StopHurt", 0.2f);
        GameObject.Find("Main Camera").GetComponent<Camera>().shakePower = 0.05f;
        GameObject.Find("Main Camera").GetComponent<Camera>().shakeTime = 0.5f;
        hp -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!anim.GetBool("IsHurt") && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))) //피격 당하지 않은 상태, 적 or 총알 충돌
        {
            if (isTouch && !isJump)
            {
                collision.gameObject.GetComponent<Enemy>().hp--;
                isTouch = false;
            }
            else if(!isTouch)
            {
                Hurt();
            }
        }

        if (collision.gameObject.CompareTag("BindEnemy"))
        {
            isBind = true;
            clickCount = collision.gameObject.GetComponent<Enemy>().bindCount;
            Invoke("BindDamaged", collision.gameObject.GetComponent<Enemy>().bindTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isJump = true;
        }
    }

    void StopHurt()
    {
        anim.SetBool("IsHurt", false);
        isTouch = false;
    }
    void StopAtk()
    {
        isTouch = false;
    }
    void StopJump()
    {
        isTouch = false;

        jump = 0;
    }

    void BindDamaged()
    {
        if (isBind)
        {
            Hurt();
            Mscoll.CurSpeed = Mscoll.speed;
            isBind = false;
        }
    }
}
