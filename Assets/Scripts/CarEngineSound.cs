using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineSound : MonoBehaviour {

    [SerializeField] private AudioSource engineSound;
    [SerializeField] private Rigidbody car;

    private float currentSpeed;
    private float pitch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = car.velocity.magnitude;
        pitch = currentSpeed / 10f;

        if (pitch < 1f) {
            engineSound.pitch = 1f;
        }
        else {
            engineSound.pitch = pitch;
        }
    }
}
