
using DG.Tweening;
using UnityEngine;

public class LoseAnim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer blueUI;
    [SerializeField] private SpriteRenderer redFade;

    private void Start()
    {
        SpriteRenderer spriteRenderer = redFade.GetComponent<SpriteRenderer>();
    }

    public void LoseGameAnimation()
    {

        blueUI.transform.DOLocalMoveY(1.844f, 1f);
        redFade.DOFade(0.5f,1.5f);
    }
}