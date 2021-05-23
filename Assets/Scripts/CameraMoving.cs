using UnityEngine;
using DG.Tweening;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeChanging = .5f;
    [SerializeField, Tooltip("X - min, Y - max")] private Vector2 borderZ;

    private void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float rotate = Input.GetAxis("Vertical");

        if (move != 0)
        {
            float z = transform.position.z;
            if ((z < borderZ.x && move < 0) || (z > borderZ.y && move > 0)) return;

            transform.DOMoveZ(z + move * moveSpeed, timeChanging);
        }
        if (rotate != 0)
        {
            Vector3 endRotation = transform.rotation.eulerAngles;
            endRotation.y += rotate * rotationSpeed;
            transform.DORotate(endRotation, timeChanging);
        }  
    } 
}