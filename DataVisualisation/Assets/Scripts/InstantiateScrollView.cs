using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateScrollView : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    public static InstantiateScrollView instance = null;
    private void Awake()
    {
        instance = this;
    }

    public void Set(Color col, string year)
    {
        GameObject ins = GameObject.Instantiate(prefab);
        ins.transform.SetParent(parent);
        ins.SetActive(true);
        ins.GetComponent<Image>().color = Color.gray;
        ins.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = year;
    }

    public void DeleteAll()
    {
        if (parent.childCount > 0)
        {
            for(int i = 0; i < parent.childCount; i++)
            {
                DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }
    }
}
