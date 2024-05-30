using Common.Interfaces;
using UnityEngine;

namespace Common
{
    public class UnityItem : MonoBehaviour, IUnityItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isDestroyed;

        public int ContentId { get; private set; }
        public Transform Transform => transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetSprite(int spriteId, Sprite sprite)
        {
            ContentId = spriteId;
            _spriteRenderer.sprite = sprite;
            
            var spriteWidth = sprite.bounds.size.x;
            var spriteHeight = sprite.bounds.size.y;

            var scaleX = 180 / (spriteWidth * sprite.pixelsPerUnit);
            var scaleY = 180 / (spriteHeight * sprite.pixelsPerUnit);

            transform.localScale = new Vector3(0.4f, 0.4f, 1);
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
            // transform.localScale = new Vector3(value, value, value);
            
            var spriteWidth = _spriteRenderer.sprite.bounds.size.x;
            var spriteHeight = _spriteRenderer.sprite.bounds.size.y;

            var scaleX = 180 / (spriteWidth * _spriteRenderer.sprite.pixelsPerUnit);
            var scaleY = 180 / (spriteHeight * _spriteRenderer.sprite.pixelsPerUnit);

            transform.localScale = new Vector3(0.4f, 0.4f, 1);
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