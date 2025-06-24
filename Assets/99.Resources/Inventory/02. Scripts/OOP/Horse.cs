using UnityEngine;

public class Horse : MonoBehaviour, IMove
{
    public bool isMounted;

    void Update()
    {
        if (isMounted)
            Move();
    }
    
    public void Move()
    {
        
    }
}