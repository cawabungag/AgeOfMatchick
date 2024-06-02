using Common.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Common
{
    public class UnityItem : MonoBehaviour, IUnityItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshPro _debugCoord;

        private bool _isDestroyed;

        public int ContentId { get; private set; }
        public Transform Transform => transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public TextMeshPro DebugCoord => _debugCoord;

        public void Show()
        {
            _spriteRenderer.color = Color.white;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _spriteRenderer.color = Color.white;
            gameObject.SetActive(false);
        }

        public void SetSprite(int spriteId, Sprite sprite)
        {
            ContentId = spriteId;
            _spriteRenderer.sprite = sprite;
            transform.localScale = new Vector3(0.35f, 0.35f, 1);
        }

        public void SetWorldPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }

        public void SetScale(float value)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }

        private Color originalColor;
        public void DebugColor()
        {
            originalColor = SpriteRenderer.color;
            SpriteRenderer.DOColor(Color.clear, 1f).OnComplete(() =>
            {
                SpriteRenderer.DOColor(originalColor, 1f);
            });
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }

        public void Dispose()
        {
            if (_isDestroyed == false)
            {
                Destroy(gameObject);
            }
        }
    }
}