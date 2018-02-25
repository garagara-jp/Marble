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
    private Vector3 resetPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
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

    private void OnTriggerEnter(Collider col)
    {
        // セーブポイントに到達したらポジションを保存する
        // セーブポイント自体はディアクティブにする
        if (col.tag == "SavePoint")
        {
            resetPos = col.transform.position;
            col.gameObject.SetActive(false);
        }
    }

    private void ResetPosition()
    {
        transform.position = resetPos;
        rb.velocity = Vector3.zero;
    }
}
