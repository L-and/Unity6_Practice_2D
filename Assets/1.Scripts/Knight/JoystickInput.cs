using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 moveDir;
    
    [SerializeField] private GameObject backgroundUI;
    [SerializeField] private GameObject handleUI;

    private Vector2 _startPos, _currPos;

    [SerializeField] private float joystickBound = 100f;
    
    void Start()
    {
        backgroundUI.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundUI.SetActive(true);
        backgroundUI.transform.position = eventData.position;
        _startPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handleUI.transform.localPosition = Vector2.zero;
        backgroundUI.SetActive(false);
        
        moveDir = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _currPos = eventData.position;
        Vector2 dragDir = _currPos - _startPos;

        float maxDist = Mathf.Min(dragDir.magnitude, joystickBound);

        handleUI.transform.position = _startPos + dragDir.normalized * maxDist;
        
        moveDir = dragDir.normalized;
    }
}
