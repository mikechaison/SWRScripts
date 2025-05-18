using UnityEngine;
using TMPro;

public class EndLevelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endTimeText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    [SerializeField] TextMeshProUGUI recordText;
    public Canvas endLevelCanvas;
    public Canvas lvlCanvas;
    public TimerController timerController;
    public LapController lapController;
    public AudioSource victorySound;
    public int nextLvl;
    float gameTime;
    float bestTime;

    void Start()
    {
        lvlCanvas.enabled = true;
        endLevelCanvas.enabled = false;
    }

    public void Run()
    {
        lvlCanvas.enabled = false;
        recordText.enabled = false;
        gameTime = timerController.elapsedTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        endTimeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

        bestTime = PlayerPrefs.GetFloat("BestTime", 3000f);

        if (gameTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", gameTime);
            bestTime = PlayerPrefs.GetFloat("BestTime", 3000f);
            recordText.enabled = true;
        }

        minutes = Mathf.FloorToInt(bestTime / 60);
        seconds = Mathf.FloorToInt(bestTime % 60);
        bestTimeText.text = string.Format("Best time: {0:00}:{1:00}", minutes, seconds);

        timerController.stop = true;
        lapController.stop = true;
        endLevelCanvas.enabled = true;

        PlayerPrefs.SetInt("MaxLvl", nextLvl);
        victorySound.Play();
    }
}
