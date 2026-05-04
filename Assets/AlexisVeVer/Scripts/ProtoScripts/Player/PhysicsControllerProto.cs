using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player
{
    public class PhysicsControllerProto : MonoBehaviour
    {
        private Rigidbody _rb;
        private GameObject _rayHit;
        
        [SerializeField] private float _desiredPlayerGroundDistance;
        [SerializeField] private float _springStrength;
        [SerializeField] private float _springDamperStrength;
        
        private Vector3 _velocity;
        private Vector3 _raycastDirection;
        
        private bool _rayDidHit;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(_rayDidHit);
            RaycastHit rayhit;
            Debug.DrawLine(transform.position, Vector3.down, Color.red);
            //Debug.DrawLine(transform.position, transform.position + _raycastDirection * _springStrength, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, out rayhit, 5, LayerMask.GetMask("Ground")))
            {
                _rayDidHit = true;
                _rayHit = rayhit.transform.gameObject;
            }
            else
            {
                _rayDidHit = false;
            }

            if (_rayDidHit)
            {
                //Récupération de la vitesse du joueur et de la direction du raycast qui regarde le sol.
                _velocity = _rb.linearVelocity;
                _raycastDirection = transform.TransformDirection(-transform.up);
                Debug.Log("Vitesse joueur + direction du raycast récupérés. Vitesse : " + _velocity + " / direction : " + _raycastDirection);
                
                //Récupération de la vitesse du l'obstacle du raycast
                Vector3 otherVelocity = Vector3.zero;
                Rigidbody hitRigidbody = rayhit.rigidbody;
                if (hitRigidbody != null)
                {
                    otherVelocity = hitRigidbody.linearVelocity;
                }
                //Debug n'as pas trigger
                Debug.Log("infos collidé récupérées. Viteesse :" + otherVelocity);
                
                //Récupération de la vitesse du joueur et de l'objet touché sur l'axe du raycast 
                float raycastDirectionVelocity = Vector3.Dot(_raycastDirection, _velocity);
                float otherDirectionVelocity = Vector3.Dot(_raycastDirection, otherVelocity);
                
                
                float relativeVelocity = raycastDirectionVelocity - otherDirectionVelocity;
                
                //Distance manquante entre la distance actuelle avec le sol et la distance voulue
                float missingHeight = rayhit.distance - _desiredPlayerGroundDistance;
                
                //Force à appliquer au joueur pour que la capsule lévite
                float springforce = missingHeight * _springStrength - relativeVelocity * _springDamperStrength;
                
                _rb.AddForce(_raycastDirection * springforce);
            }
        }
    }
}
