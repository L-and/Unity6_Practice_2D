using UnityEngine;

public class EarPhone : MonoBehaviour
{
    public string name;
    public float price;
    public int releaseYear;

    public void PlayMusic()
    {
        Debug.Log("음악 재생");
    }
}