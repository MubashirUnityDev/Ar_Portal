using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    //[RequireComponent(typeof(ARAnchorManager))]
    public class TapToPlaceObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        bool placementCheck; // used to enable and disable object placement
        public UnityEvent onContentPlaced;
        public GameObject Tutorial;
        //public GameObject m_ARCamera;
        //public ARAnchor m_ARAnchor;


        //Mubashir

       // ARAnchorManager m_ARAnchorManager;
        ARPlaneManager m_ARPlaneManager;
        //ARPlane SpawnedPlane;

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            //m_ARAnchorManager = GetComponent<ARAnchorManager>();
            m_RaycastManager = GetComponent<ARRaycastManager>();
            m_ARPlaneManager = GetComponent<ARPlaneManager>();
            //m_ARPlaneManager.planesChanged += ARPlanesChanged;


        }



        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition;
                touchPosition = new Vector2(mousePosition.x, mousePosition.y);
                return true;
            }
#else
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
#endif

            touchPosition = default;
            return false;
        }

        void Update()
        {
            if (placementCheck)
            {
                return;
            }
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    placementCheck = false;
                    Tutorial.SetActive(false);
                    //onContentPlaced.Invoke();
                    DisablePlane();
                    Handheld.Vibrate();
                    m_ARPlaneManager.enabled = false;
                    placementCheck = true ;
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                    Tutorial.SetActive(false);
                    //SetAnchors(hitPose);
                }
            }
        }

        public void DisablePlane()
        {
            foreach (var plane in m_ARPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }

        //void SetAnchors(Pose p)
        //{
        //    ARAnchor anchor = m_ARAnchorManager.AttachAnchor(SpawnedPlane, p);

        //}

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;

        //Mubashir

        //void ARPlanesChanged(ARPlanesChangedEventArgs args)
        //{
        //    if (args.added != null && args.added.Count > 0)
        //    {
        //        SpawnedPlane = args.added[0]; // Assuming you're spawning only one object on one plane
        //                                      // Spawn the object on the plane
        //    }
        //}
    }
}