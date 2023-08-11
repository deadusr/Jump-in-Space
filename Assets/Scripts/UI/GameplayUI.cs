using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour {

    Button replayButton;
    VisualElement root;
    TextElement loseReason;
    TextElement level;

    void Start() {
        UIDocument UIDocument = GetComponent<UIDocument>();

        var rootEl = UIDocument.rootVisualElement;

        replayButton = rootEl.Q<Button>("ReplayButton");
        root = rootEl.Q<VisualElement>("Root");
        loseReason = rootEl.Q<TextElement>("LoseReason");
        level = rootEl.Q<TextElement>("Level");

        replayButton.clicked += OnClickReplayButton;
        level.text = $"Level {GameplayController.Instance.CurrentLevel} out of {GameplayController.Instance.LevelsCount}";
    }

    void OnClickReplayButton() {
        Debug.Log("REPLAY");
        GameplayController.Instance.ReplayGame();
    }

    public void ShowLosePanel(string reason) {
        root.style.display = DisplayStyle.Flex;
        loseReason.text = reason;

    }

    public void HideLosePanel() {
        root.style.display = DisplayStyle.None;
    }
}