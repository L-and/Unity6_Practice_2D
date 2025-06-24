using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] List<GameObject> items = new List<GameObject>();

    public void AddItem(IItem item)
    {
        items.Add(item.Obj);
    }
}
