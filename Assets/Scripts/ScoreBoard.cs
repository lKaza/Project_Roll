using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreBoard : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    
    
    TextContainer m_TextContainer;
    private void Awake() {
        scoreText = GetComponent<TextMeshProUGUI>();
        m_TextContainer = GetComponent<TextContainer>();
    }
    int score=0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null)
            scoreText = gameObject.AddComponent<TextMeshProUGUI>();

        scoreText.text = score.ToString();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
    public void ScoreHit(int addedScore){
        score = score + addedScore;
        scoreText.text = score.ToString();
    }
    public void ScoreBySurviving(){
        Invoke("ScoreHit",5f);
    }
}
