using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCarController : MonoBehaviour
{
    public Rigidbody myRigidBody;

    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180, gravityForce = 10f, dragOnGround = 3f;
    private float speedInput, turnInput;
    public float yOffset = 0.45f;
    
    private bool grounded;
    
    public LayerMask ground;
    public float groundRayLength = 0.45f;
    public Transform groundRayPoint;
    
    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    public ParticleSystem[] dustTrail;
    public float maxEmission = 25f;
    private float emissionRate;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0) {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0) {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (grounded) {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }
 
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);

        transform.position = myRigidBody.transform.position - new Vector3(0f, yOffset, 0f);
    }

    private void FixedUpdate() {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, ground)) {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        emissionRate = 0;

        if (grounded) {
            myRigidBody.drag = dragOnGround;

            if (Mathf.Abs(speedInput) > 0) {
                myRigidBody.AddForce(transform.forward * speedInput);
                
                emissionRate = maxEmission;
            }
        }
        else {
            myRigidBody.drag = 0.1f;
            myRigidBody.AddForce(Vector3.up * -gravityForce * 100f);
        }

        foreach(ParticleSystem part in dustTrail) {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = emissionRate;
        }
    }
}
