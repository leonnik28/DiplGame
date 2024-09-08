using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Action OnTimerEnd;

    [SerializeField] private Text _text;
    [SerializeField] private Image _slider;

    private float _seconds;

    private bool _timerRunning = false;
    private float _timeRemaining;

    private void OnEnable()
    {
        _slider.fillAmount = 1f;
    }

    private void LateUpdate()
    {
        if (_timerRunning)
        {
            CountDown();
            DialSliderDown();
        }
    }

    public void StartTimer(float seconds)
    {
        _seconds = seconds;
            ResetTimer();
        _timerRunning = true;
    }

    private void CountDown()
    {
        if (_timeRemaining > 0.02f)
        {
            _timeRemaining -= Time.deltaTime;
            DisplayInTextObject();
        }
        else
        {
            _timeRemaining = 0;
            _timerRunning = false;
            OnTimerEnd?.Invoke();
            DisplayInTextObject();
        }
    }

    private void DialSliderDown()
    {
        float timeRangeClamped = Mathf.InverseLerp(_seconds, 0, _timeRemaining);
        _slider.fillAmount = Mathf.Lerp(1, 0, timeRangeClamped);
    }

    private void DisplayInTextObject()
    {
        string formattedTime = DisplayFormattedTime(_timeRemaining);
        _text.text = formattedTime;
    }

    private void ResetTimer()
    {
        _timerRunning = false;
        _timeRemaining = _seconds;
        _slider.fillAmount = 1f;
        DisplayInTextObject();
    }

    private string DisplayFormattedTime(float remainingSeconds)
    {
        return string.Format("{0:00}", Mathf.FloorToInt(remainingSeconds));
    }
}
