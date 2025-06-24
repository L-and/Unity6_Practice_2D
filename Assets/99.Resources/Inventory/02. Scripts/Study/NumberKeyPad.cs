using UnityEngine;

public class NumberKeyPad : MonoBehaviour
{
    public Animator doorAnim;
    public GameObject doorLock;
    
    public string password; // 비밀번호 설정
    public string keyPadNumber; // 입력한 숫자

    public void OnInputNumber(string numString)
    {
        keyPadNumber += numString; // keyPadNumber = keyPadNumber + numString;

        Debug.Log($"{numString} 입력 -> 현재 입력 : {keyPadNumber}");

        Debug.Log(keyPadNumber.Length); // 입력한 길이
    }

    public void OnCheckNumber()
    {
        if (keyPadNumber == password)
        {
            doorAnim.SetTrigger("Door Open");
            
            doorLock.SetActive(false);
        }
        else
        {
            keyPadNumber = "";
            Debug.Log("비밀 번호 오류");
        }
    }
}