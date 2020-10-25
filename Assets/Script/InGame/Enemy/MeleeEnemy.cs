using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int hp = 1;
    [Space(3f)]
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

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        if (canGiant) giantInCam = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("IsAttack");
        }
    }
}
