internal interface IMovementHandler
{
    void OnHorizontalMove(float x);
    void OnVerticalMove(float y);
    void OnJump();
    void OnDash(float x, float y);
    void OnWallGrab(float y);
}
