using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Dependencies")] 
    [SerializeField] private Rigidbody2D _playerRigidbody;

    [Header("Settinngs")] 
    [SerializeField] private float _lerpSpeed;

    private Vector2 _lerpedPos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(_playerRigidbody.position.x, _playerRigidbody.position.y, -5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        
    }

    private void LateUpdate()
    {
        _lerpedPos = Vector2.Lerp(transform.position, _playerRigidbody.position, _lerpSpeed * Time.deltaTime);

        transform.position = new Vector3(_lerpedPos.x, _lerpedPos.y, -5);
    }
}
