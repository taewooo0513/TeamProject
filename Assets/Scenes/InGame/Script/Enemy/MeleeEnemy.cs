using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public bool canGiant = false;
    [Tooltip("거대화 가능 여부")]

    private Animator anim;
    private bool giantInCam = false;

    void Start()
    {

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (giantInCam)
        {
            if (transform.localScale.x < 1.5f && transform.localScale.y < 1.5f)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.5f, 1.5f), Time.deltaTime);
            }
        }
    }

    void OnBecameVisible()
    {
        if (canGiant) giantInCam = true;
    }

    private void OnDestroy()
    {
        if (GameManager.isEnemyDie)
        {
            //애니메이션 플레이
            GameManager.isEnemyDie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("IsAttack", true);
            Invoke("StopAtkAnim", 0.25f);
        }
    }

    void StopAtkAnim()
    {
        anim.SetBool("IsAttack", false);
    }
}
