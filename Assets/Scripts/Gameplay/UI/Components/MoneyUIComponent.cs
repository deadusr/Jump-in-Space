using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Gameplay.Economy;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay.GameplayObjects;
using JumpInSpace.UnityServices;
using JumpInSpace.Utils;

namespace JumpInSpace.Gameplay.UI {
    [RequireComponent(typeof(UIDocument))]
    public class MoneyUIComponent : MonoBehaviour {
        Label username;
        Label moneyCounter;


        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();
            var rootEl = UIDocument.rootVisualElement.Q("Money");
            Debug.Log(rootEl);
            username = rootEl.Q<Label>("Username");
            moneyCounter = rootEl.Q<Label>("MoneyCounter");
        }

        void Start() {
            username.text = AccountManagerService.Username;
            OnMoneyChanged(EconomyManager.Instance.PlayerMoney);
        }

        void OnEnable() {
            EconomyManager.Instance.moneyAmountChanged += OnMoneyChanged;
        }

        void OnDisable() {
            EconomyManager.Instance.moneyAmountChanged -= OnMoneyChanged;
        }

        void OnMoneyChanged(int money) {
            moneyCounter.text = money.ToString();
        }
    }
}