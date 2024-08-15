using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour // ��������� Ship � MonoBehaviour ��� ����������� �������������� �� ������
{
    public float Speed { get; set; }
    public float CargoCapacity { get; set; }
    public float SupplyConsumption { get; set; }
    public float CurrentSupply { get; private set; }
    public Transform targetPort; // ������� ���� ��� ������������ �������
    public float MoveSpeed = 5f; // �������� ����������� �������

    void Start()
    {
        CurrentSupply = 100; // ��������� ���������� ���������
    }

    void Update()
    {
        if (targetPort != null)
        {
            MoveTowardsPort();
        }
    }

    public void InitializeShip(float speed, float cargoCapacity, float supplyConsumption)
    {
        Speed = speed;
        CargoCapacity = cargoCapacity;
        SupplyConsumption = supplyConsumption;
    }

    private void MoveTowardsPort()
    {
        Vector3 direction = targetPort.position - transform.position;
        transform.position += direction.normalized * MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPort.position) < 0.1f)
        {
            // �������� �����
            Debug.Log("������� ������ �����.");
            targetPort = null; // ���������������
        }
    }

    public float CalculateSupplyUsage(float distance)
    {
        return distance * SupplyConsumption;
    }

    public void RefillSupply(Port port, bool isForeignPort, float distanceToPort)
    {
        float usedSupply = CalculateSupplyUsage(distanceToPort) * 2; // ������ ���� � �������
        CurrentSupply -= usedSupply;

        float costPerUnit = port.GetSupplyCost(isForeignPort);
        float neededSupply = usedSupply;

        // ��������� ���������
        CurrentSupply += neededSupply;

        // ������������ ��������� ����������
        float totalCost = neededSupply * costPerUnit;

        Debug.Log($"������� �������� {usedSupply} ������ ��������� �� ���� � ���� {port.PortName} � �������.");
        Debug.Log(message: $"������� �������� {neededSupply} ������ ��������� � ����� {port.PortName} �� {totalCost}.");
    }
}
