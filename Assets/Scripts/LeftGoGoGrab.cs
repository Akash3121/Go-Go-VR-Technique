using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGoGoGrab : MonoBehaviour
{
    // public float gain = 1.5f; 
    public Transform centerEyeAnchor;
    public Transform selectionSphere;
   
    private Vector3 initialSpherePosition;

    public Material indicationMaterial;
    public Material selectionMaterial;

    private GameObject grabbed = null;

    void Start()
    {
        // Store the initial position of the selectionSphere
        initialSpherePosition = selectionSphere.position;

        // virtualControllerAnchor.position = initialSpherePosition;
        transform.position = initialSpherePosition;
    }

    void Update()
    {

        // Get the positions of the center eye anchor and the selection sphere
        Vector3 centerEyePosition = centerEyeAnchor.position;
        Vector3 spherePosition = selectionSphere.position;

        centerEyePosition.y = centerEyePosition.y - 0.4f;

        Vector3 headToController = centerEyePosition - spherePosition; // this is the d - magnitude

        float threshold = 0.5f;

        float s = 1.0f;

        float d = headToController.magnitude;

        if (d > threshold)
        {
            // s = d * d;
            // s = s * s * s * s * 10.0f;
            s = s * 22.0f;
            // Exponential growth
            // s = Mathf.Pow(s, 3.0f); // You can adjust the exponent as needed
        }
        else
        {
            s = 1.0f; // Move with the same speed as spherePosition within the threshold
            // virtualControllerAnchor.position = spherePosition;
            transform.position = spherePosition;
        }
        // Neglect the y-components
        // centerEyePosition.y = 0;
        // spherePosition.y = 0;
        

        // Calculate movement based on the change in position of the selectionSphere
        // Vector3 movement = (spherePosition - initialSpherePosition) * gain;
        // Vector3 movement = (spherePosition - initialSpherePosition);
        Vector3 movement = (spherePosition - initialSpherePosition) * s;

        // Move the virtualControllerAnchor in the virtual world
        // virtualControllerAnchor.Translate(movement * Time.deltaTime);
        // virtualControllerAnchor.Translate(movement);
        transform.Translate(movement);

        // Update the initial position of the selectionSphere
        initialSpherePosition = spherePosition;
        // initialSpherePosition = selectionSphere.position; // setting back to initial position.

        if (grabbed != null)
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) < 0.05f) // if clicked trigger very slightly
            {
                //trigger relased
                grabbed = null;
            }
            else
            {
                grabbed.transform.position = transform.position;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (grabbed == null)
        {
            // Check if the collided object has the "Grabbable" tag
            if (other.gameObject.tag == "grabbable")
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.9f)
                {
                    Debug.Log("Triggered with Grabbable object!");
                    grabbed = other.gameObject;
                    // Change the material of the collided object to the indicationMaterial
                    /*Renderer renderer = other.gameObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = indicationMaterial;
                    }*/
                    other.gameObject.transform.position = transform.position;
                }
            }
        }
    }

}
