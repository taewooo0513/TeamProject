using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player status")]
    public int hp = 3;
    public float speed = 5;
    private float jump;
    
    private Animator anim;

    private bool isTouch = false;
    private bool isBind = false;

    private Vector2 mousePos;
    private int clickCount, curClickCount;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

        //        Destroy(GameManager.enemy[0]);

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
                if (Input.mousePosition.y > mousePos.y + 5f) //위로 터치 슬라이드 시 점프
                {
                    isTouch = true;
                    Jump();
                }
                else //누르고 뗐을 때 공격
                {
                    isTouch = true;
                    if(GameManager.enemy[0].gameObject != null)
                    Attack();
                }
            }
        }
        else if (isBind) //묶여있는 상태
        {
            if (Input.GetMouseButtonDown(0) && curClickCount < clickCount)
            {
                curClickCount++;
                GameObject.Find("Main Camera").GetComponent<Camera>().shakePower = 0.03f;
                GameObject.Find("Main Camera").GetComponent<Camera>().shakeTime = 0.3f;
            }
            else if (curClickCount >= clickCount)
            {
                curClickCount = 0;

                GameManager.enemy[0].GetComponent<MeleeEnemy>().hp -= 1;

                isBind = false;
            }
        }
    }

    void Attack()
    {
            if (transform.position.x < GameManager.enemy[0].transform.position.x && GameManager.enemy[0].transform.position.x <= transform.position.x + 1.8f)
            {
                if (GameManager.enemy[0].tag == "MeleeEnemy")
                {
                    GameManager.enemy[0].GetComponent<MeleeEnemy>().hp -= 1;
                }
                else if (GameManager.enemy[0].tag == "RangedEnemy")
                {
                    GameManager.enemy[0].GetComponent<RangedEnemy>().hp -= 1;
                }
            }

        anim.SetBool("IsAttack", true);
        Invoke("StopAtkAnim", 0.15f);
    }

    void Jump()
    {
        anim.SetBool("IsJump", true);
        Invoke("StopJumpAnim", 0.4f);

        jump = 8;
    }

    void Hurt()
    {
        anim.SetBool("IsHurt", true);
        Invoke("StopHurtAnim", 0.2f);
        GameObject.Find("Main Camera").GetComponent<Camera>().shakePower = 0.05f;
        GameObject.Find("Main Camera").GetComponent<Camera>().shakeTime = 0.5f;
        hp -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!anim.GetBool("IsHurt") && (collision.gameObject.tag == "MeleeEnemy" || collision.gameObject.tag == "RangedEnemy" || collision.gameObject.tag == "Bullet"))
        {
            Hurt();
        }
        if (collision.gameObject.tag == "BindEnemy")
        {
            isBind = true;
            clickCount = collision.gameObject.GetComponent<MeleeEnemy>().bindCount;
            Invoke("BindDamaged", collision.gameObject.GetComponent<MeleeEnemy>().bindTime);
        }
    }

    //애니메이션 끝내는 함수
    void StopHurtAnim()
    {
        anim.SetBool("IsHurt", false);
    }
void StopJumpAnim()
    {
        anim.SetBool("IsJump", false);

        jump = 0;

        EndTouch();
    }
    void StopAtkAnim()
    {
        anim.SetBool("IsAttack", false);

        Invoke("EndTouch", 0.4f);
    }
    
    void EndTouch()
    {
        isTouch = false;
    }
    
    void BindDamaged()
    {
        if(isBind)
        {
            Hurt();
            isBind = false;
        }
    }
}
