using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2StageMob_Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public float StopTime;
    private float CurTime = 0;
    private Player player;
    private bool Stop;
    private float PlayerSpeed;
    // Update is called once per frame
    void Init()

    {
        player = GameObject.Find("Player").GetComponent<Player>();
        PlayerSpeed = player.speed;
    }
    void Update()
    {
        if (Stop == true)
        {
            CurTime += Time.deltaTime;
            if (StopTime > CurTime)
            {
                player.speed = PlayerSpeed;
                Destroy(this);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            player.speed = 0;
            Stop = true;
    }
}
