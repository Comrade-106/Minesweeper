using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class Game : MonoBehaviour
{
    private Grid _grid;

    private int _width;
    private int _height;
    private int _bombCount;

    [SerializeField] private StopWatch _stopWatch;
    [SerializeField] private BombCounter _bombCounter;
    [SerializeField] private PausePanelController _pausePanel;
    [SerializeField] private ResultPanelController _resultPanel;

    public void Init(int width, int height, int bombCount) {
        _width = width;
        _height = height;
        _bombCount = bombCount;

        StartNewGame();
    }

    private void StartNewGame() {
        GenerateGrid();

        _stopWatch.StartStopwatch();

        _bombCounter.Init(_bombCount, _height * _width, _grid);
        _bombCounter.GameOver += OnGameOver;
        _bombCounter.GameWin += OnGameWin;

        _pausePanel.OnPauseClick += _grid.DisableAllTiles;
        _pausePanel.OnContinueClick += _grid.EnableAllTiles;
        _pausePanel.OnRestartClick += Restart;

        _resultPanel.OnRestartClick += Restart;
    }

    private void GenerateGrid() {
        var gridGenerator = GetComponent<GridGenerator>();
        gridGenerator.Init(_height, _width, _bombCount);
        _grid = gridGenerator.GenerateGrid();
    }

    public void Restart() {
        _pausePanel.OnPauseClick -= _grid.DisableAllTiles;
        _pausePanel.OnContinueClick -= _grid.EnableAllTiles;

        _grid.DestroyAll();
        GenerateGrid();

        _stopWatch.ResetStopwatch();
        _stopWatch.StartStopwatch();

        _bombCounter.Init(_bombCount, _height * _width, _grid);

        _pausePanel.OnPauseClick += _grid.DisableAllTiles;
        _pausePanel.OnContinueClick += _grid.EnableAllTiles;
    }

    private void OnGameWin() {
        _resultPanel.ShowWinResult(_stopWatch.GetTimeString());
    }

    private void OnGameOver() {
        _resultPanel.ShowLoseResult(_stopWatch.GetTimeString());
    }
}
