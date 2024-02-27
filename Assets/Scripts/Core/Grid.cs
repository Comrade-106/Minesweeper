using System;
using UnityEngine;

public class Grid
{
    private int _height, _width;
    private Tile[,] _grid;

    public int Height { get => _height; }
    public int Width { get => _width; }

    public Tile this[int i, int j] {
        get {
            if (i >= 0 && j >= 0 && i < _height && j < _width) {
                return _grid[i, j];
            } else {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }
        set {
            if (i >= 0 && j >= 0 && i < _height && j < _width) {
                _grid[i, j] = value;
            } else {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }
    }

    public Grid(int height, int width) {
        _height = height;
        _width = width;
        _grid = new Tile[_height, _width];
    }

    public Grid(Tile[,] grid) {
        _grid = grid;
        _height = grid.GetLength(0);
        _width = grid.GetLength(1);
    }

    public void OpenNeighboursTiles(Tile tile) {
        (int h, int w) = FindElementIndex(tile);

        for (int i = h - 1; i <= h + 1; i++) {
            for (int j = w - 1; j <= w + 1; j++) {
                if (i >= 0 && j >= 0 && i < _height && j < _width) {
                    _grid[i, j].RevealTile();
                }
            }
        }
    }

    public (int, int) FindElementIndex(Tile searchValue) {
        for (int i = 0; i < _height; i++) // Проход по строкам
        {
            for (int j = 0; j < _width; j++) // Проход по столбцам
            {
                if (_grid[i, j] == searchValue) {
                    return (i, j); // Возвращаем индексы как кортеж
                }
            }
        }
        return (-1, -1); // Возвращаем (-1, -1), если элемент не найден
    }

    public void DestroyAll() {
        for (int i = 0; i < _height; i++) {
            for (int j = 0; j < _width; j++) {
                _grid[i, j].DestroyTile();
            }
        }
    }

    public void DisableAllTiles() {
        for (int i = 0; i < _height; i++) {
            for (int j = 0; j < _width; j++) {
                //if(_grid[i, j] != null) {
                    _grid[i, j].enabled = false;
                //}
            }
        }
    }

    public void EnableAllTiles() {
        for (int i = 0; i < _height; i++) {
            for (int j = 0; j < _width; j++) {
                //if (_grid[i, j] != null) {
                    _grid[i, j].enabled = true;
                //}
            }
        }
    }
}
