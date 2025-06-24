using System.Collections;
using UnityEngine;

public class CharacterData
{
    public string name;
    public float hp;
    public float mp;
    public int level;

    public CharacterData(string name, float hp, float mp, int level)
    {
        this.name = name;
        this.hp = hp;
        this.mp = mp;
        this.level = level;
    }

    public void Save()
    {
        Debug.Log("현재 데이터 기준으로 저장");
    }

    public void Load()
    {
        Debug.Log("현재 데이터 기준으로 로드");
    }
}