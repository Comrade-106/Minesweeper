using UnityEngine;
using UnityEngine.Events;

public enum State {
    Flagged,
    Unflagged
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Tile : MonoBehaviour {
    protected bool _isFlagged = false;
    protected bool _isRevealed = false;
    protected SpriteRenderer _tileSpriteRenderer;
    protected AudioSource _audioSource;

    private UnityEvent<State> _tileFlagged;
    protected UnityEvent<Tile> _tileRevealed;

    [SerializeField] protected SpriteRenderer _flagSpriteRenderer;
    [SerializeField] protected AudioClip _revealedSound;
    [SerializeField] private AudioClip _flaggedSound;

    public event UnityAction<State> TileFlagged {
        add => _tileFlagged?.AddListener(value);
        remove => _tileFlagged?.RemoveListener(value);
    }

    public event UnityAction<Tile> TileRevealed {
        add => _tileRevealed?.AddListener(value);
        remove => _tileRevealed?.RemoveListener(value);
    }

    private void Awake() {
        _tileSpriteRenderer = GetComponent<SpriteRenderer>();
        _tileFlagged = new UnityEvent<State>();
        _tileRevealed = new UnityEvent<Tile>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && IsMouseOver()) {
            if (_isFlagged) return;

            RevealTile();
        } else if (Input.GetMouseButtonDown(1) && IsMouseOver()) {
            FlagTile();
        }
    }
    public abstract void RevealTile();

    private void FlagTile() {
        if (_isRevealed) return;

        _flagSpriteRenderer.enabled = !_flagSpriteRenderer.enabled;
        _isFlagged = !_isFlagged;

        _audioSource.PlayOneShot(_flaggedSound);

        if (_isFlagged) {
            _tileFlagged?.Invoke(State.Flagged);
        } else {
            _tileFlagged?.Invoke(State.Unflagged);
        }
    }

    private bool IsMouseOver() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    public void DestroyTile() {
        Destroy(this.gameObject);
    }
}