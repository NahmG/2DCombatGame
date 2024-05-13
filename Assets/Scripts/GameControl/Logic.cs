using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    public Text scoreText;
    public Text roundText;
    public Text banditCount;
    public Text wizardCount;
    public FloatSO Score;
    public FloatSO Round;
    public Spawner spawner;


    private void Update()
    {
        scoreText.text = Score.Value.ToString();
        roundText.text = Round.Value.ToString();
        banditCount.text = spawner.banditNumber.ToString();
        wizardCount.text = spawner.wizardNumber.ToString(); 
    }

    public void AddScore(int score)
    {
        Score.Value += score;
    }
    
}
