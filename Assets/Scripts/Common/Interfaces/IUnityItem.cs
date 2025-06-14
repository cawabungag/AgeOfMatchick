using System;
using TMPro;
using UnityEngine;

namespace Common.Interfaces
{
    public interface IUnityItem : IDisposable
    {
        int ContentId { get; }

        Transform Transform { get; }
        SpriteRenderer SpriteRenderer { get; }
        TextMeshPro DebugCoord { get; }

        void Show();
        void Hide();

        void SetSprite(int spriteId, Sprite sprite);
        void SetWorldPosition(Vector3 worldPosition);
        Vector3 GetWorldPosition();
        void SetScale(float value);
        void DebugColor();
    }
}