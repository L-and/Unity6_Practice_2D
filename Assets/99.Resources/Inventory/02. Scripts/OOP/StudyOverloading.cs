using System;
using UnityEngine;

public class StudyOverloading : MonoBehaviour
{
    public GameObject characterPrefab;
    private CharacterData data;

    void Awake()
    {
        data = new CharacterData("플레이어1", 100f, 50f, 30); // 캐릭터 데이터
        
        data.Load();
    }
    void Start()
    {
        GameObject character = Instantiate(characterPrefab); // 캐릭터 오브젝트 생성
        
        character.GetComponent<Character>().name = data.name;
        // character.GetComponent<Character>().hp = data.hp;
        // character.GetComponent<Character>().mp = data.mp;
        // character.GetComponent<Character>().level = data.level;
    }

    private void OnApplicationQuit()
    {
        data.Save();
    }
}