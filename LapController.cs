using UnityEngine;
using TMPro;

public class LapController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI lapText;
    [SerializeField] TextMeshProUGUI lapTimeText;
    public int lapCount = 1;
    public int maxLapCount = 3;
    public int checkpointCount = 0;
    public int maxCheckpointCount = 6; 
    public CarController carController;
    public EndLevelController endLevelController;

    public float elapsedTime = 0f;
    public bool stop = false;

    void Start()
    {
        lapText.text = string.Format("Lap: {0}/{1}", lapCount, maxLapCount);
        lapTimeText.enabled = false;
    }

    void Update()
    {
        if (!stop) elapsedTime += Time.deltaTime;
    }
    
    public void UpdateCheckpoint()
    {
        checkpointCount += 1;
        if (checkpointCount == maxCheckpointCount)
        {
            checkpointCount = 0;

            lapTimeText.enabled = true;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            lapTimeText.text = string.Format("Lap {0}: {1:00}:{2:00}", lapCount, minutes, seconds);
            elapsedTime = 0f;

            if (lapCount == maxLapCount)
            {
                lapCount = maxLapCount;
                carController.isFinished = true;
                endLevelController.Run();
            }
            else
            {
                lapCount += 1;
            }

            lapText.text = string.Format("Lap: {0}/{1}", lapCount, maxLapCount);
        }
    }
}
