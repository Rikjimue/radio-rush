using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // [SerializeField]
    public int acceleration;
    public Rigidbody rigidBody;

    public GameObject body;
    public GameObject frontRightWheel;
    public GameObject frontLeftWheel;
    public GameObject backRightWheel;
    public GameObject backLeftWheel;

    void FixedUpdate()
    {
        Debug.Log(Input.GetAxis("Vertical"));
        rigidBody.AddForceAtPosition(body.transform.forward * acceleration * Input.GetAxis("Vertical") * Time.fixedDeltaTime,
                                    backRightWheel.transform.position,
                                    ForceMode.Acceleration);
        rigidBody.AddForceAtPosition(body.transform.forward * acceleration * Input.GetAxis("Vertical") * Time.fixedDeltaTime,
                                    backLeftWheel.transform.position,
                                    ForceMode.Acceleration);
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
