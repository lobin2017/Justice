using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 cameraOffset = new(0, 12, -5);

    void Update()
    {
        Vector3 cameraPosition = player.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, 4f * Time.deltaTime);
    }
}
