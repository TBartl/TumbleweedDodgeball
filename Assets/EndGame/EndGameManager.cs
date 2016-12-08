﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using InControl;

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
    public GameObject restartText;
    public GameObject controllerPrefab;

    private bool checkForRestartInput = false;
    Controller[] controllers;
    int numActivePlayersInGame = 0;
    int maxHeigthBar = 9;

    void Awake() {
        //get controllers 
        controllers = new Controller[InputManager.Devices.Count];
        for (int i = 0; i < InputManager.Devices.Count; ++i) {
            controllers[i] = Instantiate(controllerPrefab).GetComponent<Controller>();
            controllers[i].inputDeviceNum = i;
        }
        Controller.canMove = true;

        //setup scene according to game specs
        for (int i = 0; i < 4; ++i) //4 is max number of players
            if (GlobalPlayerManager.inst.IsInGame(i)) ++numActivePlayersInGame;

        for (int i = 0; i < numActivePlayersInGame; ++i) {
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
        //grow the bars based on the sorted scores
        for (int i = 0; i < numActivePlayersInGame; ++i) {
            StartCoroutine(activeBars[i].GetComponent<GrowingScoreBarManager>().GrowToPos(heights[i]));
        }

        //display restart text
        StartCoroutine(ShowRestartInput());
    }

    void Update() {
        if (checkForRestartInput) {
            if (GetPlayerInput()) {//someone pressed B to restart
				for (int i = 0; i < numActivePlayersInGame; ++i) {
					StartMenu.AddToGame(i);
				}
				AudioManager.instance.PlayClip(AudioManager.instance.confirm);
				SceneTransitioner.instance.LoadScene("MainMenu");
            }
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
        int maxScore = 0;
        List<int> scores = GlobalPlayerManager.inst.scores;
        SortedDictionary<int, List<int>> scoresToPlayer =
                new SortedDictionary<int, List<int>>(new ReverseComparator<int>(Comparer<int>.Default));
        for (int i = 0; i < numActivePlayersInGame; ++i) {
            if (scores[i] > maxScore) maxScore = scores[i];//get highest score in the game
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
        foreach (KeyValuePair<int, List<int>> entry in scoresToPlayer) {
            List<int> playersWithGivenScore = entry.Value;
            foreach (int i in playersWithGivenScore) {
                if (entry.Key == maxScore) heights[i] = maxHeigthBar;
                else {
                    heights[i] = (int)Mathf.Max((float)maxHeigthBar * ((float)entry.Key / (float)maxScore), 1f);//relative height
                }
            }
        }
        return heights;
    }


    IEnumerator ShowRestartInput() {
        yield return new WaitForSeconds(3); //wait for 5 seconds
        restartText.SetActive(true);
        checkForRestartInput = true;
    }

    bool GetPlayerInput() {
        foreach (Controller cont in controllers) {
            if (cont.GetRestartPressed()) return true;
        }
        return false;
    }
}
