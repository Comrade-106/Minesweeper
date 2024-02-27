using UnityEngine;

public class BombTile : Tile
{
    [SerializeField] private Sprite _bombSprite;
    [SerializeField] private ParticleSystem _explosionVFX;

    public void Init(AudioSource audioSource) {
        _audioSource = audioSource;
    }
 
    public override void RevealTile() {
        _tileSpriteRenderer.sprite = _bombSprite;
        _tileSpriteRenderer.color = Color.red;

        ParticleSystem explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(explosion.gameObject, 2f);

        _audioSource.PlayOneShot(_revealedSound);

        _isRevealed = true;

        _tileRevealed?.Invoke(this);
    }
}
