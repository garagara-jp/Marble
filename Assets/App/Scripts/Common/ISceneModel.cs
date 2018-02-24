using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageParam
{
    public string CurrentStageName { get; set; }

    public int DesireCount { get; set; }

    public int ClickCount { get; set; }
    public float MarblePower { get; set; }
}

public interface ISceneModel
{
    StageParam GetStageParam();
}