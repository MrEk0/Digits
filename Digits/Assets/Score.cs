using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] float firstPointNumber = 50f;
    [SerializeField] float scoreOffset = 25f;

    private float currentScore = 0f;
    private float additionalPoints = 0f;
    private float multiplyIndex = 1f;
    private int changeScoreCount = 0;

    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + currentScore.ToString();
        additionalPoints = firstPointNumber;
    }

    private void OnEnable()
    {
        TileManager.Instance.OnTileWereSummed += ChangeScoreText;
    }

    private void OnDisable()
    {
        TileManager.Instance.OnTileWereSummed -= ChangeScoreText;
    }

    private void ChangeScoreText()
    {
        currentScore += additionalPoints;

        additionalPoints += scoreOffset * multiplyIndex;
        changeScoreCount++;

        if(changeScoreCount%2==0)
        {
            multiplyIndex*=2;
        }

        scoreText.text = "Score: " + currentScore.ToString();    
    }
}
