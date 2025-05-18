using UnityEngine;

public class PauseController : MonoBehaviour
{
    public Canvas PauseCanvas;
    public AudioSource BackgroundMusic;
    public AudioSource EngineSound;
    public bool isPaused = false;

    public void Start()
    {
        PauseCanvas.enabled = false;
    }

    public void ContinueTime()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        PauseCanvas.enabled = true;
        Time.timeScale = 0;
        BackgroundMusic.Pause();
        EngineSound.Pause();
    }

    public void ContinueGame()
    {
        ContinueTime();
        PauseCanvas.enabled = false;
        BackgroundMusic.Play();
        EngineSound.Play();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }
    
}
