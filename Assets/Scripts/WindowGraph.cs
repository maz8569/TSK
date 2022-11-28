using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;

    private int NUMBER_OF_LABEL = 6;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

        labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();

        dashTemplateX = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();

        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(5, 1));
        points.Add(new Vector2(10, 2));
        points.Add(new Vector2(15, 4));
        points.Add(new Vector2(20, 8));
        points.Add(new Vector2(25, 16));
        points.Add(new Vector2(30, 32));
        points.Add(new Vector2(35, 64));
        points.Add(new Vector2(40, 128));
        points.Add(new Vector2(45, 200));
        points.Add(new Vector2(50, 400));
        points.Add(new Vector2(55, 450));

        DisplayData(points);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject go = new GameObject("circle", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().sprite = circleSprite;
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchoredPosition = anchoredPosition;
        rt.sizeDelta = new Vector2(10,10);
        rt.anchorMin = new Vector2(0,0);
        rt.anchorMax = new Vector2(0,0);
        return go;
    }

    private void CreateCircleConnections(Vector2 pointA, Vector2 pointB)
    {
        GameObject go = new GameObject("line", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rt = go.GetComponent<RectTransform>();
        Vector2 dir = (pointB - pointA).normalized;
        float dis = Vector2.Distance(pointA, pointB);
        rt.sizeDelta = new Vector2(dis,3f);
        rt.anchorMin = new Vector2(0,0);
        rt.anchorMax = new Vector2(0,0);
        rt.anchoredPosition = pointA + dir * dis * 0.5f;

        dir = pointB - pointA;
        double angle = Math.Atan2(dir.x, dir.y);
        angle = angle * ( 180 / Math.PI );
        angle -= 90;

        rt.localEulerAngles = new Vector3(0, 0, (float)angle);
        rt.localScale = new Vector3(1, -1, 1);
    }

    private void DisplayData(List<Vector2> points)
    {
        float GraphWidht = graphContainer.sizeDelta.x;
        float GraphHeight = graphContainer.sizeDelta.y;

        float MaxValueX = 0;
        float MaxValueY = 0;        
        
        for(int i = 0; i < points.Count; i++)
        {
            if(MaxValueX < points[i].x)
                MaxValueX = points[i].x;
            if(MaxValueY < points[i].y)
                MaxValueY = points[i].y;
        }

        float x, y;
        float px, py;

        int next = points.Count / NUMBER_OF_LABEL;
        int a = 0;
        for(int i = 0; i < NUMBER_OF_LABEL; i++)
        {
            x = points[a].x;
            y = points[a].y;
            px = points[a].x / MaxValueX * GraphWidht;
            py = points[a].y / MaxValueY * GraphHeight;
            a += next;

            float normVal = i * 1f / NUMBER_OF_LABEL;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(px + 0, -10);
            labelX.GetComponent<Text>().text = Math.Round(x, 0).ToString();

            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(px + 0, 0);

            ///...

            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            labelY.anchoredPosition = new Vector2(-30, py + 0);
            labelY.GetComponent<Text>().text = Math.Round(y, 0).ToString();

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(0, py + 0);
        }

        GameObject last_cgo = null;
        for(int i = 0; i < points.Count; i++)
        {
            x = points[i].x / MaxValueX * GraphWidht;
            y = points[i].y / MaxValueY * GraphHeight;

            GameObject cgo = CreateCircle(new Vector2(x, y));
            if(last_cgo != null)
            {
                CreateCircleConnections(
                    last_cgo.GetComponent<RectTransform>().anchoredPosition, 
                    cgo.GetComponent<RectTransform>().anchoredPosition);
            }
            last_cgo = cgo;
        }
    }
}
