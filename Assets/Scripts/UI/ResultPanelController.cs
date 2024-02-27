using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ResultPanelController : MonoBehaviour
{
    private UnityEvent _onRestartClick;

    public event UnityAction OnRestartClick {
        add => _onRestartClick?.AddListener(value);
        remove => _onRestartClick?.RemoveListener(value);
    }

    [SerializeField] private CanvasGroup _resultPanel;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _resultTimeText;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _winAudioClip;
    [SerializeField] private AudioClip _loseAudioClip;

    private void Awake() {
        _onRestartClick = new UnityEvent();
    }

    public void ShowWinResult(string resultTime) {
        ShowResult("YOU WIN!", resultTime, _winAudioClip);
    }

    public void ShowLoseResult(string resultTime) {
        ShowResult("YOU LOSE!", resultTime, _loseAudioClip);
    }

    private void ShowResult(string result, string resultTime, AudioClip audioClip) {
        _resultPanel.alpha = 1f;
        _resultPanel.blocksRaycasts = true;

        _resultText.text = result;
        _resultTimeText.text = resultTime;

        _audioSource.PlayOneShot(audioClip);
    }

    public void OnRestartButtonClick() {
        _onRestartClick?.Invoke();

        _resultPanel.alpha = 0f;
        _resultPanel.blocksRaycasts = false;
    }

    public void OnMainMenuButtonClick() {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        SceneManager.LoadScene(sceneName);
    }
}
