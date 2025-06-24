using UnityEngine;

public class Study_For : MonoBehaviour
{
    public int[] arrayInt = new int[5];
    
    void Start()
    {
        for (int i = 0; i < arrayInt.Length; i++)
        {
            Debug.Log(arrayInt[i]);
        }
    }
}