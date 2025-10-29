using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -10f);

    private void LateUpdate()
    {
        if (_target == null) return;

        transform.position = _target.position + offset;
    }
}