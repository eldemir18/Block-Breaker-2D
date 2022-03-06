using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks; 

    // Cached variable
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();    
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BreakeBlocks()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
            
    }

}
