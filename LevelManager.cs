using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Int & String")]
    [SerializeField]public int _loadInteger;
    [SerializeField] public int _levelInteger;
    [SerializeField] public int _levelPrice;
    [SerializeField] public string _levelStringPrice;

    [Header("Object's")]
    [SerializeField] private GameObject youWin_GameObject;
    private string dataName = "ftm_DATA.dat";
    public static LevelManager i; // ftm_DATA.dat

    public bool deneme;
    private void Awake()
    {

        i = this;
    }
    private void Update()
    {
        if (deneme)
        {
            SaveManager.i.saveBools[_levelInteger] = true;
            deneme = false;
        }
    }

    public void nextLevelButton()
    {
        levelEditor();
    }
    private void levelEditor()
    {
        int currentCoin = GameManager.instance.coin;

        if (File.Exists(Application.persistentDataPath + dataName))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + dataName, FileMode.Open);
            //diff_Storage data = (diff_Storage)bf.Deserialize(file);
            diff_Storage dataNew = new diff_Storage();

            currentCoin = currentCoin - _levelPrice;
            PlayerPrefs.SetInt("coin", currentCoin);

            SaveManager.i.saveBools[_levelInteger] = true;
            dataNew.diffBools = SaveManager.i.saveBools;


            bf.Serialize(file, dataNew);
            file.Close();

            loadNextScene();
        }

    }

    private void loadNextScene()
    {

        Time.timeScale = 1;
        
        SceneManager.LoadScene(_loadInteger);
    }


    public void finishGamePanel()
    {
        Sounds.instance.playOneSound(4);
        youWin_GameObject.SetActive(true);
    }
}

