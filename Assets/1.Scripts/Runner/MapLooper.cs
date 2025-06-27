using System;
using Runner;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLooper : MonoBehaviour
{
    private Vector3 _originalPos;
    public int loopOffset;
    
    void Start()
    {
        _originalPos =  transform.position;
    }

    void Update()
    {
        if (transform.position.x <= -loopOffset)
        {
            transform.position = _originalPos;
        }

        gameObject.transform.transform.Translate(Vector3.left * (GameManager.Instance.gameSpeed * Time.deltaTime));
    }
    
}
