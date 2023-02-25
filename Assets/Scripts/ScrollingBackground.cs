using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    private Material material;
    public float speed;
    public float xOffset;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        material.mainTextureOffset = new Vector2(xOffset,0);
    }

    void FixedUpdate()
        {
            xOffset += speed;
        }
}
