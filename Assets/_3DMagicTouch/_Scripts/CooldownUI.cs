using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CooldownUI : MonoBehaviour
{
    public Image cooldownImage;

    public void SetCooldown(float sec)
    {
        cooldownImage.DOFillAmount(0, sec).From(1f).SetEase(Ease.Linear);
    }
}
