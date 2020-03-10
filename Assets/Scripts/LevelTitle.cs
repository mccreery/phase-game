﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelTitle : MonoBehaviour
{
    private Text text;

    public float stayTime = 2.0f;
    public float fadeTime = 1.0f;

    private void Awake()
    {
        int i = LevelManager.Instance.CurrentLevel;
        NamedScene level = LevelManager.Instance.Levels[i];

        text = GetComponent<Text>();
        text.text = level.humanName;
    }

    private void Update()
    {
        Color textColor = text.color;
        textColor.a = Mathf.Clamp(-Time.timeSinceLevelLoad / fadeTime + stayTime / fadeTime + 1, 0, 1);

        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            text.color = textColor;
        }
    }
}