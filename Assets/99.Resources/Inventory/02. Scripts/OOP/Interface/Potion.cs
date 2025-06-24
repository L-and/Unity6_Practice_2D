using System;
using UnityEngine;

public class Potion : MonoBehaviour, IItem
{
    public enum PotionType { Gold, Hp, Mp }
    public PotionType potionType;
    
    void OnMouseDown()
    {
        Get();
    }

    public GameObject Obj {
        get
        {
            return gameObject;
        }
        set
        {
            
        }
    }

    public void Get()
    {
        Debug.Log($"{this.name}을 획득했습니다.");
        Inventory.Instance.AddItem(this);
        gameObject.SetActive(false);
    }
}