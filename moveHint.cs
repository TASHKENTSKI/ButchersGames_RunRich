using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveHint : MonoBehaviour
{
    [SerializeField] private List<Transform> hintAnimPoints;
    public GameObject hintBar;
     public GameObject hint;
     
 
    public bool showHint = true;

    private void Start()
    {
        StartCoroutine(ShowHintToStartGame());
    }


    public void BreakYoyoLoop()
    {
        hintBar.SetActive(false);
        hint.SetActive(false);
        
    }

    public void ShowHintToStartVoid()
    {
        
        hintBar.SetActive(true);
        hint.SetActive(true);
        StartCoroutine(ShowHintToStartGame());
        
    }
    
    IEnumerator ShowHintToStartGame()
    {
        yield return hint.transform.DOMoveX(hintAnimPoints[0].position.x, 0.5f)
            .OnComplete(() =>
            {
                hint.transform.DOMoveX(hintAnimPoints[1].position.x, 0.5f)
                    .SetEase(Ease.InOutCubic)
                    .SetLoops(-1, LoopType.Yoyo);
            });
    }

}
