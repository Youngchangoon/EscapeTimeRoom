using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float velocity = 5;
    public float turnSpeed = 10;

    private Vector2 _input;
    private float _angle;

    private Quaternion _targetRotation;
    private Transform _cam;

    private void Start()
    {
        _cam = Camera.main.transform;
    }

    private void Update()
    {
        GetInput();
        
        if(Math.Abs(_input.x) < 1 && Mathf.Abs(_input.y) < 1) 
            return;

        CalculateDirection();
        Rotate();
        Move();
    }

    private void GetInput()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
    }

    private void CalculateDirection()
    {
        _angle = Mathf.Atan2(_input.x, _input.y);
        _angle = Mathf.Rad2Deg * _angle;
        _angle += _cam.eulerAngles.y;
    }

    private void Rotate()
    {
        _targetRotation = Quaternion.Euler(0, _angle, 0);
        transform.rotation = _targetRotation;
        // transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, turnSpeed * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }
    
    
}