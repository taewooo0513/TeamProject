using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player status")]
    public int hp = 3;
    [Space(2f)]
    public float speed = 5;

    private GameObject enemy;
    private Animator anim;
    private bool isAttack = false;
    private float curTime;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 이동
        if (!GameManager.isPlayerDie)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        //플레이어 죽음
        if(hp == 0)
        {
            GameManager.isPlayerDie = true;
        }

        Attack();
    }
 
    private void Attack()
    {
        if (Input.touchCount > 0 && !isAttack)
        {
            /*가장 가까운 적 찾기*/
            List<GameObject> FindEnemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            float shortDis = Vector3.Distance(transform.position, FindEnemy[0].transform.position);
            GameObject nearEnemy = FindEnemy[0];

            foreach (GameObject found in FindEnemy)
            {
                float Distance = Vector3.Distance(transform.position, found.transform.position);

                if (Distance < shortDis)
                {
                    nearEnemy = found;
                    shortDis = Distance;
                }
            }
            /**/
            enemy = nearEnemy;
            distance = Vector2.Distance(transform.position, enemy.transform.position);
            isAttack = true;
        }

        if(isAttack)
        {
            //애니메이션 플레이
            if (distance <= 0.5f)
            {
                GameManager.isEnemyDie = true;
                Destroy(enemy);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
        {
            anim.SetBool("IsHurt", true);
            Invoke("StopHurtAnim", 0.1f);
            hp -= 1;

        }
    }

    void StopHurtAnim()
    {
        anim.SetBool("IsHurt", false);
    }
}
