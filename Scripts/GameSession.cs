using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // Config parameters
    [Range(0.1f,100f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlock;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] bool autoPlay;
    string sceneName;

    // State variables
    [SerializeField] int gameScore = 0;

    //Cache reference

    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;

        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSceneName();
        Time.timeScale = gameSpeed;
    }

    private void CheckSceneName()
    {
        sceneName = SceneManager.GetActiveScene().name;
        //Debug.Log(sceneName);
        if (sceneName == "Game Over" || sceneName == "Win Screen")
        {
            finalScoreText.text = gameScore.ToString();
            scoreText.text = "";
        }
        else
        {
            scoreText.text = gameScore.ToString();
            finalScoreText.text = "";
        }
    }

    public void IncreaseSpeed(int maxHits) 
    {
        gameSpeed += Random.Range(0.005f, 0.015f) * maxHits;
    }

    public void AddToScore(int maxHits) 
    {
        pointsPerBlock = Random.Range(30, 50);
        gameScore += Mathf.RoundToInt(gameSpeed * pointsPerBlock * maxHits);
        scoreText.text = gameScore.ToString();
    }

    public void ResetGame()
    {
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public bool IsAutoPlayEnable()
    {
        return autoPlay;
    }

}
