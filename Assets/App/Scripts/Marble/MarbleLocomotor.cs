using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleLocomotor : MonoBehaviour
{
    Rigidbody rb;
    MarbleStatusModel model;
    RectTransform rectTransform = null;

    Vector3 objScreenPoint;
    Vector3 startMousePosition;
    Vector3 currentMouseScreenPoint = new Vector3();
    private Vector3 offsetVec = new Vector3();    // ビー玉が実際に動く向きを表すベクトル

    bool isMouseDown;  // ドラッグを有効にするかどうか

    [SerializeField]
    private float power = 100f;
    [SerializeField]
    private float maxPower = 500f;

    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        model = GetComponent<MarbleStatusModel>();
        rectTransform = GetComponent<RectTransform>();

        // ステータスモデルに値をセット
        model.MovePower = power;
        model.MaxDragPower = maxPower;

        isMouseDown = false;
        FetchCam();
    }

    private void Update()
    {
        // モデルの値を参照
        power = model.MovePower;

        // モデルに値をセット
        model.DragPower = Mathf.Min(offsetVec.magnitude * power, maxPower);
        model.DragVec = offsetVec;
        model.IsClicked = isMouseDown;

        FetchCam();

        // ドラッグ中に右クリックで処理を停止
        if (isMouseDown && Input.GetMouseButtonDown(1))
        {
            offsetVec = Vector3.zero;
            isMouseDown = false;
        }
    }

    private void OnMouseDown()
    {
        //カメラから見たオブジェクトの現在位置を画面位置座標に変換
        objScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //マウスのscreenPointの値を変数に格納
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        // クリック時のマウスのワールド座標
        startMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, objScreenPoint.z));

        // マウス押下のフラグをON
        isMouseDown = true;
    }

    private void OnMouseDrag()
    {
        // マウス押下のフラグを確認
        if (isMouseDown)
        {
            //ドラッグ時のマウス位置を変数に格納
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            //ドラッグ時のマウス位置をスクリーン座標に変換する
            currentMouseScreenPoint.x = x;
            currentMouseScreenPoint.y = y;
            currentMouseScreenPoint.z = objScreenPoint.z;

            // ドラッグ時のマウスのワールド座標
            var currentMousePos = Camera.main.ScreenToWorldPoint(currentMouseScreenPoint);

            // ドラッグした方向とは反対方向の差分ベクトルを取得
            offsetVec = startMousePosition - currentMousePos;
            offsetVec.y = 0;

            //Debug.DrawRay(transform.position, offsetVec, Color.red, Time.deltaTime);
        }
    }

    private void OnMouseUp()
    {
        if (isMouseDown)
        {
            // モデルのクリックカウントを1増やす
            model.ClickCount += 1;

            // オブジェクトを動かす
            var vec = offsetVec * power;
            if (vec.magnitude > maxPower) vec = vec.normalized * maxPower;
            rb.AddForce(vec, ForceMode.Acceleration);

            // 差分ベクトルをリセット
            offsetVec = Vector3.zero;
        }

        // マウス押下のフラグをOFF
        isMouseDown = false;
    }

    private void FetchCam()
    {
        if (cam == null)
            cam = Camera.main;
    }
}