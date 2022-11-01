using Dynamitey;
using Newtonsoft.Json;
using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class UpdateDataInChart : MonoBehaviour
{
    private LineChart lineChart;
    string[] a = new string[] { "_1960", "_1961", "_1962", "_1963", "_1964", "_1965", "_1966", "_1967", "_1968", "_1969", "_1970", "_1971", "_1972", "_1973", "_1974", "_1975", "_1976", "_1977", "_1978", "_1979", "_1980", "_1981", "_1982", "_1983", "_1984", "_1985", "_1986", "_1987", "_1988", "_1989", "_1990", "_1991", "_1992", "_1993", "_1994", "_1995", "_1996", "_1997", "_1998", "_1999", "_2000", "_2001", "_2002", "_2003", "_2004", "_2005", "_2006", "_2007", "_2008", "_2009", "_2010", "_2011", "_2012", "_2013", "_2014", "_2015", "_2016", "_2017", "_2018", "_2019", "_2020", "_2021" };
    List<string> yearList;

    List<LoadedCountryData> loadedCountryDatas;

    void Start()
    {

    }

    int startValue = 0;
    int endValue = 0;
    public void ShowNextFiveCOuntriesData()
    {
        yearList = a.ToList();

        lineChart = GetComponent<LineChart>();
        lineChart.GetChartComponent<Title>().text = "EmptyChart";

        lineChart.ClearData();

        lineChart.GetChartComponent<Title>().text = fileName;

        AxisName yearName = new AxisName();
        yearName.name = "years";

        AxisName yAxisName = new AxisName();
        yAxisName.name = "values";

        lineChart.GetChartComponent<XAxis>().axisName = yearName;
        lineChart.GetChartComponent<XAxis>().data = yearList;

        lineChart.GetChartComponent<YAxis>().axisName = yAxisName;


        startValue = endValue;
        endValue = endValue + 5;
        if (endValue > loadedCountryDatas.Count)
        {
            endValue = loadedCountryDatas.Count;
        }

        {
            foreach (LoadedCountryData country in loadedCountryDatas.GetRange(startValue, 10))
            {
                string countryName = country.countryName;
                lineChart.AddSerie<Line>(countryName);

                foreach (KeyValuePair<string, float> kvp in country.values)
                {
                    int yy = 0;
                    int.TryParse(kvp.Key, out yy);
                    if (kvp.Value > 0)
                        lineChart.GetSerie(countryName).AddXYData(yy, kvp.Value);
                }
            }
        }

    }

    public void ShowPreviousFiveCOuntriesData()
    {
        startValue = endValue;
        endValue = endValue - 5;
        if (endValue < 0)
        {
            endValue = 0;
        }
        lineChart.ClearData();
        {
            foreach (LoadedCountryData country in loadedCountryDatas.GetRange(startValue, 5))
            {
                string countryName = country.countryName;
                lineChart.AddSerie<Line>(countryName);

                foreach (KeyValuePair<string, float> kvp in country.values)
                {
                    int yy = 0;
                    int.TryParse(kvp.Key, out yy);
                    if (kvp.Value > 0)
                        lineChart.GetSerie(countryName).AddXYData(yy, kvp.Value);
                }
            }
        }

    }

    internal void ClearData()
    {
        loadedCountryDatas = new List<LoadedCountryData>();
    }
    string fileName;
    public void ShowDataFromClass(List<LoadedCountryData> _loadedCountryDatas, string path)
    {
        loadedCountryDatas = _loadedCountryDatas;

        yearList = a.ToList();

        lineChart = GetComponent<LineChart>();
        lineChart.GetChartComponent<Title>().text = "EmptyChart";

        lineChart.ClearData();

        lineChart.GetChartComponent<Title>().text = System.IO.Path.GetFileNameWithoutExtension(path);
        fileName = lineChart.GetChartComponent<Title>().text;
        AxisName yearName = new AxisName();
        yearName.name = "years";

        AxisName yAxisName = new AxisName();
        yAxisName.name = "values";

        lineChart.GetChartComponent<XAxis>().axisName = yearName;
        lineChart.GetChartComponent<XAxis>().data = yearList;

        lineChart.GetChartComponent<YAxis>().axisName = yAxisName;

        ShowNextFiveCOuntriesData();
        //foreach (LoadedCountryData country in _loadedCountryDatas)
        //{
        //    string countryName = country.countryName;
        //    lineChart.AddSerie<Line>(countryName);

        //    foreach (KeyValuePair<string, float> kvp in country.values)
        //    {
        //        int yy = 0;
        //        int.TryParse(kvp.Key, out yy);
        //        if (kvp.Value > 0)
        //            lineChart.GetSerie(countryName).AddXYData(yy, kvp.Value);
        //    }
        //}
    }

    public void ShowDataFromString(string json, string path)
    {

        yearList = a.ToList();

        lineChart = GetComponent<LineChart>();
        lineChart.GetChartComponent<Title>().text = "EmptyChart";

        lineChart.ClearData();

        lineChart.GetChartComponent<Title>().text = System.IO.Path.GetFileNameWithoutExtension(path);

        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);


        AxisName yearName = new AxisName();
        yearName.name = "years";

        AxisName yAxisName = new AxisName();
        yAxisName.name = "values";

        lineChart.GetChartComponent<XAxis>().axisName = yearName;
        lineChart.GetChartComponent<XAxis>().data = yearList;

        lineChart.GetChartComponent<YAxis>().axisName = yAxisName;


        List<int> listNumbers = new List<int>();
        int number;
        System.Random rand = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            do
            {
                number = rand.Next(0, myDeserializedClass.Data.Count);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }

        int countryCount = 20;
        if (!path.Contains("population"))
        {
            //countryCount = myDeserializedClass.Data.Count;
        }

        for (int i = 0; i < listNumbers.Count; i++)
        {
            //if(myDeserializedClass.Data[i].CountryName == "Germany" || myDeserializedClass.Data[i].CountryName == "France"
            //    || myDeserializedClass.Data[i].CountryName == "Italy" || myDeserializedClass.Data[i].CountryName == "India"
            //    || myDeserializedClass.Data[i].CountryName == "China")
            {
                lineChart.AddSerie<Line>(myDeserializedClass.Data[listNumbers[i]].CountryName);

                foreach (string year in yearList)
                {
                    float value = 0f;
                    if (Dynamic.InvokeGet(myDeserializedClass.Data[listNumbers[i]], year) != null)
                    {
                        float.TryParse(Dynamic.InvokeGet(myDeserializedClass.Data[listNumbers[i]], year), out value);
                    }
                    int yy = 0;
                    int.TryParse(year, out yy);
                    if (value > 0)
                        lineChart.GetSerie(myDeserializedClass.Data[listNumbers[i]].CountryName).AddXYData(yy, value);
                }
            }
            
        }
    }

    public void OpenFile()
    {
        var extensions = new[] {
        new ExtensionFilter("Database", "txt"),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

        lineChart.GetChartComponent<Title>().text = System.IO.Path.GetFileNameWithoutExtension(paths[0]);

        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(File.ReadAllText(new System.Uri(paths[0]).AbsolutePath));

        lineChart.ClearData();

        AxisName yearName = new AxisName();
        yearName.name = "years";

        AxisName yAxisName = new AxisName();
        yAxisName.name = "values";

        lineChart.GetChartComponent<XAxis>().axisName = yearName;
        lineChart.GetChartComponent<XAxis>().data = yearList;

        lineChart.GetChartComponent<YAxis>().axisName = yAxisName;

        

        for (int i = 0; i < myDeserializedClass.Data.Count; i++)
        {
            lineChart.AddSerie<Line>(myDeserializedClass.Data[i].CountryName);
            
            foreach (string year in yearList)
            {
                float value = 0f;
                if (Dynamic.InvokeGet(myDeserializedClass.Data[i], year) != null)
                {
                    float.TryParse(Dynamic.InvokeGet(myDeserializedClass.Data[i], year), out value);
                }
                int yy = 0;
                int.TryParse(year, out yy);
                if(value>0)
                    lineChart.GetSerie(myDeserializedClass.Data[i].CountryName).AddXYData(yy, value);
            }
        }

    }
}
