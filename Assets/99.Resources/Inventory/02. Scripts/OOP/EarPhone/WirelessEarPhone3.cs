using UnityEngine;

public class WirelessEarPhone3 : WirelessEarPhone2
{
    public enum NoiseCancelType { Off, On, Around}
    public NoiseCancelType noiseCancelType;

    void Start()
    {
        name = "AirPodPro2";
        price = 300f;
        releaseYear = 2015;
        batterySize = 150f;
    }
    
    public void SetNoiseCancelType(NoiseCancelType type)
    {
        noiseCancelType = type;
    }
    
    public override void NoiseCancelling()
    {
        SetNoiseCancelType(noiseCancelType);
        
        base.NoiseCancelling();
    }
}