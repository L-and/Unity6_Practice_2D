using UnityEngine;

public class CatFollow : MonoBehaviour
{
    public Transform cat;
    public Vector3 offset; // 기준점을 맞추기 위한 변수
    
    void Update()
    {
        transform.position = cat.transform.position + offset;
    }
}