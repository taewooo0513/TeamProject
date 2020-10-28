using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tutorialManager : MonoBehaviour
{
    int count = 0;
    public GameObject toggi;
    public GameObject lol;

    bool IsPause;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        void Start()
        {
            IsPause = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetMouseButtonDown(0))
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


        if (collision.gameObject.tag == "Chack")
            {
            Debug.Log("on");
            tap();
            toggi.SetActive(true);
            }

        if (collision.gameObject.tag == "Chack1")
        {
            tap();
            lol.SetActive(true);
        }

        if (collision.gameObject.tag == "Chack2")
        {
           
            tap();
            }

       
    }

    void tap()
    {
        if (IsPause == false)
        {
            if (count == 0)
            {

                Time.timeScale = 0;
                IsPause = true;
            }
            return;
        }
    }

}