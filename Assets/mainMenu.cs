using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    //This method changes the level to level 1 after the player pressed the play button
    public void playGame()
    {
        //Switch the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //This method ends the game when the player presses the quit button
    public void quitGame()
    {
        Application.Quit();
    }
}
