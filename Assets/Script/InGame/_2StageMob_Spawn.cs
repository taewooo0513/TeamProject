using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2StageMob_Spawn : MonoBehaviour
{
    public GameObject obj;
    private GameObject Enemy;
    public Transform spanwPos;
    // Start is called before the first frame update
    public float StopTime;
    private float CurTime = 0;
    private float CCurTime = 0;
    int count = 1;
    private Player player;
    private bool Stop;
    private float PlayerSpeed;
    private List<float> ListVector;
    private List<float> _ListVector;
    private bool D;
    int t = 0;
    int tt = 0;
    // Update is called once per frame

    void Start()
    {
        ListVector = new List<float>();
        _ListVector = new List<float>();
        Enemy = GameObject.Find("Stage1-2");
        player = GameObject.Find("Player").GetComponent<Player>();
        PlayerSpeed = player.speed;
        for (int i = 0; i < 80; i++)
        {
            ListVector.Add(Mathf.Lerp(7.41f, -9.5f, 0.0125f * i));
        }
        for (int i = 0; i < 20; i++)
        {
            _ListVector.Add(Mathf.Lerp(-9.5f, -2f, 0.05f * i));
        }
    }
    void Update()
    {
        if( D == true)
        {
            CurTime += Time.deltaTime;

            if (CurTime > 0.05&&tt < 19)
            {
                tt++;
                CurTime = 0;
            }
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().offsetX = _ListVector[tt];
        }

        if (Stop == true)
        {
            CurTime += Time.deltaTime;
            CCurTime += Time.deltaTime;

            if (CurTime > 0.025)
            {
                t++;
                CurTime = 0;
            }
            if(CCurTime > 5)
            {
                D = true;
                Stop = false;
                player.speed = PlayerSpeed;
                Enemy.GetComponent<Enemy>().runSpeed = 0;
            }
            if (t < 80)
            {
                GameObject.FindWithTag("MainCamera").GetComponent<Camera>().offsetX = ListVector[t];

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(count == 1)
        {
        Instantiate(obj,spanwPos.position,Quaternion.Euler(0,0,0));
        count--;
        player.speed = 0;
        Stop = true;

        }
    }

}
