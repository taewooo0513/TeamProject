﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "MeleeEnemy" || collision.gameObject.tag == "RangedEnemy" || collision.gameObject.tag == "BindEnemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
