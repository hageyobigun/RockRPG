﻿using UnityEngine;
using UniRx;
using TMPro;
using Game;

public class ResultButton : MonoBehaviour
{
    [SerializeField] private GameObject resultObj = null;
    [SerializeField] private TextMeshProUGUI winText = null;
    [SerializeField] private TextMeshProUGUI loseText = null;


    public void Start()
    {
        resultObj.SetActive(false);
        winText.enabled = false;
        loseText.enabled = false;

        //GameManeger.Instance.gameStateChanged
        //    .Where(state => state == GameManeger.GameState.Win)
        //    .Subscribe(_ =>
        //    {
        //        resultObj.SetActive(true);
        //        winText.enabled = true;
        //    });

        //GameManeger.Instance.gameStateChanged
        //    .Where(state => state == GameManeger.GameState.Lose)
        //    .Subscribe(_ =>
        //    {
        //        resultObj.SetActive(true);
        //        loseText.enabled = true;
        //    });
    }


    public void RetryButton()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

    public void BackOptionButton()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.Start;
    }

    public void TitleButton()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.Opening;
    }

}