using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Button : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] TextMeshProUGUI priceText;

    private float numberOfPurchases=0;
    private float price;

    public float Number { get; set; }

    private void Start()
    {
        FormButtonDescription();
    }

    private void FormButtonDescription()
    {
        numberText.text = Number.ToString();

        SetPrice();
    }

    public void MakeAPurchase()
    {
        bool isEnoughToBuy = ScoreSystem.Instance.CanBuyNewTile(price);

        if (isEnoughToBuy)
        {
            //drop a tile
            TileManager.Instance.SpawnBoughtTile(Number);
            numberOfPurchases++;
            SetPrice();
        }
    }

    private void SetPrice()
    {
        price = Number * 100 * Mathf.Pow((numberOfPurchases + 1), 2);
        priceText.text = "Buy: "+price.ToString();
    }
}
