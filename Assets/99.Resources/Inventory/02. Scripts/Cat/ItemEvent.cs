using UnityEngine;

public class ItemEvent : MonoBehaviour
{
    public enum ColliderType { Pipe, Apple, Both }
    public ColliderType colliderType;

    public GameObject pipe;
    public GameObject apple;
    public GameObject particle;
    
    public float moveSpeed = 3f;
    public float returnPosX = 15f;
    public float randomPosY;

    private Vector3 initPos;

    void Awake()
    {
        initPos = transform.localPosition;
    }
    
    void OnEnable()
    {
        SetRandomSetting(initPos.x);
    }
    
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x <= -returnPosX)
        {
            SetRandomSetting(returnPosX);
        }
    }

    private void SetRandomSetting(float posX)
    {
        randomPosY = Random.Range(-8f, -3.5f);
        transform.position = new Vector3(posX, randomPosY, 0);

        pipe.SetActive(false);
        apple.SetActive(false);
        particle.SetActive(false);
        
        colliderType = (ColliderType)Random.Range(0, 3);
        
        colliderType = ColliderType.Pipe;
        
        
        switch (colliderType)
        {
            case ColliderType.Pipe:
                pipe.SetActive(true);
                break;
            case ColliderType.Apple:
                apple.SetActive(true);
                break;
            case ColliderType.Both:
                pipe.SetActive(true);
                apple.SetActive(true);
                break;
        }
    }
    
}