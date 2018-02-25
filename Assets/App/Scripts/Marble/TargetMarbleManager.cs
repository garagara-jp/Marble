using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarbleManager : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 resetPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(true);
        resetPos = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Result)
        {
            gameObject.SetActive(false);
        }

        // 落下したときの処理
        if (transform.position.y < -10)
        {
            ResetPosition();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "GoalArea")
        {
            resetPos = col.transform.position;
        }
    }

    private void ResetPosition()
    {
        transform.position = resetPos;
        rb.velocity = Vector3.zero;
    }
}
