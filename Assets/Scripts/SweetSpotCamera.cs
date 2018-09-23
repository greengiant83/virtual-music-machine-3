using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SweetSpotCamera : MonoBehaviour
{
    public Transform ScreenQuad;

    GameObject SensorLocation;
    GameObject DefaultEyeLocation;
    GameObject ScreenLocation;
    GameObject WorldTransform;
    new Camera camera;

    Vector3 vr, vu, vn;
    Vector3 pa, pb, pc;


    void Start()
    {
        //Orthogonal vectors
        vr = new Vector3(1, 0, 0);
        vu = new Vector3(0, 1, 0);
        vn = new Vector3(0, 0, 1);

        camera = GetComponent<Camera>();

        updatePerspective();
    }

    void Update()
    {
        updatePerspective();
    }

    void updatePerspective()
    {
        //Eye location
        Vector3 pe = transform.position;

        //Window's physical corners
        pa = ScreenQuad.TransformPoint(new Vector3(-0.5f, -0.5f, 0));
        pb = ScreenQuad.TransformPoint(new Vector3(0.5f, -0.5f, 0));
        pc = ScreenQuad.TransformPoint(new Vector3(-0.5f, 0.5f, 0));

        //Eye to corner vectors
        Vector3 va = pa - pe;
        Vector3 vb = pb - pe;
        Vector3 vc = pc - pe;
        
        //Distance to screen
        float d = (Vector3.Dot(vn, va)); 
        
        //Near and far clipping planes
        //float n = 0.01f, f = 50f;
        float n = camera.nearClipPlane;
        float f = camera.farClipPlane;

        float l = Vector3.Dot(vr, va) * n / d;
        float r = Vector3.Dot(vr, vb) * n / d;
        float t = Vector3.Dot(vu, vc) * n / d;
        float b = Vector3.Dot(vu, va) * n / d;
        
        Matrix4x4 frustum = glFrustum(l, r, b, t, n, f);
        Matrix4x4 projection = frustum;

        //camera.nearClipPlane = n;
        //camera.farClipPlane = f;
        camera.aspect = (r - l) / (t - b);
        camera.fieldOfView = (Mathf.Atan(t / n) - Mathf.Atan(b / n)) * 57.2957795f;

        camera.projectionMatrix = projection;
    }

    Matrix4x4 glFrustum(float l, float r, float b, float t, float n, float f)
    {
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = (2 * n) / (r - l);
        m[0, 1] = 0;
        m[0, 2] = (r + l) / (r - l);
        m[0, 3] = 0;

        m[1, 0] = 0;
        m[1, 1] = (2 * n) / (t - b);
        m[1, 2] = (t + b) / (t - b);
        m[1, 3] = 0;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = -(f + n) / (f - n);
        m[2, 3] = -(2 * f * n) / (f - n);

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = -1;
        m[3, 3] = 0;

        return m;
    }

    Matrix4x4 glTranslatef(float x, float y, float z)
    {
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = 1;
        m[0, 1] = 0;
        m[0, 2] = 0;
        m[0, 3] = x;

        m[1, 0] = 0;
        m[1, 1] = 1;
        m[1, 2] = 0;
        m[1, 3] = y;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = 1;
        m[2, 3] = z;

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = 0;
        m[3, 3] = 1;
        return m;
    }
}
