using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarbleManager : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Result)
        {
            gameObject.SetActive(false);
        }
    }
}
