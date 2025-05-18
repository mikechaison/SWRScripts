using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    public CarController carController;
    public AudioSource carAudio;
    public float minSpeed;
    public float maxSpeed;
    public float minPitch;
    public float maxPitch;
    private float curSpeed;
    private float pitchFromCar;

    void EngineSound()
    {
        curSpeed = Mathf.Max(carController.carSpeed, 0);
        pitchFromCar = curSpeed / (maxSpeed - minSpeed);
        carAudio.pitch = minPitch + (maxPitch - minPitch) * pitchFromCar;
    }

    void Update()
    {
        EngineSound();
    }
}
