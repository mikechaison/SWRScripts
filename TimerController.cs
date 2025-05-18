using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float elapsedTime;
    public bool stop = false;

    void Update()
    {
        if (!stop) elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
