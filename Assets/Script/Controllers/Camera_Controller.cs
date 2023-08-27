using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    float forward_speed = 1.0f;
    float backward_speed = 1.0f;
    float left_speed = 1.0f;
    float right_speed = 1.0f;
    public float zoomSpeed = 5000.0f;
    public float fov_max = 100f;
    public float fov_min = 50f;
    public Vector3 lookat_pos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 init_pos = new Vector3(-1.0f, 3.0f, -3.0f) ;

    private Vector3 Lookat_pos;
    float timecount = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = init_pos;
        this.transform.LookAt(lookat_pos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Key_Custom.key_bounding.forward))
        {
            this.transform.position += forward_speed * new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime;
        }
        if (Input.GetKey(Key_Custom.key_bounding.backward))
        {
            this.transform.position += backward_speed * new Vector3(0.0f, 0.0f, -1.0f) * Time.deltaTime;
        }
        if (Input.GetKey(Key_Custom.key_bounding.right))
        {
            this.transform.position += right_speed * new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Input.GetKey(Key_Custom.key_bounding.left))
        {
            this.transform.position += left_speed * new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Input.GetKey(Key_Custom.key_bounding.rotate_right))
        {
            timecount += Time.deltaTime;
            if (timecount >  Mathf.PI) timecount -= 2 * Mathf.PI;
            Vector3 v = init_pos - lookat_pos;
            float radius = new Vector2(v.x, v.z).magnitude;
            this.transform.position = radius * new Vector3(Mathf.Cos(timecount), 0.0f, Mathf.Sin(timecount)) + new Vector3(lookat_pos.x, init_pos.y ,lookat_pos.z);
            this.transform.LookAt(lookat_pos);
        }
        if (Input.GetKey(Key_Custom.key_bounding.rotate_left))
        {
            timecount -= Time.deltaTime;
            if (timecount < -Mathf.PI) timecount += 2 * Mathf.PI;
            Vector3 v = init_pos - lookat_pos;
            float radius = new Vector2(v.x, v.z).magnitude;
            this.transform.position = radius * new Vector3(Mathf.Cos(timecount), 0.0f, Mathf.Sin(timecount)) + new Vector3(lookat_pos.x, init_pos.y, lookat_pos.z);
            this.transform.LookAt(lookat_pos);
        }

        float step = zoomSpeed * Time.deltaTime;
        float fov = this.GetComponent<Camera>().fieldOfView;
        if (Input.mouseScrollDelta.y != 0 && fov <= fov_max && fov >= fov_min)
        {
            this.GetComponent<Camera>().fieldOfView += -Input.GetAxis("Mouse ScrollWheel") * step;
            if (this.GetComponent<Camera>().fieldOfView < fov_min) this.GetComponent<Camera>().fieldOfView = fov_min;
            if (this.GetComponent<Camera>().fieldOfView > fov_max) this.GetComponent<Camera>().fieldOfView = fov_max;
        }
    }
}
