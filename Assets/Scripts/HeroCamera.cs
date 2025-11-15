using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeroCamera : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Transform Pivot;

    public Transform Target;
    public Vector3 Offset;
    public float ZoomSpeed = 4f;
    public float MinZoom = 5f;
    public float MaxZoom = 15f;

    public float Pitch = 2f;
    public float YawSpeed = 100f;

    public float CurrentZoom = 10f;
    public float CurrentYaw = 1f;

    public Transform Camera;
    public bool Xray;
    public bool Rotation;
    void LateUpdate()
    {
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
            CurrentZoom = Mathf.Clamp(CurrentZoom, MinZoom, MaxZoom);
        }

        CurrentYaw -= Input.GetAxis("Horizontal") * YawSpeed * Time.deltaTime;

        transform.position = Target.position - Offset * CurrentZoom;
        transform.LookAt(Target.position + Vector3.up * Pitch);

        if (Rotation == true)
        {
            transform.RotateAround(Target.position, Vector3.up, -CurrentYaw);
        }
    //    CameraVision();
        Debug.DrawLine(Target.position, Camera.position, Color.black);
    }

    public void CameraVision()
    {
        if (Xray)
        {
            Ray ray = new Ray(Camera.position, Target.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 30))
            {
                var interacteble = hit.collider.GetComponent<Material>();
                if (interacteble != null && hit.collider.tag == "Ground")
                {
                    print("work");
                    interacteble.color = new Color(255f / 255, 255f / 255, 255f / 255, 100f / 255);
                }
            }
        }
    }
}
