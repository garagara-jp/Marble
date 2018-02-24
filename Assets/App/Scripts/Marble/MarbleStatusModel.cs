using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleStatusModel : MonoBehaviour
{
    public int ClickCount { get; set; }   // ゲーム中にクリックされた回数

    public float DragPower { get; set; }    // 実際に動くときの力
    public float MaxDragPower { get; set; }
    public Vector3 DragVec { get; set; }

    public float MovePower { get; set; }

    public bool IsClicked { get; set; }

    private void Awake()
    {
        ClickCount = 0;
        DragVec = Vector3.zero;
    }
}
