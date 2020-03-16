using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] float numberOfButtons = 100;

    private RectTransform rectTransform;
    private VerticalLayoutGroup verticalLayout;

    private void Start()
    {
        FormScrollPanel();
        SetScrollHeight();
    }

    private void SetScrollHeight()
    {
        rectTransform = GetComponent<RectTransform>();
        verticalLayout = GetComponent<VerticalLayoutGroup>();

        float spacing = verticalLayout.spacing;
        float topOffset = verticalLayout.padding.top;
        float bottomOffset = verticalLayout.padding.bottom;
        float currentHeight = rectTransform.rect.height;
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;

        float height = ((buttonHeight + spacing) * numberOfButtons + topOffset + bottomOffset )- currentHeight;
        rectTransform.sizeDelta = new Vector2(0, height);
        rectTransform.localPosition = new Vector3(0, rectTransform.rect.yMin);
    }

    private void FormScrollPanel()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().Number = i + 1;
        }
    }
}
