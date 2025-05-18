using UnityEngine;

public class UIController : MonoBehaviour
{
    public void Play()
    {
        SceneController.LoadScene(1);
    }

    public void ToMenu()
    {
        SceneController.LoadScene(0);
    }

    public void Restart()
    {
        SceneController.Restart();
    }

    public void NextLevel()
    {
        SceneController.NextLevel();
    }

    public void SceneLoad(int sceneIndex)
    {
        SceneController.LoadScene(sceneIndex);
    }
}
