using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 public enum RGBColor
{
    None, Red, Green, Blue
}

public class GameBehaviour : MonoBehaviour
{
    public static int maxEnemies = 15;
    public static int enemiesToKill = maxEnemies;
    public static bool levelWon = false;
    public static bool levelLost = false;

    public GameObject button;
    public static RGBColor RandColor()
    {
        RGBColor[] values = { RGBColor.Red, RGBColor.Green, RGBColor.Blue };
        return (RGBColor)values.GetValue(Random.Range(0,values.Length));
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        levelWon = false;
        levelLost = false;
        enemiesToKill = maxEnemies;
        SceneManager.LoadScene(0);
    }

    public void WinLevel()
    {
        Time.timeScale = 0;
        levelWon = true;
    }

    public void LoseLevel()
    {
        Time.timeScale = 0;
        levelLost = true;
    }

    private void Start()
    {
        button.SetActive(false);
    }
    private void Update()
    {
        if (enemiesToKill <= 0) WinLevel();
        if (levelWon)
        {
            Time.timeScale = 0;
            button.transform.GetChild(0).GetComponent<Text>().text = "You won!";
            button.SetActive(true);
        }
        if (levelLost)
        {
            Time.timeScale = 0;
            button.transform.GetChild(0).GetComponent<Text>().text = "You lost..";
            button.SetActive(true);
        }
    }
}
