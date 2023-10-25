using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;

public class LivesCounterScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentValue = 0;
    private TextMeshProUGUI livesText;

    public PlayerMove playerMoveScript;
    public void updateValue() {
        currentValue = playerMoveScript.currentLives;
        livesText.text = "Lives:" + currentValue.ToString();
    }
    void Start() {
       livesText = GetComponent<TextMeshProUGUI>();
       updateValue();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
