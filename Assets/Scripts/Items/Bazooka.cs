using Player;
using UnityEngine;

namespace Items
{
    public class Bazooka : Weapon
    {
        [Header("RocketRelated")]
        [SerializeField] private GameObject _rocketPrefab;
        [SerializeField] private GameObject _launchPoint;
        [SerializeField] private float _rocketLaunchingForce;
        
        [Header("other"), Space(10)]
        [SerializeField] private GameObject _itemPickUp;
        
        private Rigidbody _rocketRb;
        
        private bool _launchPointSet;

        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            GameObject rocket = Instantiate(_rocketPrefab, _launchPoint.transform.position, _launchPoint.transform.rotation);
            rocket.GetComponent<RocketBehaviour>()._layerToAvoid = playerController.gameObject.layer;
            _rocketRb = rocket.GetComponent<Rigidbody>();
            _rocketRb.AddForce(_launchPoint.transform.forward * (_rocketLaunchingForce * Time.deltaTime), ForceMode.Impulse);
            Destroy(gameObject);
        }

        public override void AutoUse(PlayerController playerController) { }
        
        private void Update()
        {
            if (!_itemPickUp.activeSelf && !_launchPointSet)
            {
                _launchPoint.transform.SetParent(GetComponentInParent<PlayerController>().transform);
                _launchPoint.transform.localPosition = Vector3.zero;
                _launchPoint.transform.localPosition = new Vector3(0.5f, 0.8f, 0.75f);
                _launchPoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _launchPointSet = true;
            }

            if (_launchPointSet)
            {
                transform.localPosition = Vector3.zero;
            }
        }
    }
}