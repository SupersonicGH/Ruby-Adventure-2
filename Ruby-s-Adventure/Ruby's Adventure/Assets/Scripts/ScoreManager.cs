using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static int scoreCount;
    public GameManagerScript winManager;
    public GameObject winTextObject;

    private bool isWin;

    public GameManagerScript restartScript;


    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false) ;
       restartScript = FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Fixed Robots: " + scoreCount;
       
        if (scoreCount >= 2) 
        {
            winTextObject.SetActive(true);
            restartScript.gameWin();
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reboot();

            }



        }
        
        
    }
    void Reboot()
    {
        winTextObject.SetActive(false);
        scoreCount = 0;
    }

    
   

        
}
