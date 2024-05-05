using System;
using UnityEngine;

public class ProtagController : MonoBehaviour
{
    public static ProtagController Instance;
    
    public Vector3 Facing => _meshBody.forward;
    
    public Transform MeshBody => _meshBody;
    
    [Header("Dependencies")]

    [SerializeField]
    private CharacterController _charController;

    [SerializeField]
    private Transform _meshBody;

    [SerializeField]
    private MoveStats _moveStats;

    [SerializeField]
    private Animator _anim;

    [Header("Particles")]

    [SerializeField]
    private ParticleSystem _runParticle;

    [SerializeField]
    private ParticleSystem _jumpParticle;

    private readonly int _animState = Animator.StringToHash("State");

    private Vector2 _currentHorizontalVelocity;

    private bool _wasGrounded;
    private float _yVelocity;
    
    private IInputProvider _inputProvider;

    private void Awake()
    {
        Instance = this;
        _inputProvider = UserInputProvider.Instance;
    }

    private void Update()
    {

        var input = _inputProvider.GetMovementInput();

        var moveDirection = new Vector3(input.x, 0, input.y);
        
        var lookDirection = _inputProvider.GetLookDirection();

        moveDirection = Quaternion.LookRotation(lookDirection) * moveDirection;

        // project onto the xz plane
        moveDirection.y = 0;
        moveDirection.Normalize();
        
        var transformedInput = new Vector2(moveDirection.x, moveDirection.z);

        HandleHorizontalMovement(transformedInput);

        HandleVerticalMovement();

        HandleAnimation(transformedInput);
    }

    private void HandleHorizontalMovement(Vector2 transformedInput)
    {

        Vector2 targetHorizontalVelocity = transformedInput * _moveStats.MoveSpeed;

        if (transformedInput.magnitude > 0)
        {
            _currentHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, targetHorizontalVelocity,
                _moveStats.MoveAccel * Time.deltaTime);
        }
        else
        {
            _currentHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, Vector2.zero,
                _moveStats.MoveFriction * Time.deltaTime);
        }

        _wasGrounded = _charController.isGrounded;

        // Move!
        _charController.Move(new Vector3(_currentHorizontalVelocity.x, _yVelocity, _currentHorizontalVelocity.y) *
                             Time.deltaTime);


        Vector3 velocity = _charController.velocity;
        _currentHorizontalVelocity = new Vector2(velocity.x, velocity.z);
    }

    private void HandleVerticalMovement()
    {
        // Gravity
        _yVelocity += Physics.gravity.y * Time.deltaTime;

        if (_charController.isGrounded && _yVelocity < 0)
        {
            _yVelocity = -10f;
        }

        // limit fall speed

        if (!_charController.isGrounded && _wasGrounded)
        {
            _yVelocity = Mathf.Max(_yVelocity, 0);
        }

        // Jump
        if (_inputProvider.GetJumpInput() && _charController.isGrounded)
        {
            _jumpParticle.Play();
            _yVelocity = _moveStats.JumpVelocity;
        }
    }

    private void HandleAnimation(Vector2 transformedInput)
    {
        // Animation
        var finalState = 0;
        if (_charController.isGrounded)
        {
            finalState = transformedInput.magnitude > 0 ? 1 : 0;
        }
        else
        {
            if(_yVelocity > 0)
                finalState = 2;
            else
            finalState = 3; 
        }

        _anim.SetInteger(_animState, finalState);

        // Smoothly rotate player to face movement direction
        if (transformedInput.magnitude > 0)
        {
            Quaternion currentRotation = _meshBody.rotation;
            var horizontalVel = new Vector3(transformedInput.x, 0, transformedInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVel);
            
            float t = 1 - Mathf.Pow(1 - 0.9999f, Time.deltaTime);
            _meshBody.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);
        }


        // Particles
        if (finalState == 1)
        {
            if (!_runParticle.isPlaying)
            {
                _runParticle.Play();
            }
        }
        else
        {
            if (_runParticle.isPlaying)
            {
                _runParticle.Stop();
            }
        }
    }

    public void Launch(float jumpHeight)
    {
        _yVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
    }
}