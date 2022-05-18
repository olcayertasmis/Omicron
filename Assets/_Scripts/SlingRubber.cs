using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlingRubber : MonoBehaviour
{
    public GameObject bandBone;

    public bool orginalPos = true;

    private void Update()
    {
        if (DragAndShoot.instance.isDrag)
        {
            orginalPos = false;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.5f;
            bandBone.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            bandBone.transform.localPosition = new Vector3(Mathf.Clamp(bandBone.transform.localPosition.x, 0f, 0.058f), 0.0338f, 0);
        }
        else if (!orginalPos)
        {
            orginalPos = true;
            bandBone.transform.DOLocalMove(new Vector3(0.04f, 0.0338f, 0), 0.5f).SetEase(Ease.OutElastic);
        }
    }
}
