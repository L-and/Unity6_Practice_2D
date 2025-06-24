using UnityEngine;

public class WirelessEarPhone2 : WirelessEarPhone
{
    public bool isNoiseCancelling;

    void Start()
    {
        name = "AirPod2";
        price = 150f;
        releaseYear = 2010;
        batterySize = 100f;
    }
    
    public virtual void NoiseCancelling()
    {
        isNoiseCancelling = !isNoiseCancelling;
        
        string msg = isNoiseCancelling ? "노이즈 캔슬링 On" : "노이즈 캔슬링 Off";
        Debug.Log(msg);
    }
}