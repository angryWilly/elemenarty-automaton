using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private bool _isClicked;

    public void ReplaceColor(string nameString)
    {
        _renderer.color = nameString == "0" ? _baseColor : _offsetColor;
    }

    
    private void OnMouseDown()
    {
        if (_isClicked)
        {
            _renderer.color = _baseColor;
            name = "0";
            _isClicked = false;
        }
        else
        {
            _renderer.color = _offsetColor;
            name = "1";
            _isClicked = true;
        }
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
