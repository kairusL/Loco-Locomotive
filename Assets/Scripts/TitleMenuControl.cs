﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuControl : MonoBehaviour
{

    public GameObject[] PlayerStatuses;

    public void LoadLevelScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void UpdatePlayerStatus(int playerIndex, string controlScheme)
    {
        string statusText = null;
        //Color color = Color.white;
        Sprite sprite = null;
        if (!string.IsNullOrEmpty(controlScheme))
        {
            sprite = GameManager.Instance.sprites[playerIndex];
            
        }

        switch (controlScheme)
        {
            case "WASD":
                statusText = "Ready!\nWASD Keyboard";
                break;
            case "Arrows":
                statusText = "Ready!\nArrows Keyboard";
                break;
            case "Gamepad":
                statusText = "Ready!\nGamepad";
                break;
            case null:
                statusText = "Not\nConnected";
                break;
        }

        PlayerStatuses[playerIndex].GetComponentInChildren<Text>().text = statusText;
        PlayerStatuses[playerIndex].GetComponentInChildren<Image>().sprite = sprite;
    }
}
