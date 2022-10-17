using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    private ARRaycastManager rayManager;
    [SerializeField]
    private GameObject visual;
    public GameObject joystickCanvas;
    bool onetime;

    void Start()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        visual.SetActive(false);
        joystickCanvas.SetActive(false);

        onetime = true;
      
    }

    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0 && onetime == true)
        {
            
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            onetime = false;
            // enable the visual if it's disabled
            if (!visual.activeInHierarchy)
                visual.SetActive(true);
                joystickCanvas.SetActive(true);
        }
    }

}