using UnityEngine;

public class WhileLoop : MonoBehaviour
{
    private int count = 0;

    void Start()
    {
        while (count <= 10)
        {
            count++;

            // 3의 배수 조건
            if (count % 3 == 0) // count를 3으로 나머지 연산을 했을 때, 값이 0이 나오는 경우
            {
                Debug.Log("박수 짝!");
                continue;
            }
            
            Debug.Log(count);
        }
    }
}