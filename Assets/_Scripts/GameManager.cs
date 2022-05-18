using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject startPanel, inGamePanel, finishPanel;

    [SerializeField]
    Text bulletText;

    public GameObject obj, Man;

    public int spawnCount, newSpawnCount;

    public Animator anim;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SpawnMans();
        newSpawnCount = spawnCount + 1;
    }

    private void Update()
    {
        bulletText.text = (newSpawnCount + " Shot Left").ToString();
    }

    public void SpawnMans()
    {
        for (int i = 0; i < spawnCount; ++i)
        {
            obj = Instantiate(Man, new Vector3(3, 1, -8 - i), Quaternion.identity);
            obj.tag = "OnFloorMan";
            anim = obj.GetComponent<Animator>();
            anim.Play("idle");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        inGamePanel.SetActive(true);
        finishPanel.SetActive(false);
    }

    public void FinishGame()
    {
        if (true)
        {
            startPanel.SetActive(false);
            inGamePanel.SetActive(false);
            finishPanel.SetActive(true);
        }

    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
