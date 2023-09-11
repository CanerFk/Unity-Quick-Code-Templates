using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text fruitText;
    public static GameManager Instance;
    public int fruit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void FruitIncrease()
    {
        AudioManager.Instance.Play("Collect");
        fruit++;
        fruitText.text = "Fruit: " + fruit.ToString();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneId = scene.buildIndex;
        AudioManager.Instance.Play("Music" + sceneId);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
