using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BombCounter : MonoBehaviour
{
    private int _bombsLeft;
    private int _tileLeft;
    private Grid _grid;

    private UnityEvent _gameOver;
    private UnityEvent _gameWin;

    public event UnityAction GameOver {
        add => _gameOver?.AddListener(value);
        remove => _gameOver?.RemoveListener(value);
    }

    public event UnityAction GameWin {
        add => _gameWin?.AddListener(value);
        remove => _gameWin?.RemoveListener(value);
    }

    [SerializeField] private TMP_Text _bombLeftText;

    private void Awake() {
        _gameOver = new UnityEvent();
        _gameWin = new UnityEvent();        
    }

    public void Init(int bombsLeft, int tileLeft, Grid grid) {
        _bombsLeft = bombsLeft;
        _tileLeft = tileLeft;
        _grid = grid;

        for (int i = 0; i < _grid.Height; i++) {
            for (int j = 0; j < _grid.Width; j++) {
                SubscribeOnTileEvents(_grid[i, j]);
            }
        }

        _bombLeftText.text = "Bomb Left: " + _bombsLeft;
    }

    public void SubscribeOnTileEvents(Tile tile) {
        tile.TileFlagged += OnTileFlagged;
        tile.TileRevealed += OnTileRevealed;
    }

    public void UnsubscribeOnTileEvents(Tile tile) {
        tile.TileFlagged -= OnTileFlagged;
        tile.TileRevealed -= OnTileRevealed;
    }

    private void OnTileFlagged(State state) {
        if (state == State.Flagged) {
            _bombsLeft--;
            _tileLeft--;
        } else {
            _bombsLeft++;
            _tileLeft++;
        }

        _bombLeftText.text = "Bomb Left: " + _bombsLeft;
    }

    private void OnTileRevealed(Tile tile) {
        if(tile is RegularTile temp) {
            if(temp.BombsCount == 0) {
                _grid.OpenNeighboursTiles(temp);
            }

            _tileLeft--;

            if (_tileLeft == _bombsLeft) {
                _gameWin?.Invoke();
            }
        } else {
            _gameOver?.Invoke();
        }
    }
}
