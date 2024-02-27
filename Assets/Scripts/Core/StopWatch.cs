using TMPro;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    private float _elapsedTime;
    private bool _stopwatchRunning;

    [SerializeField] private TMP_Text _stopwatchText;

    public float ElapsedTime {
        get => _elapsedTime;
    }

    public void StartStopwatch() {
        _stopwatchRunning = true;
    }

    public void StopStopwatch() {
        _stopwatchRunning = false;
    }

    public void ResetStopwatch() {
        _elapsedTime = 0f;
    }

    private void Update() {
        if (_stopwatchRunning) {
            _elapsedTime += Time.deltaTime;
            DisplayTime();
        }
    }

    private void DisplayTime() {
        _stopwatchText.text = GetTimeString();
    }

    public string GetTimeString() {
        float minutes = Mathf.FloorToInt(_elapsedTime / 60);
        float seconds = Mathf.FloorToInt(_elapsedTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
