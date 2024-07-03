using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sources: https://www.youtube.com/watch?v=cqATTzJmFDY&t=1s

public class BasicCarController : MonoBehaviour
{
    public Rigidbody myRigidBody;

    [SerializeField]
    private float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 80f, turnStrength = 180, maxWheelTurn = 25f, gravityForce = 10f, dragOnGround = 3f, groundRayLength = 0.45f, yOffset = 0.45f, driftThreshold = 30f, minDriftSpeed = 15f;
    
    [SerializeField] private Transform groundRayPoint, leftFrontWheel, rightFrontWheel;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject _carParticleObject;
    private ParticleSystemManager _particleManager;
    
    private float speedInput, turnInput;
    private bool grounded, isDrifting;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.transform.parent = null;

        //_particleManager = _carParticleObject.GetComponent<ParticleSystemManager>();
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
            transform.rotation =  Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        //leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, leftFrontWheel.localRotation.eulerAngles.y + 100f * Time.deltaTime, leftFrontWheel.localRotation.eulerAngles.z);
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, -90 + turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);

        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, -90 + turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);

        transform.position = myRigidBody.transform.position - new Vector3(0f, yOffset, 0f);
    }

    private void FixedUpdate() {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, ground)) {
            grounded = true;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, 0.1f);
        }

        //ControlDriftEffect();

        if (grounded) {
            myRigidBody.drag = dragOnGround;

            if (Mathf.Abs(speedInput) > 0) {
                myRigidBody.AddForce(transform.forward * speedInput);
            }
        }
        
        else {
            myRigidBody.drag = 0.1f;
            myRigidBody.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }

    private void ControlDriftEffect() {
        if (myRigidBody.velocity.magnitude > minDriftSpeed && grounded) {
            Vector3 wheelDirection = leftFrontWheel.TransformDirection(Vector3.forward);
            Vector3 carVelocityDirection = myRigidBody.velocity.normalized;

            float driftAngle = Vector3.Angle(wheelDirection, carVelocityDirection);

            if (driftAngle > driftThreshold) {
                _particleManager.StartDriftEffect();
                return;
            }
        }
        _particleManager.StopDriftEffect();
    }
}
