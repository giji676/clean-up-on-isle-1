using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour {
    #region Singleton

    public static GameManager instance;

    private void Awake() {
        instance = this;
    }

    #endregion
    
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private string gameWon;
    [SerializeField] private string gameOver;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int cansSoldGoal;
    [SerializeField] private Image timeImage;
    private PlayerUI playerUI;
    private int cansSold;
    private float time;
    private bool gameFinished = false;

    private void Start() {
        time = maxTime;
        playerUI = playerObject.GetComponent<PlayerUI>();
    }

    private void Update() {
        if (cansSold >= cansSoldGoal) GameWon();
        if (gameFinished) return;
        UpdateTimer();
    }
    
    private void UpdateTimer() {
        time -= Time.deltaTime;
        if (time <= 0) GameOver();

        timeText.text = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
        timeImage.fillAmount = Mathf.InverseLerp(0, maxTime, time);
    }

    public void UpdateSales(int sold) {
        cansSold += sold;
        playerUI.UpdateProgressBar(cansSold, cansSoldGoal);

        if (cansSold >= cansSoldGoal) GameWon();
    }
    
    public void GameOver() {
        gameFinished = true;
        string newText = $"{gameOver} with {cansSold} coins collected out of {cansSoldGoal}";
        text.text = newText;
        StartCoroutine(LoadMainMenu());
    }

    private void GameWon() {
        gameFinished = true;
        int timeLeft = (int)(maxTime-time);
        string newText = $"{gameWon} in {timeLeft} seconds";
        text.text = newText;
        StartCoroutine(LoadMainMenu());
    }
    
    private IEnumerator LoadMainMenu() {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");
    }
}