using System.Collections.Generic;
using UnityEngine;

public class ArcManager : MonoBehaviour
{
    public static ArcManager Instance;

    public GameObject dotPrefab;
    public int poolSize = 50;
    private List<GameObject> dotPool = new List<GameObject>();

    public float spacing = 0.5f;
    public float arcHeight = 2f;
    public float maxSideOffset = 1f;

    private bool isArcVisible = false;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeDotPool(poolSize);
    }

    private void Update()
    {
        if (isArcVisible)
        {
            UpdateArc(startPoint, endPoint);
        }
    }

    public void ShowArc(bool value)
    {
        isArcVisible = value;
        if (!value)
        {
            HideArc();
        }
    }

    public bool IsArcVisible()
    {
        return isArcVisible;
    }

    public void SetStartPoint(Vector3 position)
    {
        startPoint = position;
    }

    public void UpdateArcPositions(Vector3 start, Vector3 end)
    {
        startPoint = start;
        endPoint = end;
    }

    private void UpdateArc(Vector3 startPos, Vector3 endPos)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(startPos, endPos) / spacing);

        for (int i = 0; i < numDots && i < dotPool.Count; i++)
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp01(t);

            Vector3 midPoint = CalculateMidPoint(startPos, endPos);
            Vector3 position = QuadraticBezierPoint(startPos, midPoint, endPos, t);

            dotPool[i].transform.position = position;
            dotPool[i].SetActive(true);
        }

        for (int i = numDots; i < dotPool.Count; i++)
        {
            dotPool[i].SetActive(false);
        }
    }

    private Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;
        return point;
    }

    private Vector3 CalculateMidPoint(Vector3 startPos, Vector3 endPos)
    {
        Vector3 midPoint = (startPos + endPos) / 2f;

        midPoint.y += arcHeight;

        Vector3 direction = endPos - startPos;

        float angle = Vector3.Angle(direction, Vector3.up);

        float normalizedAngle = Mathf.Clamp(angle, 0f, 90f) / 90f;

        float dynamicSideOffset = maxSideOffset * normalizedAngle;


        Vector3 sideDirection = Vector3.Cross(direction, Vector3.up).normalized;

        midPoint += sideDirection * dynamicSideOffset;

        return midPoint;
    }

    private void InitializeDotPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }

    public void HideArc()
    {
        foreach (GameObject dot in dotPool)
        {
            dot.SetActive(false);
        }
    }
}
