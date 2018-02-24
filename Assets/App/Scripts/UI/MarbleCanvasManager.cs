using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ビー玉に付随するUIを表示するCanvasを管理する
/// Marble用のCanvas(Worldスペース)にアタッチ
/// </summary>
public class MarbleCanvasManager : MonoBehaviour
{
    [SerializeField]
    private Transform marble;
    [SerializeField]
    private MarbleStatusModel marbleModel;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Image arrow;
    private Vector3 arrowScale = Vector3.one;
    [SerializeField]
    private float arrowLengthPercent = 200; // 矢印の長さのパーセント

    private void Awake()
    {
        FetchCam();
    }

    private void Update()
    {
        ExtendArrow();
        FetchCam();
    }

    private void LateUpdate()
    {
        SetCanvasTransform();
    }


    private void ExtendArrow()
    {
        var a = transform.InverseTransformDirection(marbleModel.DragVec);
        float angle = -Vector3.Angle(transform.forward, a) + 180;
        if (a.x < 0) angle = (360 - angle);

        arrow.rectTransform.rotation = Quaternion.Euler(-90, 0, angle);

        arrowScale.y = (marbleModel.DragPower / marbleModel.MaxDragPower) * (arrowLengthPercent / 100);
        arrow.rectTransform.localScale = arrowScale;
    }

    private void FetchCam()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void SetCanvasTransform()
    {
        transform.position = marble.position;
        transform.rotation = Quaternion.LookRotation(Vector3.up);
    }
}
