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
    public class TapToPlaceObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        bool placementCheck; // used to enable and disable object placement
        public UnityEvent onContentPlaced;
        public GameObject HowToUse;
        public GameObject m_ARCamera;


        public TutorialController TutorialControl;
        private bool isStep2;
        private bool isStep3;



        //Mubashir

        ARPlaneManager m_ARPlaneManager;

        private void Start()
        {
            TutorialControl.SetStep(1);
            
        }
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
            m_RaycastManager = GetComponent<ARRaycastManager>();
            m_ARPlaneManager = GetComponent<ARPlaneManager>();


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
            Debug.Log("Trackalbe Count: " + m_ARPlaneManager.trackables.count);
            if(m_ARPlaneManager.trackables.count>0 )
            {
                if(placementCheck==false)
                {
                    if (isStep2 == false)
                    {
                        TutorialControl.SetStep(2);
                    }
                    isStep2 = true;
                    
                }else
                {
                    if (isStep3 == false)
                    {
                        TutorialControl.SetStep(3);
                        Invoke("setTutorials",7);
                    }
                    isStep3 = true;
                }



            }

            if (placementCheck)
            {
                return;
            }
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinBounds))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation) ; //Critical
                    //spawnedObject.transform.rotation = Quaternion.Euler(spawnedObject.transform.rotation.x, m_ARCamera.transform.rotation.y, spawnedObject.transform.rotation.z);
                    placementCheck = false;
                    //Tutorial.SetActive(false);
                    DisablePlane();
                    Handheld.Vibrate();
                    m_ARPlaneManager.enabled = false;
                    
                    placementCheck = true ;
                    

                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                    //Tutorial.SetActive(false);
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

        

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;

        //Mubashir


        public void setTutorials()
        {
            TutorialControl.DisableAllSteps();
        }
       
    }

}