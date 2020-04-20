﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoHandler : MonoBehaviour
{
    public float endingAlpha;

    private TowerStats ts;
    private Text hireName;
    private Text blerb;
    private Text wage;
    private Image icon;
    private Image background;

    private Color changingName;
    private Color changingWage;
    private Color changingIcon;
    private Color changingBlerb;
    private Color changingBackground;

    private float alphaIndex;

    // Start is called before the first frame update
    void Awake()
    {
        hireName = transform.Find("Name").GetComponent<Text>();
        blerb = transform.Find("Blerb").GetComponent<Text>();
        wage = transform.Find("Wage").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        background = GetComponent<Image>();

        changingBlerb = blerb.color;
        changingWage = wage.color;
        changingIcon = icon.color;
        changingName = hireName.color;
        changingBackground = background.color;

        endingAlpha = endingAlpha/250;

        resetStuff();
    }

    public void UpdateTs(TowerStats ts)
    {
        this.ts = ts;

        blerb.text = ts.blurb;
        hireName.text = ts.hireName;
        icon.sprite = ts.icon;
        wage.text = "Wave Wage: " + ts.wage + " Coins";

        resetStuff();
    }

    public void FixedUpdate()
    {
        if (alphaIndex < 1)
        {
            alphaIndex += 0.025f;

            changingBlerb.a = alphaIndex;
            changingIcon.a = alphaIndex;
            changingName.a = alphaIndex;
            changingWage.a = alphaIndex;

            blerb.color = changingBlerb;
            hireName.color = changingName;
            wage.color = changingWage;
            icon.color = changingIcon;

            if (alphaIndex < endingAlpha)
            {
                changingBackground.a = alphaIndex;
                background.color = changingBackground;
            }
        }
    }

    public void resetStuff()
    {
        alphaIndex = 0;
        changingBlerb.a = alphaIndex;
        changingIcon.a = alphaIndex;
        changingName.a = alphaIndex;
        changingWage.a = alphaIndex;
        changingBackground.a = alphaIndex;

        blerb.color = changingBlerb;
        hireName.color = changingName;
        wage.color = changingWage;
        icon.color = changingIcon;
        background.color = changingBackground;
    }
}