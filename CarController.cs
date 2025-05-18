using UnityEngine;
using System;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
        public float outOfTheRoad;
        public float outOfTheWay;
    }

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float swampSmoothness = 10.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public float idleSpeedThreshold = 0.1f; // Below this is considered "not moving"
    public float idleTimeLimit = 1f; // Seconds
    public float speedLimit = 18f;
    public float carSpeed = 0f;

    private Vector3 _centerOfMass = new Vector3(0, -1, 0);

    public List<Wheel> wheels;

    public Vector3 teleportPosition;
    public Quaternion teleportRotation;

    float moveInput;
    float steerInput;
    float moveSpeed;

    public Rigidbody carRb;
    public AudioSource crashSound;
    public bool isFinished = false;

    void Start()
    {
        carRb.centerOfMass = _centerOfMass;
        teleportPosition = carRb.transform.position;
        teleportRotation = carRb.transform.rotation;
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        CheckStuck();
    }

    void GetInputs()
    {
        if (!isFinished)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        carSpeed = 0f;
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;

            float wheelRadius = wheel.wheelCollider.radius;
            float rpmLimit = (60f * speedLimit) / (2 * Mathf.PI * wheelRadius);
            float rpm = Mathf.Min(wheel.wheelCollider.rpm, rpmLimit);
            float wheelSpeed = (2 * Mathf.PI * wheelRadius) * (rpm / 60f);
            carSpeed += wheelSpeed;
        }
        carSpeed = carSpeed / 4;
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.9f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                bool onSwamp = false;
                WheelHit hit;

                if (wheel.wheelCollider.GetGroundHit(out hit))
                {
                    if (hit.collider != null && hit.collider.name == "Plane")
                    {
                        onSwamp = true;
                    }
                }

                if (onSwamp)
                {
                    wheel.wheelCollider.brakeTorque = swampSmoothness * brakeAcceleration;
                }
                else
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }
    }

    void CheckStuck()
    {
        if (!isFinished)
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                var wheel = wheels[i];
                WheelHit hit;
                if (wheel.wheelCollider.GetGroundHit(out hit))
                {
                    if (hit.collider != null && hit.collider.name == "Plane")
                    {
                        wheel.outOfTheRoad += Time.deltaTime;
                        if (wheel.outOfTheRoad >= wheel.outOfTheWay)
                        {
                            wheel.outOfTheRoad = 0f;
                            carRb.transform.position = teleportPosition;
                            carRb.transform.rotation = teleportRotation;
                            wheels[i] = wheel;
                            break;
                        }
                    }
                    else
                    {
                        wheel.outOfTheRoad = 0f;
                    }
                }
                wheels[i] = wheel;
            }
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        crashSound.Play();
    }
}
