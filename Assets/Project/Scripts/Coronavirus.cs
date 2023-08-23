using UnityEngine;

public class Coronavirus : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 25.0f;
    [SerializeField]
    private float _raycastDistance = 7.0f;
    [SerializeField]
    private float Mass = 15;
    [SerializeField]
    private float MaxVelocity = 3;
    [SerializeField]
    private float MaxForce = 15;
    private Vector3 velocity;
    [SerializeField]
    private Transform target;
    private bool pursuitPlayer = false;
    void Start()
    {
        velocity = Vector3.zero;
    }

    void Update()
    {
        if (!pursuitPlayer) {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
            configureRaycast();
        } else {
            configureSteering();
        }
    }

    private void configureRaycast() {
        int layerMask = 1 << 3;
        layerMask |= 1 << 6;

        RaycastHit hitInfo;

        bool hit = Physics.Raycast(
                transform.position,
                transform.TransformDirection(Vector3.forward),
                out hitInfo,
                _raycastDistance,
                layerMask
        );

        if (hit)
        {
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.forward) * _raycastDistance,
                Color.red
            );

            if (hitInfo.collider.gameObject.layer == 3)
            {
                pursuitPlayer = true;
            }
        }
        else
        {
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.forward) * _raycastDistance,
                Color.green
            );
        }
    }

    private void configureSteering() {  
        var desiredVelocity = target.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering.y = 0;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }
}