using System;
using UnityEngine;
using UnityEngine.UI;

public class WhatDoISee : MonoBehaviour {
	public GameObject visibleObjects;
	public Text text;
	public float maxAngle = 60;
	public float maxDistance = -1;
	public float[] times;

	// Use this for initialization
	void Start () {

		foreach (Transform child in visibleObjects.transform) {
			Array.Resize (ref times, times.Length+1);
			times [times.Length - 1] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "";
		int indexTime = 0;
		foreach (Transform child in visibleObjects.transform) {
			Vector3 viewVector = child.position - this.transform.position;
			//viewVector = this.transform.position;
			float angle = Vector3.Angle(viewVector, this.transform.forward);
			bool visible; 
			visible = (angle < maxAngle);
			if (this.maxDistance > 0.0 && visible)
				visible = (viewVector.sqrMagnitude < maxDistance);

			if (visible) {
				times [indexTime] += Time.deltaTime;
			}

			//text.text = text.text + child.name + " " + (visible ? "Y" : "N") + " " + angle + " " + viewVector.sqrMagnitude + " " + times[indexTime] + " " + viewVector.x  + " " + viewVector.y  + " " + viewVector.z + System.Environment.NewLine;
			text.text = text.text + child.name + " " + (int)times[indexTime] + " " + " " + (visible ? "VISIBLE" : "") + System.Environment.NewLine;// + "    " + (int) viewVector.sqrMagnitude + " " + viewVector.x  + " " + viewVector.y  + " " + viewVector.z + System.Environment.NewLine;
			indexTime += 1;
		}
	}
}
