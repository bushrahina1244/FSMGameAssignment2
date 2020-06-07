using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMyVision : MonoBehaviour
{
    // How sensitive we are about line of vision/my line of sight?
    public enum enmSensitivity { HIGH , LOW };

    // variable to check sensitivity
    public enmSensitivity sensitivity = enmSensitivity.HIGH;

    //Are we able to see the target now
    public bool targetInSight = false;

    //Feild of vision
    public float feildOfVision = 45f;

    // we need to referance to our target here
    private Transform target = null;

    //Reference to our eyes- yet to add
    public Transform myEyes = null;

    //my Transform component?
    public Transform npcTransform = null;

    //My sphere collider
    private SphereCollider sphereCollider = null;

    //last known sight of object?
    public Vector3 LastKnownSighting = Vector3.zero;

    private void Awake()
    {
        npcTransform = GetComponent<Transform>();
        sphereCollider = GetComponent<SphereCollider>();
        LastKnownSighting = npcTransform.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // tag it later

    }

    bool InMyFeildOfVision()
    {
        Vector3 dirToTarget = target.position - myEyes.position;

        //Get angle betweenforward and view direction
        float angle = Vector3.Angle(myEyes.forward, dirToTarget);

        //Let us check if within feild of view
        if (angle <= feildOfVision)
            return true;
        else
            return false;

    }

    // We need a function to check line of sight
    bool ClearLineOfSight()
    {
        RaycastHit hit;
        if(Physics.Raycast(myEyes.position ,(target.position - myEyes.position).normalized , out hit, sphereCollider.radius))
        { 
           if (hit.transform.CompareTag("Player"))
           {
                return true;
            
        
           }
         
        }
        return false;
    }
    void UpdateSight()
    {
        switch (sensitivity)
        {
            case enmSensitivity.HIGH:
                targetInSight = InMyFeildOfVision() && ClearLineOfSight();
                break;
             case enmSensitivity.LOW:
                targetInSight = InMyFeildOfVision() && ClearLineOfSight();
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        UpdateSight();
        // last known sighting
        if (targetInSight)
            LastKnownSighting = target.position;

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        targetInSight = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
