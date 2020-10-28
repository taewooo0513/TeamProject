using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Normal Enemy")]
    public int hp = 1;

    [Header("Bind Enemy")]
    [Tooltip("플레이어 묶는 시간")]
    public float bindTime;
    [Tooltip("묶인 상태에서 풀려나기 위한 클릭 수")]
    public int bindCount;

    [Header("Any Enemy")]
    [Tooltip("거대화 적")]
    public bool canGiant = false;
    [Tooltip("맞으면 도망가는 적")]
    public bool runAway = false;
    [Tooltip("도망가는 거리")]
    public float runDis;

    private Animator anim;

    private bool giantInCam = false;

    private int firHp; //처음 HP

    void Start()
    {
        anim = GetComponent<Animator>();
        firHp = hp;
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (giantInCam)
        {
            if (transform.localScale.x < 1.5f && transform.localScale.y < 1.5f)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.5f, 1.5f), Time.deltaTime);
            }
        }
        if (runAway)
        {
            if (hp < firHp)
            {
                firHp = hp;
                transform.Translate(transform.position.x + runDis, 0, 0);
            }
        }
    }

    private void OnBecameVisible()
    {
        if (canGiant) giantInCam = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //anim.SetTrigger("IsAttack");
        }
    }
}
