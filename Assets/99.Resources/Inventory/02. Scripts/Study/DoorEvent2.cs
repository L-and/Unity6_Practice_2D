using System;
using UnityEngine;

public class DoorEvent2 : MonoBehaviour
{
    private Animator anim;

    public GameObject doorLock;

    public string openKey;
    public string closeKey;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorLock.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorLock.SetActive(false);
        }
    }
}