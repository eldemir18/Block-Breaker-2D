using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] Sprite[] hitSprites;
    int maxHits, spriteIndex;

    // Cached variables
    Level level;
    GameSession gameStatus; 

    // State variables
    [SerializeField] int timesHit = 0;
    

    private void Start()
    {
        maxHits = hitSprites.Length + 1;
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
            level.CountBlocks();     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
            HandleHit();  
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
            DestroyBlock();

        else
            ShowNextSprite();
    }

    private void ShowNextSprite()
    {
        spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];

        else
            Debug.LogError("Block sprite is missing" + gameObject.name);    
    }

    private void AddToScore()
    {
        gameStatus.AddToScore(maxHits);
    }

    private void IncreaseSpeed()
    {
        gameStatus.IncreaseSpeed(maxHits);
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.1f);
        AddToScore();
        Destroy(gameObject);
        level.BreakeBlocks();
        IncreaseSpeed();
    }
}
