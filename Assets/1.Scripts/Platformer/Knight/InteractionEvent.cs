using System.Collections;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public enum InteractionType {SIGN, DOOR, NPC}

    public InteractionType type;

    public GameObject popUp;

    public FadeRoutine fade;
    
    public GameObject map;
    public GameObject house;

    public Vector3 inDoorPos;
    public Vector3 outDoorPos;
    private bool _isHouse;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interaction(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            popUp.SetActive(false);
        }
    }

    void Interaction(Transform player)
    {
        switch (type)
        {
            case InteractionType.SIGN:
                Debug.Log("[Interaction] Sign Popup!");
                popUp.SetActive(true);
                break;
            case InteractionType.DOOR:
                StartCoroutine(DoorCoroutine(player));
                break;
            case InteractionType.NPC:
                Debug.Log("[Interaction] Sign Popup!");
                popUp.SetActive(true);
                break;
        }
    }

    IEnumerator DoorCoroutine(Transform player)
    {
        yield return StartCoroutine(fade.Fade(0.5f, Color.black, true));

        map.SetActive(_isHouse);
        house.SetActive(!_isHouse);
        var pos = _isHouse ? outDoorPos : inDoorPos;
        player.position = pos;
        _isHouse = !_isHouse;

        yield return StartCoroutine(fade.Fade(0.5f, Color.black, false));
    }
}
