using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using TMPro;

public class ARPlacementManager : MonoBehaviour
{
    // Detect User's Touch
    // Project/Shoot a Raycast
    // Instantiate the object where the raycast was shot

    public ARSessionOrigin sessionOrigin;
    public GameObject _Object;

    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    private GameObject instantiatedObject;

    public TMP_Text DebugLog;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            bool collision = sessionOrigin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, TrackableType.PlaneWithinPolygon);

            if(collision)
            {

                if(instantiatedObject == null)
                {
                    instantiatedObject = Instantiate(_Object);
                    instantiatedObject.transform.position = raycastHits[0].pose.position;

                    if (instantiatedObject.GetComponent<ARAnchor>() == null) {
                        bool isAnchored = instantiatedObject.AddComponent<ARAnchor>();
                        if (isAnchored == true) DebugLog.text = "Object Is Anchored";                        
                    }
                    

                    foreach (var plane in sessionOrigin.GetComponent<ARPlaneManager>().trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    sessionOrigin.GetComponent<ARPlaneManager>().enabled = false;

                }
            }

        }
    }

}
