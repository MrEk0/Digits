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
    private float additionalScorePoint = 0f;
    private float multiplyIndex = 1f;
    private int changeScoreCount = 0;

    private TextMeshProUGUI scoreText;
    private List<float> additionalPoints;

    private void Awake()
    {
        Instance = this;

        scoreText = GetComponent<TextMeshProUGUI>();
        SetScoreText();

        additionalPoints = new List<float>();
        additionalScorePoint = firstPointNumber;
        additionalPoints.Add(additionalScorePoint);
    }

    public void ChangeScoreAfterSumming(int tileNumber)
    {
        currentScore += GetAdditionalPoint(tileNumber);
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }

    private float GetAdditionalPoint(int tileNumber)
    {
        if (!additionalPoints.Contains(tileNumber))
        {
            additionalScorePoint += scoreOffset * multiplyIndex;
            changeScoreCount++;
            if (changeScoreCount % 2 == 0)
            {
                multiplyIndex *= 2;
            }

            additionalPoints.Add(additionalScorePoint);
        }

        return additionalPoints[tileNumber - 1];
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
