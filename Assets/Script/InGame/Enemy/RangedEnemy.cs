﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int hp = 1;

    [Header("Bullet")]
    public GameObject bullet; //총알
    public Transform firePos; //발사 위치
    public int fireCount = 1; //발사 횟수
    public float fireTime;    //첫 발사 시간
    public float fireDelay;   //발사 딜레이

    private Animator anim;

    private int count;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        anim.SetTrigger("IsAttack");
        InvokeRepeating("Fire", fireTime, fireDelay);
    }
    
    void Fire()
    {
        if(count < fireCount)
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