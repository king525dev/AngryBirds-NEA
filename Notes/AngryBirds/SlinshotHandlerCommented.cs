using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class SlingShotHandler : MonoBehaviour
{
    // References to line renderers for the slingshot visuals
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    // Transform references for slingshot positions
    [Header("Transform References")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centrePosition;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private Transform _elasticTransform;

    // Slingshot settings
    [Header("Slingshot Stats")]
    [SerializeField] private SlingShotArea _slingShotArea;
    [SerializeField] private float _shotForce = 9f;
    [SerializeField] private float _timeBetweenBirdRespawn = 2f;
    [SerializeField] private float _elasticDivider = 1.2f;
    [SerializeField] private AnimationCurve _elasticCurve;

    // Maximum distance the slingshot can be stretched
    [Header("Scripts")]
    [SerializeField] private float _maxDistance = 5f;

    // Bird prefab and positioning details
    [Header("Bird")]
    [SerializeField] private AngryBird _angryBirdPreFab;
    [SerializeField] private float _angryBirdPositionOffset = 2f;

    // Variables to track slingshot interaction
    private Vector2 _slingshotLinesPosition;
    private Vector2 _direction;
    private Vector2 _directionNormalised;

    private bool _clickedWithinArea;
    private bool _birdOnSlingshot;

    private AngryBird _spawnedAngryBird;

    private void Awake()
    {
        // Hide the slingshot lines initially
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;

        // Spawn the first bird
        SpawnAngryBird();
    }

    private void Update()
    {
        // Check if the player clicked within the slingshot area and a bird is present
        if (InputManager.WasLeftMouseButtonPressed && _slingShotArea.IsWithinSlingShotArea() && _birdOnSlingshot)
        {
            _clickedWithinArea = true;
        }

        // If the player is holding the mouse and clicked within the area, draw slingshot visuals
        if (InputManager.IsLeftMousePressed && _clickedWithinArea)
        {
            DrawSlingshot();
            PositionAndRotateAngryBird();
        }

        // If the player releases the slingshot, launch the bird
        if (InputManager.WasLeftMouseButtonReleased && _birdOnSlingshot && _clickedWithinArea)
        {
            if (GameManager.instance.HasEnoughShots())
            {
                _clickedWithinArea = false;

                // Launch the bird in the calculated direction with the specified force
                _spawnedAngryBird.LaunchBird(_direction, _shotForce);
                
                // Reduce available shots
                GameManager.instance.UsedShot();
                
                _birdOnSlingshot = false;
                
                // Animate the slingshot returning to its original position
                AnimateSlingShot();

                // Spawn a new bird after a delay if more shots are available
                if (GameManager.instance.HasEnoughShots())
                {
                    StartCoroutine(SpawnAngryBirdAfterTime());
                }
            }
        }
    }

    #region Slingshot Methods

    private void DrawSlingshot()
    {
        // Convert the mouse position to world coordinates
        Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);

        // Calculate how far the slingshot has been pulled back within the allowed distance
        _slingshotLinesPosition = _centrePosition.position + Vector3.ClampMagnitude(TouchPosition - _centrePosition.position, _maxDistance);

        // Update the slingshot lines to the new position
        SetLines(_slingshotLinesPosition);

        // Determine the launch direction
        _direction = (Vector2)_centrePosition.position - _slingshotLinesPosition;
        _directionNormalised = _direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        // Enable the slingshot lines if they were hidden
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }

        // Set the position of the slingshot lines
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }

    #endregion

    #region AngryBird Methods

    private void PositionAndRotateAngryBird()
    {
        // Move the bird according to slingshot stretch and align its rotation
        _spawnedAngryBird.transform.position = _slingshotLinesPosition + _directionNormalised * _angryBirdPositionOffset;
        _spawnedAngryBird.transform.right = _directionNormalised;
    }

    private void SpawnAngryBird()
    {
        // Reset slingshot visuals to idle position
        SetLines(_idlePosition.position);

        Vector2 dir = (_centrePosition.position - _idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)_idlePosition.position + dir * _angryBirdPositionOffset;
        
        // Instantiate a new Angry Bird and align it correctly
        _spawnedAngryBird = Instantiate(_angryBirdPreFab, _idlePosition.position, Quaternion.identity);
        _spawnedAngryBird.transform.right = dir;

        _birdOnSlingshot = true;
    }

    private IEnumerator SpawnAngryBirdAfterTime()
    {
        // Wait for the specified delay before spawning a new bird
        yield return new WaitForSeconds(_timeBetweenBirdRespawn);

        SpawnAngryBird();
    }

    #endregion

    #region Animate Slingshot

    private void AnimateSlingShot()
    {
        // Move the elastic transform to simulate slingshot snapback
        _elasticTransform.position = _leftLineRenderer.GetPosition(0);
        float dist = Vector2.Distance(_elasticTransform.position, _centrePosition.position);
        float time = dist / _elasticDivider;
        _elasticTransform.DOMove(_centrePosition.position, time).SetEase(_elasticCurve);
        
        StartCoroutine(AnimateSlingshotLines(_elasticTransform, time));
    }

    private IEnumerator AnimateSlingshotLines(Transform trans, float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            SetLines(trans.position);
            yield return null;
        }
    }

    #endregion
}
