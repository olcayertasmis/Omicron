using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;

    private bool isShoot;
    public bool isDrag = false;

    private float forceMultiplier = 100f;

    public List<GameObject> mans = new List<GameObject>();

    #region Singleton
    public static DragAndShoot instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        var floorMans = GameObject.FindGameObjectsWithTag("OnFloorMan");
        foreach (var floorMan in floorMans)
        {
            mans.Add(floorMan);
        }
    }

    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        isDrag = true;
        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        Vector3 forceV = (new Vector3(forceInit.x, Mathf.Clamp(forceInit.y, -315f, 0f), Mathf.Clamp(forceInit.y, -315f, 0f))) * forceMultiplier;
        if (!isShoot)
        {
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }
    }

    private void OnMouseUp()
    {
        isDrag = false;
        DrawTrajectory.Instance.HideLine();
        mouseReleasePos = Input.mousePosition;
        Shoot(mousePressDownPos - mouseReleasePos);
    }

    void Shoot(Vector3 Force)
    {
        if (isShoot)
        {
            return;
        }
        rb.isKinematic = false;
        rb.AddForce(new Vector3(Force.x, Mathf.Clamp(Force.y, 0f, 315f), Mathf.Clamp(Force.y, 0f, 315f)) * forceMultiplier);

        if (mans.Count > 0)
        {
            GameManager.instance.anim.Play("walking");
            mans[mans.Count - 1].transform.DOLocalMoveX(0f, 1f).OnComplete(() =>
            {
                GameManager.instance.anim.Play("climbing");
                mans[mans.Count - 1].transform.DOLocalMoveY(3.8f, 3f);
            });
            mans[mans.Count - 1].transform.DOLocalMoveZ(-8.5f, 3f).OnComplete(() =>
            {
                GameManager.instance.anim.SetTrigger("Idle");
                mans[mans.Count - 1].AddComponent<DragAndShoot>();
            });
            mans[mans.Count - 1].tag = "Player";
        }
        GameManager.instance.newSpawnCount--;

        isShoot = true;
        Destroy(GetComponent<DragAndShoot>());
    }
}