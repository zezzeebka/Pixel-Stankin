using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] CoinsLocationsConfig coinLocationsConfig;

    void Start()
    {
        // Вызываем метод SpawnCoins при инициации спаунера
        SpawnCoins();
    }

    // Спауним монеты
    private void SpawnCoins()
    {
        // Получаем список локаций для спауна монет
        List<Vector3> points = coinLocationsConfig.coinsSpawnPoints;

        // Проходимся по точкам спауна ним циклом
        foreach (Vector3 spawnPoint in points)
        {
            // Размещаем монеты на карте
            Instantiate(
              // Префаб монеты
              coinLocationsConfig.coin,
              // Точка спауна
              spawnPoint,
              // Ассоциируем созданный объект с игровым миром
              Quaternion.identity
            );

            /**
            * Данный шаг может вам не понадобиться. По какой то непонятной мне причине,
            * координата z точек спауна периодически меняется, хотя должна оставаться нулём.
            * Если у вас такая же проблема, просто заменит код в цикле на этот
            *
            * var position = spawnPoint;
            * position.z = 0;
            *
            *  Instantiate(coinLocationsConfig.coin, position, Quaternion.identity);
            **/
        }
    }
}
