using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CountryHover : MonoBehaviour
{
    public int currentValue = 0;
    public string currentYear = "__";
    public string dataType = "__";

    public bool isSelected = false;

    private Material originalMaterial;
    private Material selectedMaterial;

    public delegate void OnCountrySelect(GameObject country);
    public static event OnCountrySelect onCountrySelect;

    public delegate void OnCountryDeSelect(GameObject country);
    public static event OnCountryDeSelect onCountryDeSelect;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        if (!gameObject.GetComponent<MeshCollider>())
            gameObject.AddComponent<MeshCollider>();

        originalMaterial = GetComponent<MeshRenderer>().material;
        selectedMaterial = Resources.Load<Material>("selectedMaterial") as Material;
    }

    private int myVal = -1;

    private void OnMouseDown()
    {
        if (GlobeMapCreator.instance.GetCurrentCountryCount() == 4 && !isSelected) return;

        isSelected = !isSelected;

        if (isSelected)
        {
            onCountrySelect?.Invoke(gameObject);
            GetComponent<MeshRenderer>().material = selectedMaterial;
        }
        else
        {
            onCountryDeSelect?.Invoke(gameObject);
            GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }

    public float literacyValue, wageValue, energyValue;

    void OnMouseOver()
    {
        if (!UIHelper.instance.IsPointerOverUIElement())
        {
            if (gameObject.GetComponent<GenerateRandomPoint>())
            {
                //currentValue = gameObject.GetComponent<GenerateRandomPoint>().currentValue;
                literacyValue = gameObject.GetComponent<GenerateRandomPoint>().literacyValue;
                wageValue = gameObject.GetComponent<GenerateRandomPoint>().wageValue;
                energyValue = gameObject.GetComponent<GenerateRandomPoint>().energyValue;


                currentYear = gameObject.GetComponent<GenerateRandomPoint>().currentYear;
                currentYear = currentYear.Replace("_", "");
                //dataType = gameObject.GetComponent<GenerateRandomPoint>().dataType;
            }
            //if (currentValue <= 0)
            //{
            //    dataType += "[no data]";
            //}
            System.Func<string> getTooltipTextFunc = () =>
            {
                //return "<color=#00ff00>" + gameObject.name +
                //"</color>\nYear\n" + "<color=#F00>" + currentYear + "</color>" +
                //"</color>\nType\n" + "<color=#F00>" + dataType + "</color>" +
                //"</color>\nValue\n" + "<color=#F00>" + ToKMB((decimal)currentValue) + "</color>";

                //return "<color=#00ff00>" + gameObject.name + "\n" +
                //        "<color=#F00>" + currentYear + "" + "</color>" + "\n" +
                //         "<color=#F00>" + dataType + ": " + "</color>" +
                //        "<color=#F00>" + ToKMB((decimal)currentValue) + "</color>";

                string s1 = literacyValue <= 0 ? "[no data]" : ToKMB((decimal)literacyValue);
                string s2 = wageValue <= 0 ? "[no data]" : ToKMB((decimal)wageValue);
                string s3 = energyValue <= 0 ? "[no data]" : ToKMB((decimal)energyValue);

                return "<color=#00ff00>" + gameObject.name + "\n" +
                        "<color=#F00>" + currentYear + "" + "</color>" + "\n" +
                         "<color=#F00>" + "Literacy" + ": " + "</color>" +
                        "<color=#F00>" + s1 + "</color>" + "\n" +
                        "<color=#F00>" + "Wage" + ": " + "</color>" +
                        "<color=#F00>" + s2 + "</color>" + "\n" +
                        "<color=#F00>" + "Energy" + ": " + "</color>" +
                        "<color=#F00>" + s3 + "</color>";

            };
            TooltipScreenSpaceUI.ShowTooltip_Static(getTooltipTextFunc);
        }

    }

    void OnMouseExit()
    {
        TooltipScreenSpaceUI.HideTooltip_Static();
    }

    public string ToKMB(decimal num)
    {
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999999 || num < -999999)
        {
            return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999 || num < -999)
        {
            return num.ToString("0,.#K", CultureInfo.InvariantCulture);
        }
        else
        {
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}
