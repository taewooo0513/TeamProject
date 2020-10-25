using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class MapSich : MonoBehaviour
{
    public Button left, right;

    public List<Transform> MapBuffer;

    bool updateUI;

    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        UpdateMap();
    }

    void Update()
    {
        if (updateUI)
        {
            updateUI = false;
            UpdateUI();
        }
    }
    // Update is called once per frame
    void UpdateUI()
    {
        left.interactable = currentIndex > 0;
        right.interactable = currentIndex < MapBuffer.Count - 1;
    }

    void UpdateMap()
    {
        for (int i = 0; i < MapBuffer.Count; i++) 
        {
          
                MapBuffer[i].gameObject.SetActive(i == currentIndex);
            
        }
    }
    public void SwichMap(bool leftORright)
    {
        currentIndex += leftORright ? -1 : 1;
        updateUI = true;

        UpdateMap();
    }
}
