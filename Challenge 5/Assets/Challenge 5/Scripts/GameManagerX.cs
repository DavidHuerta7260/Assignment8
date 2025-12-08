/*
 * David Huerta
 * Challenge 5
 * Game control and timer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;   // NEW
    public GameObject titleScreen;
    public Button restartButton;

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float timeRemaining = 60;   // NEW TIMER VARIABLE

    private float spaceBetweenSquares = 2.5f;
    private float minValueX = -3.75f;
    private float minValueY = -3.75f;

    // Start game WITH difficulty
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;

        UpdateScore(0);

        timeRemaining = 60;
        timerText.gameObject.SetActive(true);

        titleScreen.SetActive(false);
    }

    void Update()
    {
        if (isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeRemaining);

            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
        }
    }

    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);
        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // FIXED — Now displays Score: X
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // FIXED — Restart button appears on Game Over
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true); // FIXED
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
