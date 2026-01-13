using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int _frameRate;
    [SerializeField] private SpriteController[] _spriteController;
    
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _currentSprites;
    private float _secondsPerFrame;
    private int _currentSpriteIndex;
    private int _currentStateIndex;
    private float _nextFrameTime;
    private bool _isPlaying = true;
    private SpriteController _currentSpritesController;
    void Start()
    {
        if (_spriteController.Length == 0)
        {
            enabled = false;
            return;
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time;
        _currentSprites = _spriteController[0]._sprites;
        _currentStateIndex = 0;
        _currentSpritesController = _spriteController[0];
    }

    private void OnEnable()
    {
        StartAnimation(0);
    }
    void Update()
    {
        if (_nextFrameTime > Time.time) return;
        if (_currentSpriteIndex >= _currentSprites.Length)
        {
            if (_currentSpritesController._isLooped)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                OnEndAnimation();
                return;
            }
        }
        _spriteRenderer.sprite = _currentSprites[_currentSpriteIndex];
        _nextFrameTime += _secondsPerFrame;
        _currentSpriteIndex++;
    }
    public void SetClip(string name)
    {
        if (!_currentSpritesController._dissableSkip || !_isPlaying) {
            for (int i = 0; i < _spriteController.Length; i++)
            {
                if (_spriteController[i].name == name)
                {
                    _currentSpritesController = _spriteController[i];
                    enabled = true;
                    StartAnimation(i);
                }
            }
        }
    }
    private void OnEndAnimation()
    {
        _isPlaying = false;
        enabled = false;
        _currentSpritesController._onComplete?.Invoke();
    }
    private void StartAnimation(int stateIndex)
    {
        _isPlaying = true;
        _currentStateIndex = stateIndex;
        _currentSprites = _spriteController[_currentStateIndex]._sprites;
        _currentSpriteIndex = 0;
        _nextFrameTime = Time.time + _secondsPerFrame;
    }
    private void OnBecameVisible()
    {
        enabled = _isPlaying;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }
}

[Serializable]
public class SpriteController
{
    public string name;
    public Sprite[] _sprites;
    public bool _isLooped;
    public bool _dissableSkip;
    public UnityEvent _onComplete;
}