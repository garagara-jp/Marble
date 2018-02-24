using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StageCanvasManager : MonoBehaviour
{
    private ISceneModel sceneModel;
    private StageParam param = new StageParam();

    [SerializeField]
    private Text currentStageText;
    [SerializeField]
    private Text desireCountText;
    [SerializeField]
    private Text clickCountText;
    [SerializeField]
    private Text marblePowerText;

    private void Start()
    {
        sceneModel = MyInterfaceController.FindObjectOfInterface<ISceneModel>();
        if (sceneModel != null)
        {
            param = sceneModel.GetStageParam();

            // 値をセット
            currentStageText.text = param.CurrentStageName;
            desireCountText.text = param.DesireCount.ToString();
        }
    }

    private void Update()
    {
        if (sceneModel != null)
        {
            param = sceneModel.GetStageParam();

            clickCountText.text = param.ClickCount.ToString();
            marblePowerText.text = param.MarblePower.ToString();
        }
        else Debug.Log("ステージパラメーターを取得できません");
    }
}
