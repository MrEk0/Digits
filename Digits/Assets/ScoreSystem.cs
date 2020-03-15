using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }

    [SerializeField] float firstPointNumber = 50f;
    [SerializeField] float scoreOffset = 25f;

    private float currentScore = 0f;
    private float additionalPoint = 0f;
    private float multiplyIndex = 1f;
    private int changeScoreCount = 0;

    private TextMeshProUGUI scoreText;
    private List<float> additionalPoints;

    private void Awake()
    {
        Instance = this;

        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + currentScore.ToString();

        additionalPoints = new List<float>();
        additionalPoint = firstPointNumber;
        additionalPoints.Add(additionalPoint);
    }

    private float GetAdditionalPoint(int tileNumber)
    {
        if(!additionalPoints.Contains(tileNumber))
        {
            additionalPoint += scoreOffset * multiplyIndex;
            changeScoreCount++;
            if (changeScoreCount % 2 == 0)
            {
                multiplyIndex *= 2;
            }

            additionalPoints.Add(additionalPoint);
        }

        return additionalPoints[tileNumber-1];
    }

    public void ChangeScoreAfterSumming(int tileNumber)
    {
        currentScore += GetAdditionalPoint(tileNumber);
        scoreText.text = "Score: " + currentScore.ToString();    
    }

    public bool CanBuyNewTile(float price)
    {
        if (currentScore < price)
        {
            return false;
        }
        else
        {
            currentScore -= price;
            scoreText.text = "Score: " + currentScore.ToString();
            return true;
        }
    }
}
