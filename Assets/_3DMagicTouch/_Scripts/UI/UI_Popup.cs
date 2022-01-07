using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Popup : MonoBehaviour
{

    public float popupDuration;
    public Ease popupEaseType;
    public float closeDuration;
    public Ease closeEaseType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Popup()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, popupDuration).SetEase(popupEaseType).From(Vector3.zero);
    }

    public void Close()
    {
        transform.DOScale(Vector3.zero, closeDuration)
            .SetEase(closeEaseType)
            .From(Vector3.one)
            .OnComplete(()=>gameObject.SetActive(false));
        // gameObject.SetActive(false);
    }
}
