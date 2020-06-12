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

    float pitchDueToPosition;
    float pitchDueToRotation;
    float pitch;
    float yaw;
    float roll;

     
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
        ShipFiring();
        
        }
        
    }


    private void ShipRotation(){
         pitchDueToPosition = transform.localPosition.y * pitchFactor;
         pitchDueToRotation = VerticalThrow * controlPitchFactor;
         pitch = pitchDueToPosition + pitchDueToRotation;
         yaw = transform.localPosition.x * yawFactor;
         roll = horizontalThrow * controlRollFactor;
         
        if (isDashPossible == true)
        {
            isDoingBarrelRoll = false;
        }
        if(!isDoingBarrelRoll){
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
        }
        if (isDashPossible && Input.GetKeyDown(KeyCode.D))
        {
            DoDoubleDash(true);
        }
        //false for left
        if (isDashPossible && Input.GetKeyDown(KeyCode.A))
        {
            DoDoubleDash(false);
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
       
        isAlive = false;
        
    }
    private void ShipFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire")){
            SetGunsActive(true);
        }else{
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns){
            var gunFX = gun.GetComponent<ParticleSystem>().emission;
            gunFX.enabled = isActive;
        }
    }


    //codigo experimental necesita arreglos

    private void ShipBarrelroll()
    {
        
        //true for right     
      
    }



    public bool isDashPossible
    {
        get
        {
            return Time.time - _lastDashTime > dashWaitTime;
        }
    }


    void DoDoubleDash(bool Side)
    {

        // If second time pressed?
        if (Time.time - _lastDashButtonTime < doubleTapTime)
        {
            _lastDashTime = Time.time;
            isDoingBarrelRoll = true;
            var rotation = transform.localEulerAngles;
            StartRotate(rotation, Side);
        }

        _lastDashButtonTime = Time.time;

    }

    // Rotate the object from it's current rotation to "newRotation" over "duration" seconds
    void StartRotate(Vector3 newRotation,bool side, float duration = 0.3f)
    {

        if (SlerpRotation != null) // if the rotate coroutine is currently running, so let's stop it and begin rotating to the new rotation.
            StopCoroutine(SlerpRotation);
        
        SlerpRotation = Rotate(newRotation,side, duration);       
        StartCoroutine(SlerpRotation);
    }

    IEnumerator SlerpRotation = null;
    IEnumerator Rotate(Vector3 newRotation,bool side, float duration)
    {
        Vector3 PitchAndYaw = new Vector3(pitch,yaw,0f);
        float rotationside;
        if(side){
            rotationside = 180f;
        }else{
            rotationside =-180f;
        }
        //Quaternion startRotation = transform.rotation; // You need to cache the current rotation so that you aren't slerping from the updated rotation each time
        //Quaternion endRotation = Quaternion.Euler(newRotation);
        Vector3 initialRotation = transform.localRotation.eulerAngles;
        Vector3 goalRotation = initialRotation;

        goalRotation.z -= rotationside;

        Vector3 currentRotation = initialRotation;
        currentRotation += new Vector3(0f,yaw,0f);
        //first half of the loop
        for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
        {
            float t = elapsed / duration; // This is the normalized time. It will move from 0 to 1 over the duration of the loop.
            print("memito0" + currentRotation);
            
            print("memito1"+currentRotation);
            currentRotation.z = Mathf.Lerp(initialRotation.z, goalRotation.z, t);
            transform.rotation = Quaternion.Euler(currentRotation);
            yield return null;
        }
        //update data with the end of first loop due Euler
        initialRotation = transform.rotation.eulerAngles;
        goalRotation = initialRotation;
        goalRotation.z -= rotationside;

        //second half of the loop
        for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
        {
            float t = elapsed / duration; // This is the normalized time. It will move from 0 to 1 over the duration of the loop.
            
            currentRotation.z = Mathf.Lerp(initialRotation.z, goalRotation.z, t);
            transform.rotation = Quaternion.Euler(currentRotation);
            yield return null;
        }

        //transform.rotation = endRotation; // finally, set the rotation to the end rotation
        SlerpRotation = null; // Clear out the IEnumerator variable so we can tell that the coroutine has ended.
    }


    









}
