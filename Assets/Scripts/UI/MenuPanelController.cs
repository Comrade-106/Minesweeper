using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private CanvasGroup _menuPanel;

    public void OnEasyButtonClick() {
        _game.Init(9, 9, 10);
        HidePanel();
    }

    public void OnMediumButtonClick() {
        _game.Init(16, 16, 40);
        HidePanel();
    }

    public void OnHardButtonClick() {
        _game.Init(30, 16, 99);
        HidePanel();
    }

    private void HidePanel() {
        _menuPanel.alpha = 0f;
        _menuPanel.blocksRaycasts = false;
    }

    public void OnExitButtonClick() {
        Application.Quit();
    }
}
