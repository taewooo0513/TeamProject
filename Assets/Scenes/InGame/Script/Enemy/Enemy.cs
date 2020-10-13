using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bullet;
    public Transform firePos;

    private Animator anim;
    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        if(GameManager.isEnemyDie)
        {
            //애니메이션, 이펙트 발생
            GameManager.isEnemyDie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "CameraCollider")
        {
            anim.SetBool("IsAttack", true);
            Invoke("StopAtkAnim", 0.2f);
            Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
        }
    }

    void StopAtkAnim()
    {
        anim.SetBool("IsAttack", false);
    }
}
