using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Normal Enemy")]
    public int hp = 1;
    [Space(3f)]

    [Header("Bind Enemy")]
    [Tooltip("플레이어 묶는 시간")]
    public float bindTime;
    [Tooltip("묶인 상태에서 풀려나기 위한 클릭 수")]
    public int bindCount;
    [Space(3f)]

    [Header("Any Enemy")]
    [Tooltip("거대화 가능 여부")]
    public bool canGiant = false;

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

        if (hp <= 0)
        {
            Destroy(gameObject);
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
