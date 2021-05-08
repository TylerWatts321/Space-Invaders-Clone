using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public EnemyTracker enemyTracker;
    public bool isPaused;
    public bool gameOver = false;
    public bool isReloadingScene;
    //public bool ignoreBarriers = true;
    //public Text gameOverText;
    public Player player;
    public Text gameOverText;
    public Image secondLife;
    public Image thirdLife;
    public int LIVES;
    public int SCORE;
    public int HISCORE;
    public int SPEED_MODIFIER;

    private void Awake()
    {
        MakeSingleton();
    }
    private void Update()
    {
        player = FindObjectOfType<Player>();
        CheckPauseStatus();
        CheckRestartStatus();
        CheckForEndInput();
        UpdateLives();
    }

    public void UpdateScore(Character character)
    {
        player.score += character.score;
    }

    public void UpdateLives()
    {
        if (player.lives == 2)
        {
            secondLife.gameObject.SetActive(false);
        }

        if (player.lives == 1)
        {
            thirdLife.gameObject.SetActive(false);
        }
    }

    private void CheckPauseStatus()
    {
        if (Time.timeScale == 0)
            isPaused = true;
        else
            isPaused = false;
    }

    private void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckRestartStatus()
    {
        if (EnemyTracker.numEnemies == 0 && !isReloadingScene)
        {
            isReloadingScene = true;
            Invoke("ReloadScene", 1.0f);
            Debug.Log("Reloading Scene!");
        }
    }
    public void ReloadScene()
    {
        UpdateHiScore();
        if (!gameOver)
        {
            player.SavePlayer();
        }
        else
        {
            SCORE = 0;
            LIVES = 3;
            secondLife.gameObject.SetActive(true);
            thirdLife.gameObject.SetActive(true);
        }
        AudioManager.instance.StopPlaying("UFOhigh");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isReloadingScene = false;
        gameOver = false;
    }

    public void GameOver()
    {
        if (!gameOver)
            Invoke("EndGame", 0.5f);
    }

    public void EndGame()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        gameOver = true;
    }


    public void CheckForEndInput()
    {
        if(gameOver && Input.GetKeyDown("y"))
        {
            gameOverText.gameObject.SetActive(false);
            ReloadScene();
            Time.timeScale = 1f;
        }

        if (gameOver && Input.GetKeyDown("n"))
        {
            Application.Quit();
        }
    }
    
    public void UpdateHiScore()
    {
        if (player.score > HISCORE)
            HISCORE = player.score;
    }
    public void StartPause(float pauseTime)
    {
        StartCoroutine(PauseGame(pauseTime));
    }
    
    public IEnumerator PauseGame(float pauseTime)
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;

        while(Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }

        Time.timeScale = 1f;
    }
}
