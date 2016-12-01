using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//custom reversed comparer used for sorted dictionary
class ReverseComparator<T> : IComparer<T> {
    private IComparer<T> baseComparer;
    public ReverseComparator(IComparer<T> baseComparer_) {
        this.baseComparer = baseComparer_;
    }
    public int Compare(T left, T right) {
        return baseComparer.Compare(right, left);
    }
}

public class EndGameManager : MonoBehaviour {
    
    public List<GameObject> activeBars;
    int numActivePlayersInGame = 0;
    int maxHeigthBar = 9;

    void Awake() {
        //setup scene according to game specs
        for (int i = 0; i < 4; ++i) //4 is max number of players
            if (GlobalPlayerManager.inst.IsInGame(i)) ++numActivePlayersInGame;

        for(int i = 0; i < numActivePlayersInGame; ++i) {
            activeBars[i].transform.FindChild("ScoreBar").GetComponent<Renderer>().material.color = GlobalPlayerManager.inst.materials[i].col;
            foreach (Renderer r in activeBars[i].transform.FindChild("Player").GetComponentsInChildren<Renderer>()) {
                r.material = GlobalPlayerManager.inst.materials[i].mat;
            }
        }

        List<int> heightsOfBars = new List<int>();
        //move bars around given number of players
        if (numActivePlayersInGame == 2) InitTwoBars();
        else if (numActivePlayersInGame == 3) InitThreeBars();
        //else leave bars as is  
        List<int> heights = GenerateHeightsBasedOnScore();
        for(int i = 0; i < numActivePlayersInGame; ++i) {
            StartCoroutine(activeBars[i].GetComponent<GrowingScoreBarManager>().GrowToPos(heights[i]));
        }
    }

    void InitTwoBars() {
        activeBars[2].SetActive(false);
        activeBars[3].SetActive(false);

        activeBars[0].transform.position = new Vector3(-3, 2, -2);
        activeBars[1].transform.position = new Vector3(3, 2, -2);
    }

    void InitThreeBars() {
        activeBars[3].SetActive(false);
        activeBars[0].transform.position = new Vector3(-4, 2, -2);
        activeBars[1].transform.position = new Vector3(0, 2, -2);
        activeBars[2].transform.position = new Vector3(4, 2, -2);
    }

    List<int> GenerateHeightsBasedOnScore() {
        List<int> scores = GlobalPlayerManager.inst.scores;
        SortedDictionary<int, List<int>> scoresToPlayer = 
                new SortedDictionary<int, List<int>>(new ReverseComparator<int>(Comparer<int>.Default));
        for (int i = 0; i < numActivePlayersInGame; ++i) {
            if (scoresToPlayer.ContainsKey(scores[i])) continue;
            scoresToPlayer.Add(scores[i], new List<int>()); //init dictionary
        }
        for (int i = 0; i < numActivePlayersInGame; ++i) {
            List<int> entries;
            scoresToPlayer.TryGetValue(scores[i], out entries);
            entries.Add(i);
            scoresToPlayer.Remove(scores[i]);
            scoresToPlayer.Add(scores[i], entries);
        }

        List<int> heights = new List<int>();
        for (int i = 0; i < numActivePlayersInGame; ++i) heights.Add(0);//init list
        int heightBar = maxHeigthBar;
        foreach(KeyValuePair<int, List<int>> entry in scoresToPlayer) {
            List<int> playersWithGivenScore = entry.Value;
            foreach (int i in playersWithGivenScore) heights[i] = heightBar;
            heightBar -= 3;
        }
        return heights;
    }

    /*
    IEnumerator ShowEndScreen() {

    }*/
}
