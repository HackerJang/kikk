using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
    }
}
