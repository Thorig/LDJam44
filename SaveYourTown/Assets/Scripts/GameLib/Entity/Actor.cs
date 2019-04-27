#undef USE_3D_RAYS
//#define USE_3D_RAYS

using GameLib.Level;
using GameLib.System.Gravity2D;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameLib.Entity
{
    public class Actor : MonoBehaviour
    {
        [SerializeField]
        protected Gravity gravity;

        public Gravity Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        [SerializeField]
        protected float gravitySetting;

        [SerializeField]
        protected IGravityClient gravityClient;

        public IGravityClient GravityClient
        {
            get { return gravityClient; }
            set { gravityClient = value; }
        }

#if USE_3D_RAYS
        [SerializeField]
        protected RayInformation3D rayInformation3D;

        public RayInformation3D RayInformation3D
        {
            get { return rayInformation3D; }
            set { rayInformation3D = value; }
        }

        protected RayHitboxes3D rayHitboxes3D;

        public RayHitboxes3D RayHitboxes3D
        {
            get { return rayHitboxes3D; }
            set { rayHitboxes3D = value; }
        }
#else
        [SerializeField]
        protected RayInformation rayInformation;

        public RayInformation RayInformation
        {
            get { return rayInformation; }
            set { rayInformation = value; }
        }

        protected RayHitboxes rayHitboxes;

        public RayHitboxes RayHitboxes
        {
            get { return rayHitboxes; }
            set { rayHitboxes = value; }
        }

#endif
        [SerializeField]
        protected bool rotateHorizontalMovement = false;

        public bool RotateHorizontalMovement
        {
            get { return rotateHorizontalMovement; }
            set { rotateHorizontalMovement = value; }
        }

        [SerializeField]
        protected new Collider2D collider2D;

        public Collider2D Collider2D
        {
            get { return collider2D; }
            set { collider2D = value; }
        }

        [SerializeField]
        protected float jumpForce;

        public float JumpForce
        {
            get { return jumpForce; }
            set { jumpForce = value; }
        }

        [SerializeField]
        protected SpriteRenderer spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get { return spriteRenderer; }
            set { spriteRenderer = value; }
        }

        [SerializeField]
        protected bool castsShadows;

        public bool CastsShadows
        {
            get { return castsShadows; }
            set { castsShadows = value; }
        }

        protected float oldZ_ = 0.0f;
        protected float currectZ = 0.0f;
        
        [SerializeField]
        protected int layermask;
        
        private void OnEnable()
        {
            init();
        }

        protected void Awake()
        {
            init();
        }

        public virtual void Start()
        {
            init();
        }

        protected virtual void init()
        {
            currectZ = transform.localRotation.eulerAngles.z;
            
            IGravityFactory gravityFactory = new GravityFactory();
#if USE_3D_RAYS
            rayHitboxes3D = gravityFactory.getRayHitboxes3D();
#else
            rayHitboxes = gravityFactory.getRayHitboxes();
#endif
            setGravity();

            gravityClient = gravityFactory.getGravityClient(this);

            if(castsShadows)
            {
                spriteRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
                spriteRenderer.receiveShadows = true;
            }

            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile") | 1 << layersLookup.giveLayerNumber("Default"));
        }
        
        public virtual void setGravity()
        {
            IGravityFactory gravityFactory = new GravityFactory();

            gravity = gravityFactory.getGravity();
            gravity.init(gravitySetting);
        }

        public virtual void OnDestroy()
        {
#if USE_3D_RAYS
            rayInformation3D = null;
#else
            rayInformation = null;
#endif
        }

        public virtual void LateUpdate()
        {            
            gravity.update(gravityClient);
#if USE_3D_RAYS
            Collider collider = rayHitboxes3D.HitMiddleBelow.collider;
#else
            Collider2D collider = rayHitboxes.HitMiddleBelow.collider;
#endif
            if (collider != null &&
                rotateHorizontalMovement &&
                currectZ != collider.gameObject.transform.localRotation.eulerAngles.z)
            {
                setRotations();
            }
        }

        public virtual void moveActor(Vector3 positionNormalGravity)
        {
            transform.position = transform.position + 
                
                Quaternion.Euler(new Vector3(
                                       transform.rotation.eulerAngles.x,
                                       transform.rotation.eulerAngles.y,
                                       transform.rotation.eulerAngles.z)) * positionNormalGravity;
        }

        protected virtual void setRotations()
        {
            oldZ_ = currectZ;
#if USE_3D_RAYS
            currectZ = rayHitboxes3D.HitMiddleBelow.collider.gameObject.GetComponent<Tile>().getAngle();
#else
            currectZ = rayHitboxes.HitMiddleBelow.collider.gameObject.GetComponent<Tile>().getAngle();
#endif
            gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, currectZ);
        }
    }
}