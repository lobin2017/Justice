using UnityEngine;

public class CurseWarning : MonoBehaviour
{
    public float duration = 1f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }   
}
