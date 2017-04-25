using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IGameManager
{

    [SerializeField]
    private Text testLabel;


    public ManagerStatus Status
    {
        get;
        private set;
    }

    public void Startup()
    {
        this.Status = ManagerStatus.Started;
    }

    public void RefreshUI() {
        this.testLabel.text = Managers.Player.CurrentEnergy.ToString();
    }

}
