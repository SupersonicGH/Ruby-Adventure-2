using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadedBar : MonoBehaviour
{
    public Text loadedText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadedText.text = "Cogs Loaded: " + RubyController.isLoaded;
    }
}
