﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitySteer;
using UnitySteer.Behaviors;

[RequireComponent(typeof(SteerForPathSimplified))]
public class CarPathController : MonoBehaviour 
{
	
	SteerForPathSimplified _steering;
	
	[SerializeField] Transform _pathRoot;
	
	[SerializeField] bool _followAsSpline;

	private float numPaths;
	
	// Use this for initialization
	void Start() 
	{
		_steering = GetComponent<SteerForPathSimplified>();
		numPaths = 0;
		AssignPath();
	}
	
	
	void AssignPath()
	{
		// Get a list of points to follow;
		var pathPoints = PathFromRoot(_pathRoot);
		numPaths = pathPoints.Count;
		_steering.Path = _followAsSpline ? new SplinePathway(pathPoints, 1) : new Vector3Pathway(pathPoints, 1);
	}
	
	static List<Vector3> PathFromRoot(Transform root)
	{
		var children = new List<Transform>();
		foreach (Transform t in root)
		{
			if (t != null)
			{
				children.Add(t);
			}
			Debug.Log (t.position.ToString());
		}
		return children.OrderBy(t => t.gameObject.name).Select(t => t.position).ToList();
	}
	
	public void FixedUpdate() {
		if(_steering.PathPercentTraversed >= 1 - 1 / numPaths / 2) {\
			var pathPoints = PathFromRoot(_pathRoot);
			_steering.Path = _followAsSpline ? new SplinePathway(pathPoints, 1) : new Vector3Pathway(pathPoints, 1);
		}
	}
}