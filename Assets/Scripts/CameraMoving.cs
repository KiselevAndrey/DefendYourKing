using UnityEngine;
using DG.Tweening;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeChanging = .5f;
    [SerializeField, Tooltip("X - min, Y - max")] private Vector2 borderZ;
    [SerializeField, Tooltip("X - min, Y - max")] private Vector2 zoomDistance;

    private Transform _camera;

    #region Start Update
    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        ZMoveWithTwoAxis();
        Zoom();
    }
    #endregion

    private void ZMoveWithOneAxis()
    {
        float move = Input.GetAxis("Horizontal");
        float rotate = Input.GetAxis("Vertical");

        if (move != 0)
        {
            float z = transform.position.z;
            if (AboutBorder(z, move, borderZ)) return;

            transform.DOMoveZ(z + move * moveSpeed, timeChanging);
        }
        Rotate(rotate);
    }

    private void ZMoveWithTwoAxis()
    {
        float lateralDirection = Input.GetAxis("Horizontal");
        float forwardDirection = Input.GetAxis("Vertical");

        Vector3 temp = transform.position;

        if (lateralDirection != 0)
            temp += transform.right * lateralDirection * moveSpeed;
        if (forwardDirection != 0)
            temp += transform.forward * forwardDirection * moveSpeed;

        if(!AboutBorder(temp.z, temp.z - transform.position.z, borderZ))
            transform.DOMoveZ(temp.z, timeChanging);

        float rotate = Input.GetAxis("Rotate");
        Rotate(rotate);
    }

    private bool AboutBorder(float z, float moveDirection, Vector2 border) => (z < border.x && moveDirection < 0) || (z > border.y && moveDirection > 0);

    private void Rotate(float rotate)
    {
        if (rotate != 0)
        {
            Vector3 endRotation = transform.rotation.eulerAngles;
            endRotation.y += rotate * rotationSpeed;
            transform.DORotate(endRotation, timeChanging);
        }
    }

    private void Zoom()
    {
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if(mouseScrollWheel != 0 && !AboutBorder(Vector3.Distance(transform.position, _camera.position), -mouseScrollWheel, zoomDistance))
        {
            _camera.transform.DOMove(_camera.position + _camera.forward * mouseScrollWheel * zoomSpeed, timeChanging);
        }
    }
}