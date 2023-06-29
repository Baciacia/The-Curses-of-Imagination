using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject canvas;
    public void StartNewGame()
    {
        SceneManager.LoadScene("SampleScene"); 
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Menu2");
    }
    // public void StartAgain()
    // {
    //     SceneManager.LoadScene("SampleScene"); 
    //     canvas.SetActive(false);
    //     player.setStatus();
    //     canvasGame.SetActive(true);
    // }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Quit()
    {
         Application.Quit();
    }
}