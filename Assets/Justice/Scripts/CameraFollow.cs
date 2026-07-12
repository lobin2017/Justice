using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private float followSpeed = 8f;

    private Transform target;

    private void Start()
    {
        FindPlayer();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        Vector3 desiredPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            1f - Mathf.Exp(-followSpeed * Time.deltaTime)
        );
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }
    }
}