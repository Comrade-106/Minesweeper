using TMPro;
using UnityEngine;

public class RegularTile : Tile
{
    private int _bombsCount = 0;
    
    [SerializeField] private TMP_Text _bombsCountText;

    public int BombsCount {
        get => _bombsCount;
    }

    public void Init(AudioSource audioSource, int bombsCount) {
        _audioSource = audioSource;
        _bombsCount = bombsCount;

        _bombsCountText.text = _bombsCount > 0 ? _bombsCount.ToString() : "";
    }

    public override void RevealTile() {
        if (!_isRevealed) {
            _isRevealed = true;
            _tileRevealed?.Invoke(this);
            _tileSpriteRenderer.enabled = false;
            _flagSpriteRenderer.enabled = false;
            //_audioSource.PlayOneShot(_revealedSound);
        }
    }
}
