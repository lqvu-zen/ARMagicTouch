using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class DeadScreen : MonoBehaviour
{
    public TMP_Text highscore;
    public TMP_Text score;
    public IntVariable currentScore;
    // Start is called before the first frame update
    public CanvasGroup  mainCanvas;

    public void TurnOn()
    {
        UpdateScore();
        mainCanvas.DOFade(1f, 1f).From(0f).OnComplete(() => {
            mainCanvas.interactable = true;
            mainCanvas.blocksRaycasts = true;
        }
        );
    }
    public void TurnOff()
    {
        mainCanvas.interactable = false;
        mainCanvas.blocksRaycasts = false;
        mainCanvas.DOFade(0f, 1f).From(1f);
    }

    public void UpdateScore()
    {
        score.text = currentScore.value.ToString();
        highscore.text = HighscoreController.GetHightscore().ToString();
    }
}
