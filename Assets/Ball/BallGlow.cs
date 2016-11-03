using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallGlow : MonoBehaviour {
	public GameObject glowChild;
	List<GameObject> players = new List<GameObject>();

	// Use this for initialization
	void Start () {
		glowChild.SetActive(false);
	}

	public void Grabbed()
	{
		players.Clear();
		glowChild.SetActive(false);
	}

	public void AddInRange(GameObject player)
	{
		if (!players.Contains(player))
		{
			players.Add(player);
			glowChild.SetActive(true);
		}
	}

	public void RemoveInRange(GameObject player)
	{
		if (players.Contains(player))
		{
			players.Remove(player);
			if (players.Count == 0)
				glowChild.SetActive(false);
		}
	}
}
