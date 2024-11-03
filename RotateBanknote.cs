using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateBanknote : MonoBehaviour
{ 
    private void Start()
    {
        // Устанавливаем постоянное вращение вокруг оси Y
        transform.DORotate(new Vector3(0, 360,0), 8f)
            .SetLoops(-1, LoopType.Incremental); 
    }
}
