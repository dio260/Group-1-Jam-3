using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endMenu : MonoBehaviour
{
    //This method changes the level to level 1 after the player pressed the play button
    public void replayGame()
    {
        //Switch the level
        SceneManager.LoadScene(1);
    }

    //This method ends the game when the player presses the quit button
    public void quitGame()
    {
        Application.Quit();
    }
}
