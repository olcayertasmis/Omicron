using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Objects : MonoBehaviour
{
    public float totalPoint;

    public GameObject cubeFloor;

    #region Singleton
    public static Objects instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private void Start()
    {
        totalPoint = transform.childCount;
    }
}
