using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    private GameObject player;

    [HideInInspector]
    public float shakeTime; //흔드는 시간
    private float shakePower = 0.05f; //흔드는 힘
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z - 5);

        if (shakeTime > 0)
        {
            CameraShake();
        }
    }

    public void CameraShake()
    {
        pos = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z - 5);

        transform.position = Random.insideUnitSphere * shakePower + pos;
        shakeTime -= Time.deltaTime;
    }
}
