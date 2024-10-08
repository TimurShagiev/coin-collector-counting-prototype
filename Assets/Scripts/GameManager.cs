using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] ball;
    public GameObject player;

    public GameObject titleScreen;
    public Button restartButton;

    public Text scoreText;
    public TextMeshProUGUI collectBallsText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI livesText;

    private int horizontalBorder = 20;
    private int verticalRadiusBottom = 10;
    private int verticalRadiusTop = 35;
    private int scoreCount = 0;
    public int livesCount = 50;
    public int ballsToSpawn = 5;
    public int healAmount = 3;
    public float spawnRate = 5f;
    public float spawnRateChange = 0.05f;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnBalls ()
    {
        while (livesCount > 0)
        {
            for (int i = 0; i < ballsToSpawn; i++)
            {
                SpawnBall();
            }
            ballsToSpawn++;
            spawnRate -= spawnRateChange;
            GameObject[] particlesToDelete = GameObject.FindGameObjectsWithTag("Particle");
            foreach (GameObject particle in particlesToDelete)
            {
                Destroy(particle);
            }
            yield return new WaitForSeconds(spawnRate);
        }
        ShowScore();
    }

    void SpawnBall()
    {
        int randomIndex = Random.Range(0, ball.Length);
        int randomPosZ = Random.Range(-horizontalBorder, horizontalBorder);
        int randomPosY = Random.Range(verticalRadiusBottom, verticalRadiusTop);
        Instantiate(ball[randomIndex], new Vector3(0, randomPosY, randomPosZ), Random.rotation);
    }
    void ShowScore()
    {
        GameObject[] ballsToDelete = GameObject.FindGameObjectsWithTag("Coin").Concat(GameObject.FindGameObjectsWithTag("Bad")).ToArray().Concat(GameObject.FindGameObjectsWithTag("Live")).ToArray();
        foreach (GameObject ball in ballsToDelete)
        {
            Destroy(ball);
        }
        collectBallsText.gameObject.SetActive(false);
        currentScoreText.text = "Your Score: " + scoreCount;
        currentScoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void AddScore()
    {
        scoreCount++;
        scoreText.text = "Coins:" + scoreCount;
    }
    public void AddLives()
    {
        livesCount += healAmount;
        livesText.text = "Lives: " + livesCount;
    }
    public void ReduceLives(int damage)
    {
        livesCount -= damage;
        livesText.text = "Lives: " + livesCount;
    }
    public void CollectBalls()
    {
        StartCoroutine(SpawnBalls());
        player.gameObject.SetActive(true);
        collectBallsText.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
    }
    public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
