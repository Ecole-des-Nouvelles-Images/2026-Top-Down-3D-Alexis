using System;
using UnityEngine;

namespace AlexisVeVer.Scripts
{
    public class GrabbingController : MonoBehaviour
    {
        [SerializeField] private float _flyForce = 10;
        [SerializeField] private float _moveSpeed = 10;
        [SerializeField] private float _springForce = 100;
        [SerializeField] private float _damperForce = 100;
        [SerializeField] private float _breakForce = 200;
        [SerializeField] private float _breakTorque = 200;
        [SerializeField] private bool _jumpActive;
        [SerializeField] private bool _moveActive;
        [SerializeField] private bool _invertMove;
        
        private Collider _otherCollider;
        private Rigidbody _rigidbody;
        private SpringJoint _joint;

        private bool IsGrabbing => _joint != null;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // movement
            if (_moveActive)
            {
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * (_moveSpeed * Time.deltaTime);
                if (_invertMove) move *= -1;
                transform.Translate(move, Space.World);
            }
            
            // jump
            if (Input.GetButtonDown("Jump") && _jumpActive)
            {
                _rigidbody.AddForce(Vector3.up * _flyForce, ForceMode.Impulse);
            }
            
            // grab
            if (Input.GetKeyDown(KeyCode.E) && _otherCollider && !IsGrabbing)
            {
                _joint = gameObject.AddComponent<SpringJoint>();
                _joint.connectedBody = _otherCollider.GetComponent<Rigidbody>();
                _joint.spring = _springForce;
                _joint.breakForce = _breakForce;
                _joint.breakTorque = _breakTorque;
            }

            if (Input.GetKeyUp(KeyCode.E) && _otherCollider && IsGrabbing)
            {
                Destroy(_joint);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameObject) return;
            if (other.GetComponent<Rigidbody>() == null) return;
            if (other.GetComponent<GrabbingController>() == this) return;
            
            _otherCollider = other;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == gameObject) return;
            if (other.GetComponent<Rigidbody>() == null) return;
            if (other.GetComponent<GrabbingController>() == this) return;
            if (_otherCollider != other) return;
            
            _otherCollider =  null;
            Destroy(_joint);
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
