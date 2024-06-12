using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 target_offset;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + target_offset, .125f);
    }
}
