using System;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    public PinballManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Rigidbody2D leftBarRb;
    public Rigidbody2D rightBarRb;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftBarRb.AddTorque(30f);
        }
        else
        {
            leftBarRb.AddTorque(-30f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightBarRb.AddTorque(-30f);
        }
        else
        {
            rightBarRb.AddTorque(30f);
        }

    }

}
