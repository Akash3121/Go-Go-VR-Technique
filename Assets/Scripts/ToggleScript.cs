using UnityEngine;

public class ToggleScript: MonoBehaviour
{
    public GameObject ovrRightControllerAnchor;
    public GameObject ovrLeftControllerAnchor;
    public GameObject rightControllerAnchor;
    public GameObject leftControllerAnchor;
    private bool isRightControllerActive = true;
    private bool isLeftControllerActive = true;

    // Call this method when you want to toggle the controller state
    public void ToggleControllerComponents()
    {
        if (ovrRightControllerAnchor != null)
        {
            // Toggle LineRenderer
            LineRenderer lineRenderer = ovrRightControllerAnchor.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.enabled = !lineRenderer.enabled;
            }
            

            // Toggle FishingReelGrab script
            FishingReelGrab fishingReelScript = ovrRightControllerAnchor.GetComponent<FishingReelGrab>();
            if (fishingReelScript != null)
            {
                fishingReelScript.enabled = !fishingReelScript.enabled;
            }
            

            // Toggle the OVRControllerPrefab child
            Transform ovrControllerPrefab = ovrRightControllerAnchor.transform.Find("OVRControllerPrefab");
            if (ovrControllerPrefab != null)
            {
                ovrControllerPrefab.gameObject.SetActive(!ovrControllerPrefab.gameObject.activeSelf);
            }
            

            // Toggle the overall controller state
            isRightControllerActive = !isRightControllerActive;
        }
        // ----------------------------
        if (ovrRightControllerAnchor != null)
        {
            
            // Toggle the OVRControllerPrefab child
            Transform ovrLeftControllerPrefab = ovrLeftControllerAnchor.transform.Find("OVRControllerPrefab");
            if (ovrLeftControllerPrefab != null)
            {
                ovrLeftControllerPrefab.gameObject.SetActive(!ovrLeftControllerPrefab.gameObject.activeSelf);
            }
            

            // Toggle the overall controller state
            isLeftControllerActive = !isLeftControllerActive;
        }
        
        // --------------------------------------------
        if (rightControllerAnchor != null)
        {
            rightControllerAnchor.SetActive(!rightControllerAnchor.activeSelf);
        }
        
        // -------------------------------------------------
        if (leftControllerAnchor != null)
        {
            leftControllerAnchor.SetActive(!leftControllerAnchor.activeSelf);
        }
        
    }

    // Example of how to use the script
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three)) // Button.One is 'A' and Button.Three is 'X'
        {
            ToggleControllerComponents();
        }
    }
}

