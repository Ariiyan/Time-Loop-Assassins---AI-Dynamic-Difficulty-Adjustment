using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float intensity = 1f;
    public float smooth = 10f;

    private Quaternion originRotation;

    void Start()
    {
        originRotation = transform.localRotation;
    }

    void Update()
    {
        UpdateSway();
    }

    private void UpdateSway()
    {
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y");
        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion targetRotation = originRotation * t_x_adj * t_y_adj;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
