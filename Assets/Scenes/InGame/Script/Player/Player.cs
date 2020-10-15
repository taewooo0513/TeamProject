﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player status")]
    public int hp = 3;
    [Space(2f)]
    public float speed = 5;

    private GameObject enemy;       //적
    private Animator anim;
    private SpriteRenderer sprRenderer;
    private bool isAttack = false;  //공격 상태

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
        if (!GameManager.isPlayerDie)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        //플레이어 죽음
        if(hp == 0)
        {
            anim.SetBool("IsDie", true);
            GameManager.isPlayerDie = true;
        }

        Attack();
    }
 
    private void Attack()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && !isAttack)
        {
            //if(GameObject.FindGameObjectWithTag("Bullet") != null)
            //{
            //    FindNearObject("Bullet");
            //}
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
                if (enemy.tag == "Enemy")
                {
                    if (transform.position.x < enemy.transform.position.x && enemy.transform.position.x <= transform.position.x + 1.8f)
                    {
                        GameManager.isEnemyDie = true;
                        Destroy(enemy);
                    }

                    anim.SetBool("IsAttack", true);
                    Invoke("StopAtkAnim", 0.15f);
                }
                //else if (enemy.tag == "Bullet")
                //{
                //    if (transform.position.x < enemy.transform.position.x && enemy.transform.position.x <= transform.position.x + 3f)
                //    {
                //        anim.SetBool("IsJump", true);
                //        Invoke("AtkOff", 0.4f);
                //    }
                //}
            }

            isAttack = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!anim.GetBool("IsHurt") && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet"))
        {
            anim.SetBool("IsHurt", true);
            Invoke("StopHurtAnim", 0.2f);
            hp -= 1;
        }
    }

    void StopHurtAnim()
    {
        anim.SetBool("IsHurt", false);
    }
    void StopJumpAnim()
    {
        anim.SetBool("IsJump", false);

        AtkOff();
    }
    void StopAtkAnim()
    {
        anim.SetBool("IsAttack", false);

        Invoke("AtkOff", 0.4f);
    }
    void AtkOff()
    {
        isAttack = false;
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