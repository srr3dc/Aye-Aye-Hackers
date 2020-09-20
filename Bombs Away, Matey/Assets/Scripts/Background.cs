using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Material material;
    Vector2 offset;
    Renderer myRenderer;
    public string sortingLayerName = string.Empty;
    public int orderInLayer = 0;

    public float xVel, yVel;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(xVel, yVel);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }

    public void SetSortingLayer()
    {
        if (sortingLayerName != string.Empty)
        {
            myRenderer.sortingLayerName = sortingLayerName;
            myRenderer.sortingOrder = orderInLayer;
        }
    }
}
