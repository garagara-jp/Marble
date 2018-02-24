using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneModel : MonoBehaviour, ISceneModel
{
    private StageParam param = new StageParam();

    [SerializeField]
    private string currentStageName;
    [SerializeField]
    private int desireCount;

    [SerializeField]
    private MarbleStatusModel marbleModel;
    private int clickCount;
    private float marblePower;

    private void Update()
    {
        clickCount = marbleModel.ClickCount;
        marblePower = Mathf.Floor(marbleModel.DragPower);
    }

    public StageParam GetStageParam()
    {
        param.CurrentStageName = currentStageName;
        param.DesireCount = desireCount;

        param.ClickCount = clickCount;
        param.MarblePower = marblePower;

        return param;
    }
}
