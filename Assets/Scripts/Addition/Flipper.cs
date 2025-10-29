using UnityEngine;

public class Flipper : MonoBehaviour
{
    private float _rotationAngle = 180;

    public void Flip(float direction)
    {
        if (direction > 0)
        {
            Quaternion rotation = transform.rotation;
            rotation.y = 0;
            transform.rotation = rotation;
        }
        else
        {
            Quaternion rotation = transform.rotation;
            rotation.y = _rotationAngle;
            transform.rotation = rotation;
        }
    }
}
