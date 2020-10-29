﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyKind
{
    MELEE,
    GIANT,
    RUNNER,
    CHASING,
    RANGED
}


public class Enemy : MonoBehaviour
{
    public EnemyKind kind;

    public int hp = 1;

    [Header("Bind Enemy")]
    [Tooltip("플레이어 묶는 시간")]
    public float bindTime;
    [Tooltip("묶인 상태에서 풀려나기 위한 클릭 수")]
    public int bindCount;

    [Header("Runner Enemy")]
    [Tooltip("도망가는 거리")]
    public float runDis;

    [Header("Ranged Enemy")]
    public GameObject bullet; //총알
    public Transform firePos; //발사 위치
    public int fireCount = 1; //발사 횟수
    public float fireTime;    //첫 발사 시간
    public float fireDelay;   //발사 딜레이

    private Animator anim;

    private bool giantInCam;
    private int firHp;
    private int count;

    void Start()
    {
        anim = GetComponent<Animator>();

        switch (kind)
        {
            case EnemyKind.RUNNER:
                firHp = hp;
                break;
            case EnemyKind.CHASING:
                InvokeRepeating("Fire", fireTime, fireDelay);
                break;
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        switch (kind)
        {
            case EnemyKind.GIANT:
                Giant();
                break;
            case EnemyKind.RUNNER:
                Runner();
                break;
        }
    }

    private void OnBecameVisible()
    {
        switch (kind)
        {
            case EnemyKind.GIANT:
                giantInCam = true;
                break;
                
            case EnemyKind.CHASING:
                GameObject.FindWithTag("MainCamera").GetComponent<Camera>().offsetX = -GameObject.FindWithTag("MainCamera").GetComponent<Camera>().offsetX;
                break;

            case EnemyKind.RANGED:
                anim.SetTrigger("IsAttack");
                InvokeRepeating("Fire", fireTime, fireDelay);
                break;
        }
    }

    private void OnDestroy()
    {
        switch(kind)
        {
            case EnemyKind.CHASING:
                GameObject.FindWithTag("MainCamera").GetComponent<Camera>().offsetX = -GameObject.FindWithTag("MainCamera").GetComponent<Camera>().offsetX;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //anim.SetTrigger("IsAttack");
        }
    }

    private void Giant()
    {
        if (giantInCam)
        {
            if (transform.localScale.x < 1.5f && transform.localScale.y < 1.5f)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.5f, 1.5f), Time.deltaTime);
            }
        }
    }
    private void Runner()
    {
        if (hp < firHp)
        {
            firHp = hp;
            transform.Translate(transform.position.x + runDis, 0, 0);
        }
    }
    private void Chasing()
    {
        transform.Translate(GameObject.FindWithTag("Player").GetComponent<Player>().speed * Time.deltaTime, 0, 0);
    }

    void Fire()
    {
        if (count < fireCount)
        {
            Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
            count++;
        }
        else
        {
            CancelInvoke("Fire");
        }
    }
}