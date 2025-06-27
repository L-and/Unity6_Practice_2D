using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Vector2 moveDir;
    public bool jump;
    public bool attack;
    void Update()
    {
        moveDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")).normalized;
        
        jump =  Input.GetButtonDown("Jump");
        
        attack = Input.GetButtonDown("Fire1");
    }
}
