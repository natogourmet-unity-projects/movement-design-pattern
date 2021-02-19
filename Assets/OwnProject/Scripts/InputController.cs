using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private IMovementHandler _movementHandler;
    [SerializeField] private IThrowingHandler _throwingHandler;

    private void Awake()
    {
        _movementHandler = GetComponent<IMovementHandler>();
        _throwingHandler = GetComponent<IThrowingHandler>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        bool _jump = Input.GetButtonDown("Jump");
        bool _dash = Input.GetButtonDown("Dash");
        bool _wallGrab = Input.GetButton("Grab");
        bool _throw = Input.GetButtonDown("Throw");
        
        _movementHandler.OnHorizontalMove(x);
        _movementHandler.OnVerticalMove(y);
        if (_jump) _movementHandler.OnJump();
        if (_dash && (x != 0 || y != 0)) _movementHandler.OnDash(x, y);
        if (_wallGrab) _movementHandler.OnWallGrab(y);
        if (_throw) _throwingHandler.OnThrow(x, y);

    }
}
