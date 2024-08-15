using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Port : MonoBehaviour
{
    public GameObject portMenu;
    public Text productionLevelText;
    public Text supplyLevelText;
    public Text protectionLevelText;
    public Image resourceIcon;
    public int productionLevel = 0;
    public int supplyLevel = 1;
    public int protectionLevel = 0;
    public float supplyRefillAmount = 1;
    public string PortName;

    private GameManager gameManager;
    private int portLevel = 1;
    private readonly int[] levelUpgradesCost = { 50, 100, 150 };
    private readonly float[] refillAmounts = { 1f, 2f, 3f };

    void Start()
    {
        portMenu.SetActive(false);
        UpdateUI();
    }

    void OnMouseDown()
    {
        portMenu.SetActive(true);
    }

    public void OnUpgradeProductionClicked()
    {
        if (gameManager.money >= 50)
        {
            gameManager.money -= 50;
            productionLevel++;
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ����� ��� �������� ������������!");
        }
    }

    public void OnUpgradeSupplyClicked()
    {
        if (gameManager.money >= 50)
        {
            gameManager.money -= 50;
            supplyLevel++;
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ����� ��� �������� ���������!");
        }
    }

    public void OnUpgradeProtectionClicked()
    {
        if (gameManager.money >= 50)
        {
            gameManager.money -= 50;
            protectionLevel++;
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ����� ��� �������� ������!");
        }
    }

    public void OnUpgradePortLevelClicked()
    {
        if (portLevel < 3 && gameManager.money >= levelUpgradesCost[portLevel - 1])
        {
            gameManager.money -= levelUpgradesCost[portLevel - 1];
            portLevel++;
            supplyRefillAmount = refillAmounts[portLevel - 1];
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ����� ��� �������� ����� ��� ������������ ������� ���������!");
        }
    }

    private void UpdateUI()
    {
        productionLevelText.text = $"������������: {productionLevel}";
        supplyLevelText.text = $"���������: {supplyLevel} (������� {portLevel})";
        protectionLevelText.text = $"������: {protectionLevel}";
        // �������� ������ �������, ���� �����
    }

    public float GetSupplyCost(bool isForeignPort)
    {
        return isForeignPort ? 3.0f : 1.5f; // ��������� ��������
    }

    private class GameManager
    {
        internal int money;
    }
}

internal class GameManager
{
}