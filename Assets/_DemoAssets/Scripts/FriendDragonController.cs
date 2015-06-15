﻿using UnityEngine;
using System.Collections;

public class FriendDragonController : MonoBehaviour {
	
	public GameObject dragonModel = null;
	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector3 jumpForce = new Vector3(0, 300, 0);

	// FIXME change jumpData to private when testing done
	public string jumpData = "";
	
	private float playTimeTotal = 0.0f;
	private Rigidbody dragonRigidbody;
	private float[] jumpTimestamps;
	private int currentJumpTimestampIndex = 0;
	
	public string JumpData {
		get {
			return jumpData;
		}
		set {
			jumpData = value;
		}
	}
	
	void Awake() {
		dragonRigidbody = dragonModel.GetComponent<Rigidbody> ();

		string[] jumbTimestampSt = jumpData.Split('|');

		if (jumbTimestampSt.Length > 	1) {
			Debug.Log(jumbTimestampSt.Length);
			jumpTimestamps = new float[jumbTimestampSt.Length];
			
			for (int i = 0; i < jumpTimestamps.Length; i++) {
				float timestamp = float.Parse (jumbTimestampSt [i]);
				jumpTimestamps [i] = timestamp;
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		playTimeTotal = 0.0f;
		
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		playTimeTotal += Time.deltaTime;
		
		if (jumpTimestamps != null && currentJumpTimestampIndex < jumpTimestamps.Length) {
			if (playTimeTotal >= jumpTimestamps[currentJumpTimestampIndex]) {
				dragonRigidbody.velocity = Vector3.zero;
				dragonRigidbody.AddForce (jumpForce);
				
				currentJumpTimestampIndex++;
			}
		}
	}
	
	public void OnTapToPlay() {
		enabled = true;
		GetComponentInChildren<Rigidbody> ().useGravity = true;
	}
	
	/// <summary>
	/// Raises the dragon killed event.
	/// </summary>
	public void OnDragonKilled() {
		Reset ();
	}
	
	/// <summary>
	/// Reset this instance.
	/// </summary>
	public void Reset() {
		playTimeTotal = 0.0f;
		currentJumpTimestampIndex = 0;

		enabled = false;
		GetComponentInChildren<Rigidbody> ().useGravity = false;
	}
}

