using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : Singleton<Trajectory>
{
    public BezierCurve.Curve curve = new BezierCurve.Curve();
    private BezierCurveVisualizer visualizer;

    private void Start()
    {
        visualizer = GetComponent<BezierCurveVisualizer>();
    }

    public void Calculate(Vector3 startPosition, Vector3 endPosition)
    {
        curve.start = startPosition;
        curve.end = endPosition + new Vector3(0, 0, -1f);
        curve.middle = (startPosition + endPosition) * 0.5f;

        Visualize();
    }
    private void Visualize()
    {
        visualizer.Visualize(curve.start, curve.middle, curve.end);
    }
    public void Clear()
    {
        visualizer.Clear();
    }
}