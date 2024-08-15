using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour // Превратим Ship в MonoBehaviour для возможности взаимодействия со сценой
{
    public float Speed { get; set; }
    public float CargoCapacity { get; set; }
    public float SupplyConsumption { get; set; }
    public float CurrentSupply { get; private set; }
    public Transform targetPort; // Добавим цель для передвижения корабля
    public float MoveSpeed = 5f; // Скорость перемещения корабля

    void Start()
    {
        CurrentSupply = 100; // Начальное количество снабжения
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
            // Достигли порта
            Debug.Log("Корабль достиг порта.");
            targetPort = null; // Останавливаемся
        }
    }

    public float CalculateSupplyUsage(float distance)
    {
        return distance * SupplyConsumption;
    }

    public void RefillSupply(Port port, bool isForeignPort, float distanceToPort)
    {
        float usedSupply = CalculateSupplyUsage(distanceToPort) * 2; // Дорога туда и обратно
        CurrentSupply -= usedSupply;

        float costPerUnit = port.GetSupplyCost(isForeignPort);
        float neededSupply = usedSupply;

        // Пополняем снабжение
        CurrentSupply += neededSupply;

        // Рассчитываем стоимость пополнения
        float totalCost = neededSupply * costPerUnit;

        Debug.Log($"Корабль затратил {usedSupply} единиц снабжения на путь в порт {port.PortName} и обратно.");
        Debug.Log(message: $"Корабль пополнил {neededSupply} единиц снабжения в порту {port.PortName} за {totalCost}.");
    }
}
