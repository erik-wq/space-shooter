using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgMove : MonoBehaviour
{

    public Vector2 mousePos;

    [Range(-1f, 1f)]
    public float scrollSpeed;
    private float offsetx;
    private float offsety;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            scrollSpeed = -0.1f;
            offsetx += (Time.deltaTime * scrollSpeed);
            mat.SetTextureOffset("_MainTex", new Vector2(offsetx, offsety));
        }
        if (Input.GetKey(KeyCode.D))
        {
            scrollSpeed = 0.1f;
            offsetx += (Time.deltaTime * scrollSpeed);
            mat.SetTextureOffset("_MainTex", new Vector2(offsetx, offsety));
        }

        if (Input.GetKey(KeyCode.S))
        {
            scrollSpeed = -0.1f;
            offsety += (Time.deltaTime * scrollSpeed);
            mat.SetTextureOffset("_MainTex", new Vector2(offsetx, offsety));
        }
        if (Input.GetKey(KeyCode.W))
        {
            scrollSpeed = 0.1f;
            offsety += (Time.deltaTime * scrollSpeed);
            mat.SetTextureOffset("_MainTex", new Vector2(offsetx, offsety));
        }

    }

}