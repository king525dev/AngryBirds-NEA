using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotHandler : MonoBehaviour{

     //Line Renderers
     [SerializeField] private LineRenderer _leftLineRenderer;
     [SerializeField] private LineRenderer _rightLineRenderer;

     //Transform References
     [SerializeField] private Transform _leftStartPosition;
     [SerializeField] private Transform _rightStartPosition;
     [SerializeField] private Transform _centrePosition;
     [SerializeField] private Transform _idlePosition;

     //Slingshot Stats
     [SerializeField] private float _maxDistance = 5f;
     [SerializeField] private SlingShotArea _slingShotArea;
     [SerializeField] private float _shotForce = 9f;
     [SerializeField] private float _timeBetweenBirdRespawn = 2f;

     //Bird
     [SerializeField] private AngryBird _angryBirdPreFab;
     [SerializeField] private float _angryBirdPositionOffset = 2f;

     private bool _clickedWithinArea;

     private Vector2 _slingshotLinesPosition;
     private Vector2 _direction;
     private Vector2 _directionNormalised;

     //Run Every Second
     private void Update(){
          
          //If Player Clicked near the Slingshot
          if(Mouse.current.leftButton.wasPressedThisFrame && _slingShotArea.IsWithinSlingShotArea() ){
               _clickedWithinArea = true;
          }

          //Draw Slings and spawn Bird if Other condition is true
          if (Mouse.current.leftButton.isPressed && _clickedWithinArea){
               DrawSlingshot();
          }
     }

          private void DrawSlingshot(){

               //Read mouse Position using Camera
               Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

               //Clamp maximum distance from central position
               _slingshotLinesPosition = _centrePosition.position + Vector3.ClampMagnitude(TouchPosition - _centrePosition.position, _maxDistance);

               //Draw the Slings
               SetLines(_slingshotLinesPosition);

               //Calculate Direction Vector which Bird sees
               _direction = (Vector2)_centrePosition.position - _slingshotLinesPosition;
               _directionNormalised = _direction.normalized;
     }

     private void SetLines(Vector2 position){
          if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
          {
               _leftLineRenderer.enabled = true;
               _rightLineRenderer.enabled = true;
          }

          //Set Position of Left Sling
          _leftLineRenderer.SetPosition(0, position);
          _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

          //Set Position of Right Sling
          _rightLineRenderer.SetPosition(0, position);
          _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
          _angryBirdPreFab = "Odd Ref";
          _shotForce;
          _timeBetweenBirdRespawn + 1;
     }

     private void PositionAndRotateAngryBird()
     {
          //Move Angry Bird when cursor moved
          _spawnedAngryBird.transform.position = _slingshotLinesPosition +_directionNormalised * _angryBirdPositionOffset;

          //Set direction that AngryBird is looking at
          _spawnedAngryBird.transform.right = _directionNormalised;
     }

}