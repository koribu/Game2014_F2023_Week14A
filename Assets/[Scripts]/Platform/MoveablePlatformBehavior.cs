using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveablePlatformBehavior : MonoBehaviour
{
    
    public PlatformerMovementTypes _type;

    [SerializeField]
    float _horizontalSpeed = 5;
    [SerializeField]
    float _verticalSpeed = 5;

    [SerializeField]
    float _horizontalTravelDistance = 5;
    [SerializeField]
    float _verticalTravelDistance = 3;



    [Header("Custom Movement")]
    [SerializeField]
    List<Transform> _pathTransforms = new List<Transform>();

    List<Vector2> _customMovementTargets = new List<Vector2>();

    Vector2 _startPosition;
    Vector2 _endPosition;

    float _timer;
    [SerializeField]
    [Range(0f, .10f)]
    float _timerSpeedValue;

    int _currentTargetPathIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;

        foreach(Transform t in _pathTransforms)
        {
            _customMovementTargets.Add(t.position);
        }

        _customMovementTargets.Add(_startPosition);
        _endPosition = _customMovementTargets[_currentTargetPathIndex];

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if(_type == PlatformerMovementTypes.CUSTOM)
        {
            if(_timer < 1 )
            {
                _timer += _timerSpeedValue;
            }
            else if (_timer >= 1)
            {
                _timer = 0;

                _currentTargetPathIndex++;

                if(_currentTargetPathIndex >= _customMovementTargets.Count)
                {
                    _currentTargetPathIndex = 0;
                }

                _startPosition = transform.position;
                _endPosition = _customMovementTargets[_currentTargetPathIndex];

            }
        }
    }

    void Move()
    {
        switch(_type)
        {
            case PlatformerMovementTypes.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalTravelDistance) + _startPosition.x,
                                        transform.position.y);
                break;
            case PlatformerMovementTypes.VERTICAL:
                transform.position = new Vector2(transform.position.x,
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalTravelDistance) + _startPosition.y);
                break;
            case PlatformerMovementTypes.DIAGONAL_RIGHT:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalTravelDistance) + _startPosition.x,
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalTravelDistance) + _startPosition.y);

                break;
            case PlatformerMovementTypes.DIAGONAL_LEFT:
                transform.position = new Vector2(_startPosition.x - Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalTravelDistance),
                                                  Mathf.PingPong(_verticalSpeed * Time.time, _verticalTravelDistance) + _startPosition.y);
                break;
            case PlatformerMovementTypes.CUSTOM:
                transform.position = Vector2.Lerp(_startPosition, _endPosition, _timer);
                break;
        }
    }

}
