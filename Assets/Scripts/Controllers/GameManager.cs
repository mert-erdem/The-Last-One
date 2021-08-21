using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //düþman spawnlama vb. temel fonksiyonlar
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] graves;//spawn points

    public static UnityAction actGameOver;

    private int giantBeetleLuck = 900;
    [SerializeField] [Range(0f, 5f)] private float spawnInterval = 0;
    private float currentSpawnInterval = 0;
    private bool spawn = true;

    private bool gameOver = false;

    private static int gold;

    private float surviveTime;

    private void Awake() => actGameOver += GameOver;

    private void Start()
    {
        gold = 100;
        CanvasController.ChangeGoldText(gold);
        currentSpawnInterval = spawnInterval;
    }

    private void Update()
    {
        if(!gameOver) surviveTime = Time.timeSinceLevelLoad;

        if(Time.timeSinceLevelLoad >= currentSpawnInterval && !gameOver)
        {
            currentSpawnInterval += spawnInterval;

            SpawnEnemy();

            if(spawnInterval > 0.8f) spawnInterval -= 0.009f;
        }
    }

    private void SpawnEnemy()
    {
        var grave = graves[Random.Range(0, 6)];
        int value = Random.Range(0, 1000);

        if (value >= giantBeetleLuck)
            Instantiate(enemies[1], grave.position, Quaternion.identity);
        else
            Instantiate(enemies[0], grave.position, Quaternion.identity);

        //depends on time, spawning giant beetle probability will increase
        giantBeetleLuck-=5;
    }

    public static void AddGold(int amount)
    {
        gold += amount;
        CanvasController.ChangeGoldText(gold);
        //PlayerPrefs.SetInt("GOLD", gold);
    }

    public static void DecreaseGold(int amount)
    {
        gold -= amount;
        CanvasController.ChangeGoldText(gold);
    }

    public static int GetGold() => gold;

    private void GameOver()
    {
        gameOver = true;

        if(surviveTime > PlayerPrefs.GetFloat("TIME", 0))
            PlayerPrefs.SetFloat("TIME", surviveTime);

        PlayerPrefs.SetFloat("SURVIVETIME", surviveTime);

        StartCoroutine(LoadScene());
        //end scene        
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("EndScene");
    }
}
