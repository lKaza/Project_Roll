using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

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

        [Header("Particles")]
  
   float horizontalThrow,VerticalThrow;
   private bool isAlive = true;


     
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
        }
        
    }


    private void ShipRotation(){
        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToRotation = VerticalThrow*controlPitchFactor ;

        float pitch= pitchDueToPosition + pitchDueToRotation;
        float yaw= transform.localPosition.x * yawFactor;
        float roll= horizontalThrow*controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
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
}
