using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int hp = 1;
    [Space (3f)]
    [Header("Bullet")]
    public GameObject bullet; //총알
    public Transform firePos; //발사 위치
    public int fireCount = 1; //발사 횟수
    public float fireTime;    //발사 시간

    private Animator anim;

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
        Invoke("Fire", fireTime);
    }
    
    void Fire()
    {
        for (int i = 0; i < fireCount; i++)
        {
            Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
        }
    }
}