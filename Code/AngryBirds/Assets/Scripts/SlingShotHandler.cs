using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class SlingShotHandler : MonoBehaviour
{
    //Initialising Variables
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transform References")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centrePosition;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private Transform _elasticTransform;

    [Header("Slingshot Stats")]
    [SerializeField] private SlingShotArea _slingShotArea;
    [SerializeField] private float _shotForce = 9f;
    [SerializeField] private float _timeBetweenBirdRespawn = 2f;
    [SerializeField] private float _elasticDivider = 1.2f;
    [SerializeField] private AnimationCurve _elasticCurve;

    [Header("Scripts")]
    [SerializeField] private float _maxDistance = 5f;

    [Header("Bird")]
    [SerializeField] private AngryBird _angryBirdPreFab;
    [SerializeField] private float _angryBirdPositionOffset = 2f;

    private Vector2 _slingshotLinesPosition;
    private Vector2 _direction;
    private Vector2 _directionNormalised;

    private bool _clickedWithinArea;
    private bool _birdOnSlingshot;

    private AngryBird _spawnedAngryBird;
    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;

        SpawnAngryBird();
    }

    //Run Every Second
    private void Update()
    {
        //If Player Clicked near the Slingshot
        if(InputManager.WasLeftMouseButtonPressed && _slingShotArea.IsWithinSlingShotArea() && _birdOnSlingshot)
        {
            _clickedWithinArea = true;
        }

        //Draw Slings and spawn Bird if Other condition is true
        if (InputManager.IsLeftMousePressed && _clickedWithinArea)
        {
            DrawSlingshot();
            PositionAndRotateAngryBird();
        }

        //Set _clickedWithinArea back to False 
        if (InputManager.WasLeftMouseButtonReleased && _birdOnSlingshot && _clickedWithinArea)
        {
            if (GameManager.instance.HasEnoughShots())
            {
                _clickedWithinArea = false;

                _spawnedAngryBird.LaunchBird(_direction, _shotForce);

                GameManager.instance.UsedShot();

                _birdOnSlingshot = false;

                AnimateSlingShot();

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
        Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);

        _slingshotLinesPosition = _centrePosition.position + Vector3.ClampMagnitude(TouchPosition - _centrePosition.position, _maxDistance);

        SetLines(_slingshotLinesPosition);

        _direction = (Vector2)_centrePosition.position - _slingshotLinesPosition;
        _directionNormalised = _direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }

        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }

    #endregion

    #region AngryBird Methods

    private void PositionAndRotateAngryBird()
    {
        //Move Angry Bird when cursor moved
        _spawnedAngryBird.transform.position = _slingshotLinesPosition + _directionNormalised * _angryBirdPositionOffset;
        _spawnedAngryBird.transform.right = _directionNormalised;
    }

    private void SpawnAngryBird()
    {
        //Put Slings in Idle Position
        SetLines(_idlePosition.position);

        Vector2 dir = (_centrePosition.position - _idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)_idlePosition.position + dir * _angryBirdPositionOffset;
        
        //Create an Instance of an Angry Bird
        _spawnedAngryBird = Instantiate(_angryBirdPreFab, _idlePosition.position, Quaternion.identity);
        _spawnedAngryBird.transform.right = dir;

        _birdOnSlingshot = true;
    }

    private IEnumerator SpawnAngryBirdAfterTime()
    {
        //Wait a certain time
        yield return new WaitForSeconds(_timeBetweenBirdRespawn);

        //Spawn Angry Bird as usual
        SpawnAngryBird();
    }

    #endregion

    #region Animate Slingshot
    private void AnimateSlingShot()
    {
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