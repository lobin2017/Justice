using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lerpSpeed = 8f;

    private Vector3 targetPos;

    private void LateUpdate()
    {
        if (target == null) return;

        targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            1f - Mathf.Exp(-lerpSpeed * Time.deltaTime)
        );
    }
}