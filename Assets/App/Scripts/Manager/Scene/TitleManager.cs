using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Image fadePanel;

    [SerializeField]
    private GameObject omakeUI;

    [SerializeField]
    private GoalManager[] goalManagers;

    private void Awake()
    {
        omakeUI.SetActive(false);
        GameManager.Instance.SetCurrentState(GameState.Title);
    }

    private void Start()
    {
        PanelFader.Fade(fadePanel, 1f, true);

        var stream = this.UpdateAsObservable().Publish().RefCount();

        stream.Where(_ => GameManager.Instance.CurrentGameState == GameState.Title)
            .Subscribe(_ => CheckGoal());
        stream.Where(_ => GameManager.Instance.CurrentGameState == GameState.Result)
            .First()
            .Subscribe(_ => ShowOmake());
    }

    private void CheckGoal()
    {
        Debug.Log("ゴールをチェックしています");

        if (goalManagers.All(x => x.isGoal))
        {
            Debug.Log("全てのゴール地点が達成されました");
            GameManager.Instance.SetCurrentState(GameState.Result);
            return;
        }
    }

    // タイトル画面でゴールしたらオマケを見せる
    private void ShowOmake()
    {
        omakeUI.SetActive(true);
    }
}