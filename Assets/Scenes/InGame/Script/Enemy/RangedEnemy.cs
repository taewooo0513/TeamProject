using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
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

    }

    private void OnDestroy()
    {
        if(GameManager.isEnemyDie)
        {
            //애니메이션
            GameManager.isEnemyDie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "CameraCollider")
        {
            anim.SetBool("IsAttack", true);
            Invoke("StopAtkAnim", 0.2f);
            Invoke("Fire", fireTime);
        }
    }

    void StopAtkAnim()
    {
        anim.SetBool("IsAttack", false);
    }
    
    void Fire()
    {
        for (int i = 0; i < fireCount; i++)
        {
            Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
        }
    }
}
