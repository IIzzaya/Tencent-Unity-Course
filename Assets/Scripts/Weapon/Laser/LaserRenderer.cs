using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BezierCurve))]
[RequireComponent(typeof(AnimatedUVs))]
public class LaserRenderer : MonoBehaviour {

	[SerializeField]
	int nodeCount;

	LineRenderer lineRenderer;
	public BezierCurve bezier;
	private AnimatedUVs uvAnimator;

	public Transform GetEndPoint() {
		return bezier.Points[bezier.Points.Length - 1];
	}

	public void SetMidPoints() {

		float step = 1f / (bezier.Points.Length - 1f);
		var direction = bezier.DirectionVector;

		for (int i = 1; i < bezier.Points.Length - 1; i++) {
			bezier.Points[i].position = transform.position + i * step * direction;
		}
	}

	void SetVertexCount(int count) {
		this.lineRenderer.positionCount = count;
	}

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
		bezier = GetComponent<BezierCurve>();
		uvAnimator = GetComponent<AnimatedUVs>();
		SetVertexCount(nodeCount + 1);
	}

	void Update() {
		uvAnimator.speedMultiplier = 100 / bezier.DirectionVector.magnitude;
		for (int i = 0; i <= nodeCount; ++i) {
			Vector3 to = this.bezier.GetBezierPoint(i / (float) nodeCount);
			this.lineRenderer.SetPosition(i, to);
		}
	}
}