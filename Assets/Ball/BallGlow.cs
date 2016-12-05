using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallGlow : MonoBehaviour {
	public GameObject glowChild;
	private SpriteRenderer glowSprite;
	List<GameObject> players = new List<GameObject>();

	// Use this for initialization
	void Start () {
		glowChild.SetActive(true);
		glowSprite = glowChild.GetComponent<SpriteRenderer>();
	}

	void Update() {
		GameObject closest = FindClosestPlayer();
		Color GlowColor = (closest == null) ? new Color(1,1,1,.5f) : PlayerManager.inst.GetColor(closest.GetComponent<PlayerData>().num);
		glowSprite.color = GlowColor; 
	}

	public void Grabbed()
	{
		players.Clear();
		glowChild.SetActive(false);
	}

	public void Throwed()
	{
		glowChild.SetActive(true);
	}

	public void AddInRange(GameObject player)
	{
		if (!players.Contains(player))
		{
			players.Add(player);
			//glowChild.SetActive(true);
		}
	}

	public void RemoveInRange(GameObject player)
	{
		if (players.Contains(player))
		{
			players.Remove(player);
			if (players.Count == 0){}
				//glowChild.SetActive(false);
		}
	}

	GameObject FindClosestPlayer() {
		GameObject closestPlayer = null;
		float shortestDistance = float.MaxValue;
		foreach (GameObject player in players) {
			float thisDistance = DistanceToPlayer(player);
			if (closestPlayer == null || thisDistance < shortestDistance) {
				closestPlayer = player;
				shortestDistance = thisDistance;
			}
		}
		return closestPlayer;
	}

	float DistanceToPlayer(GameObject player) {
		return Vector3.Distance(transform.position, player.transform.position);
	}
}
