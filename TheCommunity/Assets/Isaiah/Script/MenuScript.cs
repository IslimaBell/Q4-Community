using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private PresentationSize presentation;
    [SerializeField]
    private ComputerSize computer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MainMenuBad()
    {
        SceneManager.LoadScene("MainMenuBad");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void CreditsBad()
    {
        SceneManager.LoadScene("CreditsForBadTitle");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void Presentation()
    {
        presentation.pressedpres = true;
        computer.pressedCom = false;
    }
    public void Computer()
    {
        presentation.pressedpres = false;
        computer.pressedCom = true;
    }
}
