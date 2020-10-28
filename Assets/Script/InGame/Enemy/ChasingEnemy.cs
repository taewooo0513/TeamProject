using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public int hp = 1;

    [Header("Bullet")]
    public GameObject bullet; //총알
    public Transform firePos; //발사 위치
    public int fireCount = 1; //발사 횟수
    public float fireTime;    //첫 발사 시간
    public float fireDelay;   //발사 딜레이

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        InvokeRepeating("Fire", fireTime, fireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(GameObject.FindWithTag("Player").GetComponent<Player>().speed * Time.deltaTime, 0, 0);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MeleeEnemy")
        {
            hp--;
        }
    }
}
