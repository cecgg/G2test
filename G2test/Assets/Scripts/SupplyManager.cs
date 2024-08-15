using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    public float SupplyAmount { get; private set; }

    void Start()
    {
        SupplyAmount = 1000;
    }

    public float GetSupplyCost(bool isForeignPort)
    {
        return isForeignPort ? 2.0f : 1.0f;
    }

    public void UseSupply(float amount)
    {
        SupplyAmount -= amount;
        if (SupplyAmount < 0)
        {
            SupplyAmount = 0;
        }
    }

    public void AddSupply(float amount)
    {
        SupplyAmount += amount;
    }
}
