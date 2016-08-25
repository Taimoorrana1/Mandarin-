﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] players;
	public Transform[] locations;
	int currentPlayer = 0;
	bool gameOver = false;
	const int moneyLandValue = 100;
	bool passedAPlayer = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void StartCurrentPlayerTurn(){
		if (passedAPlayer) {
			currentPlayer--;
			movePlayer ();
			currentPlayer++;
			passedAPlayer = false;
			return;
		}
		// go back to player 1 if out of bounds
		if (currentPlayer >= players.Length) {
			currentPlayer = 0;
		}
		int playerPreviousPosition = players [currentPlayer].GetComponent<Player>().positionNumber;

		int playerNewPosition = movePlayer ();


		foreach (GameObject player in players) {
			if (player.GetComponent<Player>() != players [currentPlayer].GetComponent<Player>()) {
				if(playerPreviousPosition < player.GetComponent<Player>().positionNumber && playerNewPosition > player.GetComponent<Player>().positionNumber){
					Debug.Log("passed a player");
					passedAPlayer = true;
				}
			}
			
		}
		if (!passedAPlayer) {
			// if the player lands on money position, then collect money, else collect a chip
			if (playerNewPosition % 3 == 1) {
				players [currentPlayer].GetComponent<Player> ().CollectMoney (moneyLandValue);
			} else {
				players [currentPlayer].GetComponent<Player> ().TakeChip ();
			}
		} else {
			Debug.Log("Collecting chip from PLAYER");
		}

		// go to next player
		currentPlayer++;
	}

	private int Roll(){
		int value = Random.Range (1, 7);
		Debug.Log ("Rolled: "+value);
		return value;
	}

	public int movePlayer(){
		// calculate the new position
		int playerNewPosition = Roll () + players [currentPlayer].GetComponent<Player>().positionNumber;

		// don't go over the location quantity
		if (playerNewPosition >= locations.Length) {
			playerNewPosition -= locations.Length;
		}

		// set the position in the player object
		players [currentPlayer].GetComponent<Player> ().positionNumber = playerNewPosition;

		// move the player
		players[currentPlayer].GetComponent<Player>().Move(locations[playerNewPosition]);

		return playerNewPosition;
	}
}
