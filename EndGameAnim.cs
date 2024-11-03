using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EndGameAnim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer blueUI;
    [SerializeField] private SpriteRenderer blueFade;

    private void Start()
    {
        SpriteRenderer spriteRenderer = blueFade.GetComponent<SpriteRenderer>();
    }

    public void EndGameAnimation()
    {

        blueUI.transform.DOLocalMoveY(2f, 1f);
        blueFade.DOFade(0.5f,1.5f);
    }
}
