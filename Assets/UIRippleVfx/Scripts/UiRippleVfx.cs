/* Copyright (C) 2020 Vadimskyi - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License license.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VadimskyiLab.Utils;

namespace VadimskyiLab.SimpleUI
{
    /// <summary>
    /// ButtonRippleEffect with lazy initialization
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UiRippleVfx : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private float _effectDuration = 0.3f;

        private RectMask2D _mask;
        private Camera _mainCamera;
        private Image _rippleSprite;
        private Texture2D _generatedTexture;
        private RectTransform _rectTransform;
        private ITweenRemoteControl _fadeTweener;
        private ITweenRemoteControl _scaleTweener;

        private readonly int _maxTexSize = 200;
        private readonly int _scaleFactor = 7;
        private readonly float _alphaFactor = 0.5f;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            CreateTrigger();
        }

        private void CreateTrigger()
        {
            var manager = GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();

            var pointerDownTrigger = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            pointerDownTrigger.callback.AddListener(OnClick);
            manager.triggers.Clear();
            manager.triggers.Add(pointerDownTrigger);
        }

        private void OnClick(BaseEventData pData)
        {
            CreateRippleSprite();
            GenerateRippleTexture();
            CreateMask();

            var newPos = _mainCamera.ScreenToWorldPoint(InputUtils.GetInputPosition());
            _rippleSprite.rectTransform.position = new Vector2(newPos.x, newPos.y);
            _rippleSprite.rectTransform.localScale = Vector3.one;
            _rippleSprite.SetAlpha(0);

            _fadeTweener?.Kill();
            _fadeTweener = _rippleSprite.TweenAlpha(_alphaFactor, _effectDuration, TweenerPlayStyle.PingPong);
            _scaleTweener?.Kill();
            _scaleTweener = _rippleSprite.transform.TweenScale2D(new Vector2(_scaleFactor, _scaleFactor), _effectDuration, TweenerPlayStyle.Once);
            _scaleTweener.OnComplete(ClearCache);

            pData.Use();
        }

        private void CreateRippleSprite()
        {
            if(_rippleSprite != null) return;

            var go = new GameObject("ripple", typeof(RectTransform), typeof(Image));
            go.transform.SetParent(transform, false);
            go.GetComponent<CanvasRenderer>().cullTransparentMesh = true;
            _rippleSprite = go.GetComponent<Image>();
            _rippleSprite.raycastTarget = false;

            SetRippleObjectSize(_rippleSprite);

            _rippleSprite.SetAlpha(0);
        }

        private void CreateMask()
        {
            _mask = _mask ?? GetComponent<RectMask2D>() ?? gameObject.AddComponent<RectMask2D>();
        }

        private void ClearCache()
        {
            if (_generatedTexture != null)
                TextureStaticFactory.ReturnTexture(_generatedTexture);
            _rippleSprite.sprite = null;
            if (_mask == null) return;
            Destroy(_mask);
            _mask = null;
        }

        private void SetRippleObjectSize(Image rippleSprite)
        {
            var rippleObjectSize = GetRippleObjectSize();
            rippleSprite.rectTransform.sizeDelta = new Vector2(rippleObjectSize, rippleObjectSize);
        }

        private int GetRippleObjectSize()
        {
            return (int)(_rectTransform.sizeDelta.x > _rectTransform.sizeDelta.y
                                       ? _rectTransform.sizeDelta.x
                                       : _rectTransform.sizeDelta.y) / 4;
        }

        private void GenerateRippleTexture()
        {
            var texSize = GetRippleTextureSize();

            _generatedTexture = TextureStaticFactory.CreateCircleTexture(_color, texSize, texSize, texSize / 2, texSize / 2, texSize / 2);
            _rippleSprite.sprite = Sprite.Create(
                _generatedTexture,
                new Rect(Vector2.zero, new Vector2(texSize, texSize)),
                Vector2.zero,
                100,
                1,
                SpriteMeshType.FullRect);
        }

        private int GetRippleTextureSize()
        {
            var size = GetRippleObjectSize() * 4;
            return size > _maxTexSize ? _maxTexSize : size;
        }

        private void OnDestroy()
        {
            _fadeTweener?.Kill();
            _scaleTweener?.Kill();
            if(_generatedTexture != null)
                TextureStaticFactory.ReturnTexture(_generatedTexture);
        }
    }
}
