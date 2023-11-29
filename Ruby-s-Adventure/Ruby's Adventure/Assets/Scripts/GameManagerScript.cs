using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    //public ScoreManager Reboot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Reboot = FindObjectOfType<GameManagerScript>();
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();

        }

    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gameWin()
    {
       if(Input.GetKeyDown(KeyCode.R)) 
        {
            restart();

        }
    }
    
}
