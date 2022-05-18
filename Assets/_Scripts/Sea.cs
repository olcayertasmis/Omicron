using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sea : MonoBehaviour
{
    [SerializeField]
    Text scoreText, finishScoreText;

    public Slider slider;

    public float scoreControl;

    public GameObject effectPrefab;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cube")
        {
            Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
            other.gameObject.tag = "SeaCube";
        }
        Score();
    }

    private void Score()
    {
        GameObject[] gameObject;
        gameObject = GameObject.FindGameObjectsWithTag("SeaCube");
        float i = gameObject.Length;
        scoreText.text = "%" + Mathf.Round(((i / Objects.instance.totalPoint) * 100)).ToString();
        finishScoreText.text = scoreText.text;
        scoreControl = (i / Objects.instance.totalPoint) * 100;

        slider.value = Mathf.Round(((i / Objects.instance.totalPoint) * 100));

    }

    private void Update()
    {
        if (scoreControl == 100 || GameManager.instance.newSpawnCount == 0)
        {
            Invoke("FinishControl", 3f);
        }
    }
    void FinishControl()
    {
        GameManager.instance.FinishGame();
    }
}
