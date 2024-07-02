using UnityEngine;

// Sources: https://www.youtube.com/watch?v=ZBj3LBA2vUY || https://gist.github.com/bendux/76a9b52710b63e284ce834310f8db773

public class CarCameraFollow : MonoBehaviour {
    [SerializeField] private Transform carTransform;
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private Vector3 offsetForward = new Vector3(0f, 4f, -7f);
    [SerializeField] private Vector3 offsetBackward = new Vector3(0f, 4f, 7f);
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private float rotationSmoothTime = 0.15f;
    [SerializeField] private float lookAheadDistance = 10f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotationVelocity = Vector3.zero;

    private void Start() {
        if (carTransform == null || carRigidbody == null) {
            Debug.LogError("Car transform or Rigidbody is not set!");
            enabled = false;
            return;
        }
    }

    private void LateUpdate() { // With LateUpdate, Sphere must be set to Interpolate
        if (carTransform == null || carRigidbody == null) return;

        Vector3 targetPosition;
        Quaternion targetRotation;

        bool isReversing = Vector3.Dot(carTransform.forward, carRigidbody.velocity) < 0;

        if (isReversing) {
            targetPosition = carTransform.TransformPoint(offsetBackward);
            targetRotation = Quaternion.LookRotation(carTransform.position - targetPosition);
        }
        else {
            Vector3 lookAheadPoint = carTransform.position + carTransform.forward * lookAheadDistance;
            targetPosition = carTransform.TransformPoint(offsetForward);
            targetRotation = Quaternion.LookRotation(lookAheadPoint - targetPosition);
        }
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.rotation = QuaternionSmoothDamp(transform.rotation, targetRotation, ref rotationVelocity, rotationSmoothTime);
    }

    private Quaternion QuaternionSmoothDamp(Quaternion current, Quaternion target, ref Vector3 angleVelocity, float smoothTime) {
        Vector3 c = current.eulerAngles;
        Vector3 t = target.eulerAngles;
        return Quaternion.Euler(
            Mathf.SmoothDampAngle(c.x, t.x, ref angleVelocity.x, smoothTime),
            Mathf.SmoothDampAngle(c.y, t.y, ref angleVelocity.y, smoothTime),
            Mathf.SmoothDampAngle(c.z, t.z, ref angleVelocity.z, smoothTime)
        );
    }
}