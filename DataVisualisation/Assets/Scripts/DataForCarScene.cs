using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DataForCarScene : MonoBehaviour
{
    public static DataForCarScene instance = null;
    public List<SelectedCountry> allDatas;

    public List<Texture2D> flags = new List<Texture2D>();
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
            int i = 0;
            foreach (SelectedCountry country in allDatas)
            {
                Debug.Log(country.countryName + "," +   country.enerygyValue + "," +
                                                        country.wageValue + "," +
                                                        country.literacyValue + ",");
            }
        }
        StartCoroutine(GetTexture());

    }

    IEnumerator GetTexture()
    {

        if (allDatas.Count > 0)
        {
            foreach (SelectedCountry country in allDatas)
            {
                Debug.Log(country.countryName + "," + country.enerygyValue + "," +
                                                        country.wageValue + "," +
                                                        country.literacyValue + ",");

                UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://countryflagsapi.com/png/" + country.countryCode);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    flags.Add(myTexture);
                }
            }
        }


    }


}
