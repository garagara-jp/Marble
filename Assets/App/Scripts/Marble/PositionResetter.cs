using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MarbleのPositionをセットし直す
/// Marbleにアタッチ
/// </summary>
public class PositionResetter : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 startPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void Update()
    {
        // リセット処理
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }

        // 落下したときの処理
        if (transform.position.y < -10)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
    }
}
