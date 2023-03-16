using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    private int _height = 0;

    [Range(0, 255)] public int rule;

    [SerializeField] private Tile _tilePrefab;
    private GameObject _tileParent;
    private Tile _spawnedTile;
    private string[] _tilesName;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        Time.fixedDeltaTime = .5f;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _tilesName = new string[_width];
        var tileParent = new GameObject($"{_height} State");
        for (int x = 0; x < _width; x++)
        {
            _spawnedTile = Instantiate(_tilePrefab, new Vector3(x, _height), Quaternion.identity);
            _spawnedTile.name = $"0";
            _spawnedTile.transform.parent = tileParent.transform;

            _tilesName[x] = _spawnedTile.name;
        }
        _tileParent = tileParent;

        _camera.transform.position = new Vector3((float)_width / 2 - 0.5f, 0, -10);
    }

    private bool buttonPressed;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            buttonPressed = buttonPressed == false;
    }

    private void FixedUpdate()
    {
        if (buttonPressed)
            DoSmth();
    }

    private void DoSmth()
    {
        var ruleEvo = new RuleEvolution(rule);
        ruleEvo.CreateRule();
        
        GrabInitState();
        _tilesName = ruleEvo.DoEvolution(_tilesName);
        
        SpawnTiles(_tilesName);
    }

    private void SpawnTiles(string[] tileStrings)
    {
        _height--;
        var tileParent = new GameObject($"{_height} State");
        for (int x = 0; x < _width; x++)
        {
            _spawnedTile = Instantiate(_tilePrefab, new Vector3(x, _height), Quaternion.identity);
            _spawnedTile.name = tileStrings[x];
            _spawnedTile.ReplaceColor(_spawnedTile.name);
            
            _spawnedTile.transform.parent = tileParent.transform;
        }
        _tileParent = tileParent;
    }

    private void GrabInitState()
    {
        for (int i = 0; i < _width; i++)
        {
            _tilesName[i] = _tileParent.transform.GetChild(i).name;
        }
    }
}
