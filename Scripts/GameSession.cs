using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Config parameters
    
    [SerializeField] int pointsPerBlock;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI speedText;
    
    [SerializeField] bool autoPlay;
    
    // State variables
    static public int gameScore = 0;
    static public float gameSpeed = 1f;

    void Start()
    {
        UpdateScoreText();
        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        speedText.text = "Speed: x" + gameSpeed.ToString("F3");
    }

    private void UpdateScoreText()
    {
        scoreText.text = gameScore.ToString();
    }

    public void IncreaseSpeed(int maxHits) 
    {
        gameSpeed += Random.Range(0.005f, 0.0010f) * maxHits;
        Time.timeScale = gameSpeed;
        
        UpdateScoreText();
    }

    public void AddToScore(int maxHits) 
    {
        pointsPerBlock = Random.Range(30, 50);
        gameScore += Mathf.RoundToInt(pointsPerBlock * maxHits);
        
        UpdateSpeedText();
    }

    public bool IsAutoPlayEnable()
    {
        return autoPlay;
    }

}
