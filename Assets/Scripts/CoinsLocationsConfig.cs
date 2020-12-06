using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Создаём новый пункт меню "Coin Locations Config" для создания новых конфигов
[CreateAssetMenu(menuName = "Coin Locations Config")]
public class CoinsLocationsConfig : ScriptableObject
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject coinLocations;

    // Префаб coin
    public GameObject coin
    {
        get
        {
            return coinPrefab;
        }
    }

    //  Точки спауна монет
    public List<Vector3> coinsSpawnPoints
    {
        get
        {
            return ExtractPositionPoints();
        }
    }

    // "Распаковываем" точки спауна врагов из контейнера
    private List<Vector3> ExtractPositionPoints()
    {
        var locations = new List<Vector3>();

        foreach (Transform child in coinLocations.transform)
        {
            locations.Add(child.transform.transform.position);
        }

        return locations;
    }
}