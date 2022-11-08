using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CountryHover : MonoBehaviour
{
    public int currentValue = 0;
    public string currentYear = "__";
    public string dataType = "__";

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        if(!gameObject.GetComponent<MeshCollider>())
            gameObject.AddComponent<MeshCollider>();
    }
    void OnMouseOver()
    {
        if (!UIHelper.instance.IsPointerOverUIElement())
        {
            if (gameObject.GetComponent<GenerateRandomPoint>())
            {
                currentValue = gameObject.GetComponent<GenerateRandomPoint>().currentValue;
                currentYear = gameObject.GetComponent<GenerateRandomPoint>().currentYear;
                currentYear = currentYear.Replace("_", "");
                dataType = gameObject.GetComponent<GenerateRandomPoint>().dataType;
            }
            if (currentValue <= 0)
            {
                dataType += "[no data]";
            }
            System.Func<string> getTooltipTextFunc = () =>
            {
                //return "<color=#00ff00>" + gameObject.name +
                //"</color>\nYear\n" + "<color=#F00>" + currentYear + "</color>" +
                //"</color>\nType\n" + "<color=#F00>" + dataType + "</color>" +
                //"</color>\nValue\n" + "<color=#F00>" + ToKMB((decimal)currentValue) + "</color>";

                return "<color=#00ff00>" + gameObject.name + "\n" +
                        "<color=#F00>" + currentYear + "" + "</color>"  + "\n" +
                         "<color=#F00>" + dataType + ": " + "</color>" +
                        "<color=#F00>" + ToKMB((decimal)currentValue) + "</color>";
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
