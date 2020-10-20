using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player status")]
    public int hp = 3;
    [Space(2f)]
    public float speed = 5;
    public float jump;

    private GameObject enemy;
    private Animator anim;
    private SpriteRenderer sprRenderer;

    private bool isTouch = false;
    private bool isBind = false;

    private Vector2 mousePos;
    private int clickCount;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();
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

        //        FindNearObject("BindEnemy");
        //        GameManager.isEnemyDie = true;
        //        Destroy(enemy);

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
                    Attack();
                }
            }
        }
        else if(isBind) //묶여있는 상태
        {
            if (Input.GetMouseButtonDown(0) && clickCount < 20)
            {
                clickCount++;
                //Debug.Log(clickCount);
            }
            else if(clickCount >= 20)
            {
                clickCount = 0;

                FindNearObject("BindEnemy");
                GameManager.isEnemyDie = true;
                Destroy(enemy);

                isBind = false;
            }
        }
    }

    void Attack()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            FindNearObject("Enemy");
        }
        else
        {
            enemy = null;
        }

        if (enemy != null)
        {
            if (transform.position.x < enemy.transform.position.x && enemy.transform.position.x <= transform.position.x + 1.8f)
            {
                GameManager.isEnemyDie = true;
                Destroy(enemy);
            }
        }

        anim.SetBool("IsAttack", true);
        Invoke("StopAtkAnim", 0.15f);
    }

    void Jump()
    {
        anim.SetBool("IsJump", true);
        Invoke("StopJumpAnim", 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!anim.GetBool("IsHurt") && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet"))
        {
            anim.SetBool("IsHurt", true);
            Invoke("StopHurtAnim", 0.2f);
            hp -= 1;
        }
        if(collision.gameObject.tag == "BindEnemy")
        {
            isBind = true;
        }
    }

    void StopHurtAnim()
    {
        anim.SetBool("IsHurt", false);
    }
    void StopJumpAnim()
    {
        anim.SetBool("IsJump", false);

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

    void FindNearObject(string Object)
    {
        /*가장 가까운 적 찾기*/
        List<GameObject> FindEnemy = new List<GameObject>(GameObject.FindGameObjectsWithTag(Object));
        float shortDis = Vector3.Distance(transform.position, FindEnemy[0].transform.position);
        GameObject nearEnemy = FindEnemy[0];

        foreach (GameObject found in FindEnemy)
        {
            float Distance = Vector3.Distance(transform.position, found.transform.position);

            if (Distance < shortDis)
            {
                nearEnemy = found;
                shortDis = Distance;
            }
        }
        /**/
        enemy = nearEnemy;
    }
}
