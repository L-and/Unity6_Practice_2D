using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Study_Switch : MonoBehaviour
{
    public enum CalculationType { Plus, Minus, Multiply, Divide } // 열거형 생성
    public CalculationType calculationType;
    
    public bool isPlus;
    public bool isMinus;
    public bool isMultiply;
    public bool isDivide;

    public int input1, input2, result;

    void Start()
    {
        Debug.Log($"계산 결과 : {Calculation()}");
    }

    private int Calculation()
    {
        switch (calculationType)
        {
            case CalculationType.Plus:
                result = input1 + input2;
                break;
            case CalculationType.Minus:
                result = input1 - input2;
                break;
            case CalculationType.Multiply:
                result = input1 * input2;
                break;
            case CalculationType.Divide:
                result = input1 / input2;
                break;
        }

        return result;
    }
}