using System;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    public enum CoinType { Gold, Green, Blue }
    public CoinType coinType;

    public float price;

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