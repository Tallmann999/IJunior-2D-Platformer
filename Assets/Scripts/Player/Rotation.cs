using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void RotationInspector(float direction)
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
            rotation.y = 180;
            transform.rotation = rotation;
        }
    }
}
