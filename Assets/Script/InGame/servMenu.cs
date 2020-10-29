using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class servMenu : MonoBehaviour
{
    private bool IsMenue = false;
   
    public GameObject menue;
    private bool IsPause = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void menu()
    {

       
            menue.SetActive(true);
            IsMenue = true;
        

        
            
                Time.timeScale = 0;
                IsPause = true;
             
           
          
        
    }
    public void menuerun()
    {
        
            menue.SetActive(false);
            IsMenue = false;
       

            Time.timeScale = 1;
            IsPause = false;
 
            return;
        
    }
    // Update is called once per frame
    void Update()
    {
       


    }
}
