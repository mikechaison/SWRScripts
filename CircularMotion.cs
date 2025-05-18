using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform center; // The center point of the circle
    public float radius = 5f; // Radius of the circle
    public float speed = 2f; // Speed of rotation

    private float angle = 180f;

    void FixedUpdate()
    {
        angle += speed; //* Time.deltaTime; // Increment the angle over time

        float x = center.position.x + Mathf.Cos(angle) * radius;
        float z = center.position.z + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, transform.position.y, z);

        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)); 

        transform.rotation = Quaternion.LookRotation(direction);
    }
}

