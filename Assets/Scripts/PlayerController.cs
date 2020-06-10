using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
        [Header("General")]
   [Tooltip("In ms")] [SerializeField] float velocity = 50f;
   [Tooltip("In m")] [SerializeField] float xRange = 13f;
   [Tooltip("In m")] [SerializeField] float yRange = 10f;

        [Header("Position Factors")]
   [SerializeField] float pitchFactor = -1f;
   [SerializeField] float yawFactor = 1f;
   
        [Header("Control Factors")]
   
   [SerializeField] float controlRollFactor = -50f;
   [SerializeField] float controlPitchFactor = -20f;
   [SerializeField] GameObject [] guns;

        [Header("Particles")]
  
   float horizontalThrow,VerticalThrow;

   // Must double tap within half a second (by default)
     public float doubleTapTime = 0.5f;
     // Time to wait between dashes
     public float dashWaitTime = 2.0f;

     private float _lastDashButtonTime;
     // Time of the last dash
     private float _lastDashTime;
   private bool isAlive = true;
   private bool isDoingBarrelRoll = false;


     
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        {
        ShipMovementX();
        ShipMovementY();
        ShipRotation();
        ShipBarrelroll();
        ShipFiring();
        
        }
        
    }

    private void ShipBarrelroll()
    {
         if(isDashPossible== true){
            isDoingBarrelRoll=false;
         }
         if (isDashPossible && Input.GetKeyDown(KeyCode.D)) {
             // If second time pressed?
             if (Time.time - _lastDashButtonTime < doubleTapTime)
                 DoDoubleDash();
             _lastDashButtonTime = Time.time;
         }
    }
     bool isDashPossible {
         get {
             return Time.time - _lastDashTime > dashWaitTime;
         }
     }
        void DoDoubleDash() {
         _lastDashTime = Time.time;
         isDoingBarrelRoll = true;
        
         //todo rotar ignorando shiprotation en un tiempo
        
     }

    private void ShipRotation(){
        if(!isDoingBarrelRoll){
        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToRotation = VerticalThrow*controlPitchFactor ;

        float pitch= pitchDueToPosition + pitchDueToRotation;
        float yaw= transform.localPosition.x * yawFactor;
        float roll= horizontalThrow*controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
        }else{
            return;
        }
    }

    private void ShipMovementX()
    {
        horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffSet = velocity * Time.deltaTime * horizontalThrow;
        float rawNewXPos = xOffSet + transform.localPosition.x;
        float ClampedPosX = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        transform.localPosition = new Vector3(ClampedPosX, transform.localPosition.y, transform.localPosition.z);
    }
     private void ShipMovementY()
    {
        VerticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffSet = velocity * Time.deltaTime * VerticalThrow;
        float rawNewYPos = yOffSet + transform.localPosition.y;
        float ClampedPosY = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, ClampedPosY, transform.localPosition.z);
    }
    public void Dying(){
        print("shit thats all you had to say negro");
        isAlive = false;
        
    }
    private void ShipFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire")){
            ActivateGuns();
        }else{
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach(GameObject gun in guns){
            gun.SetActive(true);
        }
    }
       private void DeactivateGuns()
    {
       foreach(GameObject gun in guns){
           gun.SetActive(false);
       }
    }
}
