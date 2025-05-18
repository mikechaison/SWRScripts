using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedometerController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI speedText;
    public RectTransform needle;
    public CarController carController;
    public float startPosition = 0f;
    public float endPosition = -180f;
    float playerSpeed;
    float maxPlayerSpeed;
    

    void Update()
    {
        playerSpeed = Mathf.Max(carController.carSpeed * 10, 0);
        maxPlayerSpeed = carController.speedLimit * 10;
        speedText.text = string.Format("{0:d}", (int)playerSpeed);
        UpdateNeedle();
    }

    void UpdateNeedle()
    {
        float temp = (float)(int)(playerSpeed) / (float)(int)(maxPlayerSpeed);
        float rotation = startPosition + temp * endPosition;
        needle.pivot = new Vector2(1f, 0.5f);
        needle.eulerAngles = new Vector3(0, 0, rotation);
    }
}
