using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    public void GameSceneLoad()
    {
        SceneManager.LoadScene("Mars", LoadSceneMode.Single);

    }
    public void GameQuitt()
    {
        Application.Quit();
    }
}
