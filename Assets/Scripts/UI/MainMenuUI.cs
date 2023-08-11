using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour {

    Button playButton;
    VisualElement root;

    void Start() {
        UIDocument UIDocument = GetComponent<UIDocument>();

        var rootEl = UIDocument.rootVisualElement;
        root = rootEl.Q<VisualElement>("Root");
        playButton = rootEl.Q<Button>("PlayButton");
        playButton.clicked += OnClickPlayButton;
    }

    void OnClickPlayButton() {
        GameplayController.Instance.StartGame();
    }
}