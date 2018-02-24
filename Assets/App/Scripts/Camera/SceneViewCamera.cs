using UnityEngine;

/// <summary>
/// GameビューにてSceneビューのようなカメラの動きをマウス操作によって実現する
/// </summary>
[RequireComponent(typeof(Camera))]
public class SceneViewCamera : MonoBehaviour
{
    Camera thisCam;

    [SerializeField]
    private float wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 10f)]
    private float moveSpeed = 0.3f;

    [SerializeField, Range(0.1f, 10f)]
    private float rotateSpeed = 0.3f;

    [SerializeField]
    float offset = 10;

    public float _wheelSpeed
    {
        get { return wheelSpeed; }
        set { wheelSpeed = value; }
    }

    public float _offset
    {
        get { return offset; }
        set { offset = value; }
    }

    Vector3 preMousePos;
    Vector3 centerPosition;
    Ray ray;

    void Start()
    {
        thisCam = GetComponent<Camera>();
    }

    private void Update()
    {
        MouseUpdate();
        return;
    }

    /// <summary>
    /// マウスの操作
    /// </summary>
    private void MouseUpdate()
    {
        // スクロールの値を取得
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // スクロールによる操作
        if (scrollWheel != 0.0f)
            MouseWheel(scrollWheel);

        // ドラッグした距離を取得
        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
            preMousePos = Input.mousePosition;

        // ドラッグによる操作
        MouseDrag(Input.mousePosition);
    }

    /// <summary>
    /// マウスホイールによる拡大縮小
    /// </summary>
    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * wheelSpeed;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        // 誤差を無視
        if (diff.magnitude < Vector3.kEpsilon)
            return;

        // 左Alt + 右ドラッグ
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
        {
            // 拡大縮小する方向(1か-1)を計算
            float sign;
            if (Mathf.Pow(diff.x, 2) >= Mathf.Pow(diff.y, 2))
                sign = Mathf.Sign(diff.x);
            else sign = -Mathf.Sign(diff.y);

            // 拡大縮小
            transform.position += sign * transform.forward * diff.magnitude * wheelSpeed * 0.001f;
            preMousePos = mousePos;
            return;
        }

        // 左Alt + 左ドラッグ
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        {
            AltCameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed, offset);
            preMousePos = mousePos;
            return;
        }

        // 中ドラッグ、右ドラッグ
        if (Input.GetMouseButton(2))
            transform.Translate(-diff * Time.deltaTime * moveSpeed);
        else if (Input.GetMouseButton(1))
            CameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed);

        preMousePos = mousePos;
    }

    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }

    public void AltCameraRotate(Vector2 angle, float offset)
    {
        // 画面の左上端の座標を計算
        var topLeft = thisCam.ScreenToWorldPoint(Vector3.zero);
        // 画面の右下端の座標を計算
        var bottomRight = thisCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 画面の中央の座標を計算
        ray = new Ray(transform.position, transform.forward);
        centerPosition = ray.GetPoint(offset);
         
        // 回転
        transform.RotateAround(centerPosition, transform.right, angle.x);
        transform.RotateAround(centerPosition, Vector3.up, angle.y);
    }
}