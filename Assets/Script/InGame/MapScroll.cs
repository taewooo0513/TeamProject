using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    public GameObject PlayerPos;
    private MeshRenderer render;
    private float offset;
    public float speed;
    public float CurSpeed;
    // Start is called before the first frame update
    void Start()
    {
        CurSpeed = speed;
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(PlayerPos.transform.position.x-2,1,1);
        offset += Time.deltaTime * CurSpeed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
