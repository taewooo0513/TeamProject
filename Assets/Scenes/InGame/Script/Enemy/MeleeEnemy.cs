using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
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
        if (GameManager.isEnemyDie)
        {
            //애니메이션
            GameManager.isEnemyDie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CameraCollider")
        {
            anim.SetBool("IsAttack", true);
            Invoke("StopAtkAnim", 0.2f);
        }
    }
}
