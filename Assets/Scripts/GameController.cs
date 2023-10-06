using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum GameDebugMode
{
    Normal,
    Distance,
    Vision
}

public class GameController : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI debugPosText;
    public TextMeshProUGUI debugVelocityText;
    public TextMeshProUGUI debugNearestDistText;
    
    
    private LineRenderer _lineRenderer;
    private GameObject[] _pickups;
    private const string PickUpTag = "PickUp";
    private GameDebugMode _debugMode;
    private Vector3 _cachedPosition;
    

    private void Start()
    {
        _debugMode = GameDebugMode.Normal;
        _pickups = GameObject.FindGameObjectsWithTag(PickUpTag);
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _cachedPosition = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _debugMode = _debugMode switch
            {
                GameDebugMode.Normal => GameDebugMode.Distance,
                GameDebugMode.Distance => GameDebugMode.Vision,
                GameDebugMode.Vision => GameDebugMode.Normal,
                _ => throw new ArgumentOutOfRangeException()
            };
            ChangeDebugMode(_debugMode);
        }
    }

    private void ChangeDebugMode(GameDebugMode mode)
    {
        switch (mode)
        {
            case GameDebugMode.Normal:
                debugPosText.enabled = false;
                debugVelocityText.enabled = false;
                _lineRenderer.enabled = false;
                break;
            case GameDebugMode.Distance:
                debugPosText.enabled = true;
                debugVelocityText.enabled = true;
                _lineRenderer.enabled = true;
                break;
            case GameDebugMode.Vision:
                debugPosText.enabled = false;
                debugVelocityText.enabled = false;
                _lineRenderer.enabled = true;
                break;
            default:
                debugPosText.enabled = false;
                debugVelocityText.enabled = false;
                _lineRenderer.enabled = false;
                break;
        }
    }

    private void FixedUpdate()
    {
        var position = player.position;
        var velocity = (position - _cachedPosition) / Time.deltaTime;
        if (_debugMode == GameDebugMode.Distance)
        {
            debugPosText.text = $"Pos: {position.ToString("0.00")}";
            debugVelocityText.text = $"Velocity: {velocity.ToString("0.00")}";
            
            var closest = _pickups
                .Where(p => p.activeSelf)
                .OrderBy(p => Vector3.Distance(p.transform.position, player.position))
                .FirstOrDefault();

            if (closest is null)
            {
                return;
            }
        
            foreach (var pickup in _pickups) pickup.GetComponent<Renderer>().material.color = Color.white;
            closest.GetComponent<Renderer>().material.color = Color.blue;
            _lineRenderer.SetPosition(0, player.position);
            _lineRenderer.SetPosition(1, closest.transform.position);
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
        }

        if (_debugMode == GameDebugMode.Vision)
        {
            _lineRenderer.SetPosition(0, position);
            _lineRenderer.SetPosition(1, position + velocity);
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
        }
        
        _cachedPosition = position;
    }
}