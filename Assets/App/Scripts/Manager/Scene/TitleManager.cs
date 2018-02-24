using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Image fadePanel;

    private void Start()
    {
        PanelFader.Fade(fadePanel, 1f, true);
    }
}