﻿using UnityEngine;
using System.Collections;

public class RideTheJeep : MonoBehaviour {
	public	RidingScript rs;

	public void OnEnable(){
		Board();
	}
	
	private void Board(){
		rs.AddPassenger(gameObject);
	}
}
