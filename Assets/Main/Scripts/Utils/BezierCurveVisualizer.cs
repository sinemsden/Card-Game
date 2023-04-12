using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class BezierCurveVisualizer : MonoBehaviour
{
	public Color startColor = Color.white;
    public Color endColor = Color.white;
	public float startWidth = 0.2f;
    public float endWidth = 0.5f;
	public int numberOfPoints = 20;
	LineRenderer lineRenderer;

	void Start () 
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;

        lineRenderer.startColor = startColor;
		lineRenderer.endColor = endColor;
   		lineRenderer.startWidth = startWidth;
		lineRenderer.endWidth = endWidth;

        if (numberOfPoints > 0)
   		{
    		lineRenderer.positionCount = numberOfPoints;
		}
	}
    public void Visualize(Vector3 start, Vector3 middle,Vector3 end)
    {
        if(lineRenderer == null)
		{
			return; // no points specified
		}

        Vector3 position;
		for(int i = 0; i < numberOfPoints; i++)
		{
			float t = i / (numberOfPoints - 1.0f);
            position = BezierCurve.GetPositionFromCurve(start, middle, end, t);            
			lineRenderer.SetPosition(i, position);
		}
    }
	public void Clear()
	{
		//Set all points to zero
		for(int i = 0; i < numberOfPoints; i++)
		{
			lineRenderer.SetPosition(i, Vector3.zero);
		}
	}
}
