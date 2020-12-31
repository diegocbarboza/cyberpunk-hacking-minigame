using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to control the minigame time.
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField]
    private float time;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Image timerImage;

    [SerializeField]
    private StringEventChannelSO codeSelectedChannel;

    [SerializeField]
    private VoidEventChannelSO gameOverChannel;
    
    [SerializeField]
    private VoidEventChannelSO timeOutChannel;    

    private float totalTime;

    private bool hasStarted = false;
    
    public float Time { get => time; set => time = value; }

    private void Awake()
    {
        codeSelectedChannel.OnEventRaised += OnCodeSelected;
        gameOverChannel.OnEventRaised += OnGameOver;
    }

    private void Start()
    {
        totalTime = time; 
        UpdateUI();
    }

    private void OnDestroy()
    {
        codeSelectedChannel.OnEventRaised -= OnCodeSelected;
        gameOverChannel.OnEventRaised -= OnGameOver;
    }

    private void Update()
    {
        if (hasStarted)
        {
            time -= UnityEngine.Time.deltaTime;
            
            if (time <= 0)
            {
                timeOutChannel.RaiseEvent(gameObject);
                time = 0;
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        timerText.text = $"{time:00.00}";
        timerImage.fillAmount = time / totalTime;
    }

    private void OnCodeSelected(GameObject sender, string code)
    {
        hasStarted = true;
    }

    private void OnGameOver(GameObject sender)
    {
        enabled = false;
    }
}
