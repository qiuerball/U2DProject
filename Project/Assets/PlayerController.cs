using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;
    public float playerSpeed;
    public float jumpForce;

    private Rigidbody2D rb;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        inputControl.GamePlay.Jump.started += Jump;
    }

    

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // 更新人物的x轴速度
        rb.velocity = new Vector2(inputDirection.x * playerSpeed * Time.deltaTime, rb.velocity.y);

        int faceDirection = (int)transform.localScale.x;
        if (inputDirection.x > 0) {
            faceDirection = 1;
        } else if (inputDirection.x < 0) {
            faceDirection = -1;
        } else {

        }

        //人物图像翻转
        transform.localScale = new Vector3(faceDirection, 1, 1);

    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        // Debug.Log("Jump\n");
    }
}
