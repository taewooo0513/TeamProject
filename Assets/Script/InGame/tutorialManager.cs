using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tutorialManager : MonoBehaviour
{
    private GameObject player;
    private Player PlayerS;
    int count = 0;
    public GameObject toggi;
    public GameObject lol;
    public bool UP = false;
    private bool IsPause =false;
    public int num;  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PlayerS = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void tap()
    {
        if (IsPause == false)
        {
            if (count == 0)
            {
                Time.timeScale = 0;
                IsPause = true;
                count++;
            }
            return;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            toggi.SetActive(false);
            lol.SetActive(false);
            if (IsPause == true)
            {
                Time.timeScale = 1;
                IsPause = false;
                count++;
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        count = 0;
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerS.isTouch = false;
            tap();
            toggi.SetActive(true);
        }
        
    }
  

}