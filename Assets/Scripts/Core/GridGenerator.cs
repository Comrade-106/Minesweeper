using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class GridGenerator : MonoBehaviour
{
    private int _width;
    private int _height;
    private int _bombCount;
    private AudioSource _audioSource;
    
    [SerializeField] private float _cellSize;
    [SerializeField] private float _cellOffset;

    [SerializeField] private BombTile _bombTilePrefab;
    [SerializeField] private RegularTile _regularTilePrefab;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(int height, int width, int bombsCount) {
        _height = height;
        _width = width;
        _bombCount = bombsCount;
    }

    public Grid GenerateGrid() {
        int[,] grid = new int[_height, _width];

        for(int i = 0; i < _bombCount;) {
            int h = Random.Range(0, _height);
            int w = Random.Range(0, _width);

            if(grid[h, w] != -1) {
                grid[h, w] = -1;
                UpdateAdjacentBombs(h, w, grid);
                i++;
            }
        }

        return SpawnTiles(grid);

        //PrintGrid(grid);
    }

    void UpdateAdjacentBombs(int h, int w, int[,] grid) {
        for (int i = h - 1; i <= h + 1; i++) {
            for (int j = w - 1; j <= w + 1; j++) {
                if (i >= 0 && j >= 0 && i < _height && j < _width) {
                    if (grid[i, j] == -1) continue;

                    grid[i, j]++;
                }
            }
        }
    }

    private Grid SpawnTiles(int[,] grid) {
        var tileGrid = new Grid(_height, _width);

        // –азмеры каждой клетки с учетом отступа
        float cellSize = _cellSize + _cellOffset;

        // ќбщий размер сетки
        float gridWidth = cellSize * _width;
        float gridHeight = cellSize * _height;

        // ѕолучаем размеры видимой области камеры
        float visibleHeight = 2f * Camera.main.orthographicSize;
        float visibleWidth = visibleHeight * Camera.main.aspect;

        // ¬ычисл€ем начальную позицию сетки дл€ ее центрировани€
        //Vector2 startPosition = new Vector2(
        //    (visibleWidth - gridWidth) / 2 - cellSize / 2,
        //    (visibleHeight - gridHeight) / 2 - cellSize / 2
        //);

        Vector2 startPosition = new Vector2(-gridWidth / 2, -gridHeight / 2);

        for (int i = 0; i < _height; i++) {
            for (int j = 0; j < _width; j++) {
                Vector2 spawnPosition = new Vector2(j * cellSize, i * cellSize) + startPosition;
                if (grid[i, j] == -1) {
                    var temp = Instantiate(_bombTilePrefab, spawnPosition, Quaternion.identity, transform);
                    temp.Init(_audioSource);
                    tileGrid[i, j] = temp;
                } else {
                    var temp = Instantiate(_regularTilePrefab, spawnPosition, Quaternion.identity, transform);
                    temp.Init(_audioSource, grid[i, j]);
                    tileGrid[i, j] = temp;
                }
            }
        }

        // ÷ентрируем сетку относительно камеры
        //transform.position = new Vector3(-visibleWidth / 2 + cellSize / 2, -visibleHeight / 2 + cellSize / 2, 0);

        return tileGrid;
    }

    private void PrintGrid(int[,] grid) {
        string temp = "";

        for(int i = 0; i < _height; i++) {
            for(int j = 0; j < _width; j++) {
                temp += grid[i, j] + " ";
            }
            temp += "\n";
        }

        Debug.Log(temp);
    }
}
