using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataForCarScene : MonoBehaviour
{
    public static DataForCarScene instance = null;
    public List<SelectedCountry> allDatas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GlobeMapCreator.onAllDataCollected += GlobeMapCreator_onAllDataCollected;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void GlobeMapCreator_onAllDataCollected(List<SelectedCountry> _allDatas)
    {
        allDatas = _allDatas;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TestData();
    }

    private void TestData()
    {
        if (allDatas.Count > 0)
        {
            foreach (SelectedCountry country in allDatas)
            {
                Debug.Log(country.countryName + "," +   country.enerygyValue + "," +
                                                        country.wageValue + "," +
                                                        country.literacyValue + ",");
            }
        }

    }
}
