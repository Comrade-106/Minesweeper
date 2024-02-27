using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PausePanelController : MonoBehaviour
{
    private UnityEvent _onPauseClick;
    private UnityEvent _onContinueClick;
    private UnityEvent _onRestartClick;

    public event UnityAction OnPauseClick {
        add => _onPauseClick?.AddListener(value);
        remove => _onPauseClick?.RemoveListener(value);
    }

    public event UnityAction OnContinueClick {
        add => _onContinueClick?.AddListener(value);
        remove => _onContinueClick?.RemoveListener(value);
    }

    public event UnityAction OnRestartClick {
        add => _onRestartClick?.AddListener(value);
        remove => _onRestartClick?.RemoveListener(value);
    }

    [SerializeField] private CanvasGroup _pausePanel;
    [SerializeField] private StopWatch _stopWatch;

    private void Awake() {
        _onContinueClick = new UnityEvent();
        _onPauseClick = new UnityEvent();
        _onRestartClick = new UnityEvent();
    }

    public void OnPauseButtonClick() {
        _pausePanel.alpha = 1f;
        _pausePanel.blocksRaycasts = true;

        _onPauseClick?.Invoke();

        _stopWatch.StopStopwatch();
    }

    public void OnContinueButtonClick() {
        _pausePanel.alpha = 0f;
        _pausePanel.blocksRaycasts = false;

        _onContinueClick?.Invoke();

        _stopWatch.StartStopwatch();
    }

    public void OnRestartButtonClick() {
        _onRestartClick?.Invoke();

        _pausePanel.alpha = 0f;
        _pausePanel.blocksRaycasts = false;
    }

    public void OnMainMenuButtonClick() {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        SceneManager.LoadScene(sceneName);
    }
}
