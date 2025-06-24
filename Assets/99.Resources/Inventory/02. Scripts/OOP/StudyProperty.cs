using UnityEditor;
using UnityEngine;

public class StudyProperty : MonoBehaviour
{
    [Header("속도")]
    [SerializeField] private float moveSpeed = 20f;
    
    [Space (10)]
    [Header("속도2")]
    [Range(0, 10)]
    [field: SerializeField]
    private float moveSpeed2 = 10f;
    public float MoveSpeed2
    {
        get { return moveSpeed2; }
        set { moveSpeed2 = value; }
    }


    [HideInInspector]
    public int level = 1;
    
}