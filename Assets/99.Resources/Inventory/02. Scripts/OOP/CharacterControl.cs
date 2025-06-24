using System;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private IDropItem currentItem;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform grabPos;

    void Update()
    {
        Move();
        Interaction();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal"); // x축 오른쪽/왼쪽
        float v = Input.GetAxis("Vertical"); // z축 앞쪽/뒤쪽
        
        Vector3 dir = new Vector3(h, 0, v).normalized;
        
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void Interaction()
    {
        if (currentItem == null)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            currentItem.Use(); // 아이템 사용
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentItem.Drop(); // 아이템 버리기
            currentItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDropItem>() != null)
        {
            currentItem = other.GetComponent<IDropItem>();
            
            currentItem.Grab(grabPos); // 아이템 줍기
        }
    }
}