using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private Player _player;
    private CapsuleCollider _capsule;

    [Header("Collision Parameters")]
    [SerializeField] private readonly float _slopeLimit = 45;

    public Vector3 GroundNormal;
    public bool OnGround = false;

    [SerializeField] private LayerMask _layerMask;

    void Awake()
    {
        _player = GetComponent<Player>();
        _capsule = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider == _capsule) return;

        // Check if any of the contacts has acceptable floor angle
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.normal.y > Mathf.Sin(_slopeLimit * Mathf.Deg2Rad + Mathf.PI / 2f))
            {
                GroundNormal = contact.normal;
                OnGround = true;
                return;
            }
        }
    }

}
