using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralWalk : MonoBehaviour
{
    private Player _player;

    [SerializeField] private Transform _footTargetR;
    [SerializeField] private Transform _footTargetL;

    [Header("")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector3 _pivotOffset;


    private float angle = 0.0f;
    [SerializeField] private float _stepSpeed = 1f;

    [SerializeField] private float _stepHeight = 0.5f;
    [SerializeField] private float _stepLength = 0.5f;

    [SerializeField] private float _footSpacing = 0.1f;

    [SerializeField] private float _distanceToGround = 0;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        // Update the angle based on the speed
        angle -= _player.Rigidbody.velocity.magnitude * _stepSpeed * Time.deltaTime;
        float x = _stepLength * Mathf.Cos(angle);
        float y = _stepHeight * Mathf.Sin(angle);

        _footTargetR.localPosition = _pivotOffset + new Vector3(_footSpacing, y, x);
        _footTargetL.localPosition = _pivotOffset + new Vector3(-_footSpacing, -y, -x);

        RaycastHit hitR;
        RaycastHit hitL;

        Ray rayR = new Ray(_footTargetR.position + transform.up, -transform.up);
        Ray rayL = new Ray(_footTargetL.position + transform.up, -transform.up);

        if(Physics.Raycast(rayL, out hitL, _distanceToGround + 1f, _layerMask))
        {
            Vector3 footPosition = hitL.point;
            footPosition.y += _distanceToGround;

            _footTargetL.position = footPosition;
            // _footTargetL.rotation = Quaternion.FromToRotation(transform.up, hitL.normal) * Quaternion.Euler(90,0,0) * transform.rotation;
        }

        if(Physics.Raycast(rayR, out hitR, _distanceToGround + 1f, _layerMask))
        {
            Vector3 footPosition = hitR.point;
            footPosition.y += _distanceToGround;

            _footTargetR.position = footPosition;
            // _footTargetR.rotation = Quaternion.FromToRotation(transform.up, hitR.normal) * Quaternion.Euler(90,0,0) * transform.rotation;
        }
    }

    // float angleGizmos = 0;
    // private void OnDrawGizmos()
    // {
    //     Vector3 pivot = transform.position + _pivotOffset;
    //     for (int i = 0; i < 360; i++)
    //     {
    //         float x = _stepLength * Mathf.Cos(i * Mathf.PI / 180);
    //         float y = _stepHeight * Mathf.Sin(i * Mathf.PI / 180);

    //         Gizmos.DrawSphere(pivot + new Vector3(0, y, x), 0.01f);
    //     }

    //     // angleGizmos -= _stepSpeed * Time.deltaTime;
    //     // float x = _stepLength * Mathf.Cos(angleGizmos);
    //     // float y = _stepHeight * Mathf.Sin(angleGizmos);

    //     // Gizmos.color = Color.white;
    //     // Gizmos.DrawSphere(transform.position + new Vector3(_footSpacing, y, x), 0.1f);
    //     // Gizmos.DrawSphere(transform.position + new Vector3(-_footSpacing, -y, -x), 0.1f);
    // }
}
