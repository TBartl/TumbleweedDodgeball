using UnityEngine;
using System.Collections;

public class AddBallUI : MonoBehaviour {

	public GameObject[] balls;
	public GameObject[] UIpos;

	private static GameObject[] Statballs;
	private static GameObject[] StatUIpos;
	private static GameObject[] StatUIBalls = new GameObject[8];

	void Start () {
		Statballs = balls;
		StatUIpos = UIpos;
	}

	public static void BalltoUI (int playerID, int hand) {
		GameObject temp = (GameObject)Instantiate(Statballs[0], StatUIpos[playerID*2+hand].transform.position, Quaternion.identity);
		temp.transform.SetParent(StatUIpos[playerID*2+hand].transform);
		StatUIBalls[playerID*2+hand] = temp;
	}

	public static void BallfromUI (int playerID, int hand) {
		Destroy(StatUIBalls[playerID*2+hand]);
	}
}
