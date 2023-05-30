using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>


    [RequireComponent(typeof(ARAnchorManager))]
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;

        public ARPlaneManager plane_Manager;
        public ARPointCloudManager cloudsPoints;

        UnityEvent placementUpdate;

        [SerializeField]
        GameObject visualObject;

        public GameObject CoachingClick;
        public GameObject DetectionCanvas;

    bool x = true;


        public void OnValueChanged(bool isOn)
        {
            VisualizePlanes(isOn);

        }

        void VisualizePlanes(bool active)
        {
            plane_Manager.enabled = active;

        foreach (ARPlane plane in plane_Manager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
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
            m_AnchorManager = GetComponent<ARAnchorManager>();

            if (placementUpdate == null)
                placementUpdate = new UnityEvent();

            placementUpdate.AddListener(DisableVisual);
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
        
        if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null && x == true)
                {
                    // spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);

                    var anchor = CreateAnchor(s_Hits[0]);
                    if (anchor)
                    {
                        // Remember the anchor so we can remove it later.
                        m_Anchors.Add(anchor);
                    }
                    else
                    {
                        Debug.Log("Error creating anchor");
                    }


                }
                else
                {
                    //repositioning of the object 
                    spawnedObject.transform.position = hitPose.position;
                }
                placementUpdate.Invoke();
            }



        }

        ARAnchor CreateAnchor(in ARRaycastHit hit)
        {
            ARAnchor anchor = null;

            // If we hit a plane, try to "attach" the anchor to the plane
            if (hit.trackable is ARPlane plane)
            {
                var planeManager = GetComponent<ARPlaneManager>();
                if (planeManager)
                {
                    Debug.Log("Creating anchor attachment.");
                    
                    //Creating an object Anchor
                    var oldPrefab = m_AnchorManager.anchorPrefab;
                    m_AnchorManager.anchorPrefab = m_PlacedPrefab;

                    //Creating the new Anchor of the position of the touch
                    anchor = m_AnchorManager.AttachAnchor(plane, hit.pose);
                    m_AnchorManager.anchorPrefab = oldPrefab;

                //Place only 1 prefab
                x = false;
                    return anchor;
                }
            }

 
            return anchor;
        }




        public void DisableVisual()
        {
            visualObject.SetActive(false);
            VisualizePlanes(false);
            CoachingClick.SetActive(false);

            cloudsPoints.SetTrackablesActive(false);


        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;

        List<ARAnchor> m_Anchors = new List<ARAnchor>();
        ARAnchorManager m_AnchorManager;
    }
