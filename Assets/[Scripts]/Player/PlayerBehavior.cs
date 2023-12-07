using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField]
    float _walkSpeed = 5;

    [SerializeField]
    float _maxSpeed = 50;

    [SerializeField]
    float _jumpForceAmount = 100;

    [SerializeField]
    Transform _groundPoint;

    [SerializeField]
    LayerMask _groundingLayers;

    Joystick _leftJoystick;
    [SerializeField]
    [Range(0f, 1f)]
    float _tresholdForJump = .3f;

    float _airFactor = .3f;

    [SerializeField]
    float _hurtJumpAmount = 100;
    

    public bool _isGrounded = false;

    [SerializeField]
    HealthBarController _healthBar;

    Animator _animator;
    SoundManager _soundManager;
    ParticleSystem _dustTrail;

    [Header("ScreenShakeEffect")]
    [SerializeField]
    private CinemachineVirtualCamera _mainCamera;
    CinemachineBasicMultiChannelPerlin _cameraPerlin;

    [SerializeField]
    float _shakePower;

    [SerializeField]
    float _cameraShakeTime;

    bool _isCameraShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _soundManager = FindObjectOfType<SoundManager>();
        _dustTrail = GetComponentInChildren<ParticleSystem>();

        _cameraPerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();


        if(GameObject.Find("ScreenController"))
            _leftJoystick = GameObject.Find("LeftJoystick").GetComponent<Joystick>();

    }

    // Update is called once per frame
    void Update()
    {
       IsGrounded();
        //Movement functionality
    
       //Jump fucntionality
      

        if(!_isGrounded) //OnAir
        {
            //_dustTrail.Stop();

            if(_rigidbody.velocity.y >= 0)
            {
                _animator.SetInteger("State", (int)AnimationStates.JUMP);
            }
            else if(_rigidbody.velocity.y < 0)
            {
                _animator.SetInteger("State", (int)AnimationStates.FALL);
            }
        }
        else
        {
            _dustTrail.Play();
            _animator.SetInteger("State", (int)AnimationStates.IDLE);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    void Move()
    {
        float leftJoystickInput = 0;
        if(_leftJoystick)
        {
            leftJoystickInput = _leftJoystick.Horizontal;
        }
        float xDirection = Input.GetAxisRaw("Horizontal") + leftJoystickInput; // if it moves to right it is +1 else if it moves to left it is -1


        Flip(xDirection);

        Vector2 force = Vector2.zero;

        if (xDirection > 0)
        {
             force = Vector2.right * _walkSpeed * Time.deltaTime;

            _rigidbody.AddForce(Vector2.right * _walkSpeed * Time.deltaTime);
        }
        else if (xDirection < 0)
        {
            force = Vector2.left * _walkSpeed * Time.deltaTime;
           
        }
        float maxSpeed = _maxSpeed;

        if(force != Vector2.zero)
        {
            if (!_isGrounded)
            {
                force = force * _airFactor;

                maxSpeed = _airFactor * _maxSpeed;
            }
        }
                

        _rigidbody.AddForce(force);

        _rigidbody.velocity = new Vector2(Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed , maxSpeed), Mathf.Clamp(_rigidbody.velocity.y, -10, 10));
    }


    void Jump()
    {
       
        //Check the input 
        float leftJoystickVerticalInput = 0;

        if(_leftJoystick)
        {
            leftJoystickVerticalInput = _leftJoystick.Vertical;
        }

        float _isJumping = Input.GetAxisRaw("Jump") + leftJoystickVerticalInput;

        if(_isGrounded && _isJumping > _tresholdForJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForceAmount);
            _soundManager.PlaySound(Channel.PLAYER_SFX, Sound.JUMP);

        }
    }

    void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_groundPoint.position, .01f, Vector2.down, .1f, _groundingLayers);
        _isGrounded = hit;
    }

    void Flip(float direction)
    {
        if (direction < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction > 0)
            transform.localScale = Vector3.one;
    }

    void DustTrail()
    {
       
    }

    IEnumerator CameraShakeRoutine()
    {
        if (_isCameraShaking)
            yield break;

        _isCameraShaking = true;

        _cameraPerlin.m_AmplitudeGain = _shakePower;
        yield return new WaitForSeconds(_cameraShakeTime);
        _cameraPerlin.m_AmplitudeGain = 0;

        _isCameraShaking = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(_groundPoint.position,Vector3.down * .1f + _groundPoint.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _soundManager.PlaySound(Channel.PLAYER_HURT_SFX, Sound.HURT);
            _healthBar.GetDamage(20);

            StartCoroutine(CameraShakeRoutine());
        }
        else if (collision.CompareTag("Hazard"))
        {
            _soundManager.PlaySound(Channel.PLAYER_HURT_SFX, Sound.HURT);
            _healthBar.GetDamage(10);

            Vector2 dir = (transform.position - collision.transform.position);
            _rigidbody.AddForce(dir * _hurtJumpAmount, ForceMode2D.Impulse);

            Debug.Log("Hazard hurt me");

            StartCoroutine(CameraShakeRoutine());

        }
    }
}
