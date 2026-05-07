using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class HandGrabsThing : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private float _springForce = 100;
        [SerializeField] private float _damperForce = 100;
        [SerializeField] private float _breakForce = 200;
        [SerializeField] private float _breakTorque = 200;
        [SerializeField] private bool _isLeftHand;

        [SerializeField] private Collider _otherCollider;
        
        private Rigidbody _rigidbody;
        private SpringJoint _joint;

        private bool IsGrabbing => _joint != null;

        private int _layerNumber;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _layerNumber = gameObject.layer;
        }

        private void Update()
        {
            // grab left hand
            if (_isLeftHand && _playerController.LeftHandGrab && _otherCollider != null)
            {
                _joint = gameObject.AddComponent<SpringJoint>();
                _joint.connectedBody = _otherCollider.GetComponentInParent<Rigidbody>();
                _joint.spring = _springForce;
                _joint.breakForce = _breakForce;
                _joint.breakTorque = _breakTorque;
                _playerController.LeftHandGrab = false;
            }

            if (_isLeftHand && !_playerController.LeftHandGrab && _playerController.LeftHandReleaseGrab)
            {
                Destroy(_joint);
                _playerController.LeftHandReleaseGrab = false;
            }
            // grab right hand
            if (!_isLeftHand && _playerController.RightHandGrab && _otherCollider != null)
            {
                _joint = gameObject.AddComponent<SpringJoint>();
                _joint.connectedBody = _otherCollider.GetComponent<Rigidbody>();
                _joint.spring = _springForce;
                _joint.breakForce = _breakForce;
                _joint.breakTorque = _breakTorque;
                _playerController.RightHandGrab = false;
            }

            if (!_isLeftHand && !_playerController.RightHandGrab && _playerController.RightHandReleaseGrab)
            {
                Destroy(_joint);
                _playerController.RightHandReleaseGrab = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 4) return;
            if (other.gameObject == gameObject) return;
            if (other.GetComponent<Rigidbody>() == null) return;
            if (other.GetComponent<HandGrabsThing>() == this) return;
            if (other.gameObject.layer == _layerNumber) return;

            _otherCollider = other;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == gameObject) return;
            if (other.GetComponent<Rigidbody>() == null) return;
            if (other.GetComponent<HandGrabsThing>() == this) return;
            if (_otherCollider != other) return;

            _otherCollider =  null;
        } 
        private void OnDrawGizmos()
        {
            if (!IsGrabbing) return;

            Gizmos.color = new Color(1f, 1f, 0f, 1);

            // Draw the line
            Gizmos.DrawLine(transform.position, _otherCollider.transform.position);

            // Draw spheres at start and end points
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.DrawSphere(_otherCollider.transform.position, 0.1f);

            // Calculate and display the midpoint
            Vector3 midpoint = (transform.position + _otherCollider.transform.position) / 2f;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(midpoint, 0.15f);

            // Display the distance
            float distance = Vector3.Distance(transform.position, _otherCollider.transform.position);
            UnityEditor.Handles.Label(midpoint, $"Distance: {distance:F2}");
        }
    }
}