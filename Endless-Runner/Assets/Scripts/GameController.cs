using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public ChallengeScroller myChallengeScroller;
    int score = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InvokeGameOver()
    {
        Invoke("ShowOverPanel", 1.0f);
    }

    void ShowOverPanel()
    {
        gameOverText.text = scoreText.text;
        scoreText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore(int amount)
    {
        myChallengeScroller.increaseSpeed();
        score += amount;
        scoreText.text = score.ToString();
    }

} 
