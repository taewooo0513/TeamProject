using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header ("Offset")]
    public float offsetX = 0;
    public float offsetY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - offsetX, player.transform.position.y - offsetY, player.transform.position.z - 1);
    }
}
