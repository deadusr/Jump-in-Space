using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelFinishedUI : MonoBehaviour {

    Button nextLevelButton;
    TextElement levelInfo;
    VisualElement root;

    void Start() {
        UIDocument UIDocument = GetComponent<UIDocument>();
        
        var rootEl = UIDocument.rootVisualElement;

        nextLevelButton = rootEl.Q<Button>("NextLevelButton");
        levelInfo = rootEl.Q<TextElement>("LevelInfo");
        root = rootEl.Q<VisualElement>("Root");
        
        nextLevelButton.clicked += OnClickNextLevelButton;
    }

    void OnClickNextLevelButton() {
        GameplayController.Instance.NextLevel();
    }

    public void ShowPanel() {
        root.style.display = DisplayStyle.Flex;

    }

    public void HidePanel() {
        root.style.display = DisplayStyle.None;
    }
}