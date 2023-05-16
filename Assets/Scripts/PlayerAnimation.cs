// PlayerAnimation.cs

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator Animator;
    [SerializeField] PlayerController controller;
    [SerializeField] Rigidbody2D rb2D;

    private float _horizontalInput;
    private bool _isTouchingGround, _isTouchingWall;

    // Update is called once per frame
    void Update()
    {
        _isTouchingGround = controller.IsTouchingGround();
        _isTouchingWall = controller.IsTouchingWall();

        Animator.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));
        Animator.speed = rb2D.velocity.x.IsApproxZero() ? 1 : Mathf.Abs(rb2D.velocity.x) / controller.TopSpeed;
        Animator.SetBool("isAirborn", !_isTouchingWall && !_isTouchingGround);
        Animator.SetBool("isWalled", _isTouchingWall && !_isTouchingGround);

        // Squish on ground if player pressed down on ground
        if ((Input.GetAxisRaw("Vertical") + 1).IsApproxZero() && controller.IsTouchingGround())
            Animator.SetBool("isCrouching", true);
        else
            Animator.SetBool("isCrouching", false);

        // Flip Sprite based on Input direction
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (_horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 0);
        else if (_horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 0);

    }
}
