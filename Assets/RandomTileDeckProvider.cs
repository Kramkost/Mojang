using System.Collections.Generic;
using UnityEngine;
using MyAssets.Scripts;

namespace MyAssets.Scripts.Spawning
{
    // ==========================================
    // ИНТЕРФЕЙСЫ (Абстракции)
    // ==========================================

    // 1. Интерфейс для выдачи позиций (Раскладка уровня)
    public interface ILayoutProvider
    {
        List<Vector3> GetTilePositions();
    }

    // 2. Интерфейс для выдачи ассетов (Перемешивание колоды)
    public interface ITileContentProvider
    {
        List<TableAsset> GetShuffledDeck(int totalTilesRequired, int groupSize);
    }

    // ==========================================
    // РЕАЛИЗАЦИИ
    // ==========================================

    // Провайдер контента: собирает колоду так, чтобы всем костяшкам хватило пары (или тройки)
    public class RandomTileDeckProvider : ITileContentProvider
    {
        private readonly List<TableAsset> _availableAssets;

        public RandomTileDeckProvider(List<TableAsset> availableAssets)
        {
            _availableAssets = availableAssets;
        }

        public List<TableAsset> GetShuffledDeck(int totalTilesRequired, int groupSize)
        {
            List<TableAsset> deck = new List<TableAsset>();
            int uniqueGroupsNeeded = totalTilesRequired / groupSize;

            // Набираем нужные группы
            for (int i = 0; i < uniqueGroupsNeeded; i++)
            {
                // Берем случайный ассет из доступных
                TableAsset randomAsset = _availableAssets[Random.Range(0, _availableAssets.Count)];
                
                // Добавляем его нужное количество раз (например, 2 для пары)
                for (int j = 0; j < groupSize; j++)
                {
                    deck.Add(randomAsset);
                }
            }

            // Перемешиваем колоду (Алгоритм Фишера-Йетса)
            for (int i = 0; i < deck.Count; i++)
            {
                TableAsset temp = deck[i];
                int randomIndex = Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }

            return deck;
        }
    }

    // Пример раскладки: Классическая плоская сетка
    public class GridLayoutProvider : ILayoutProvider
    {
        private readonly int _rows;
        private readonly int _cols;
        private readonly float _spacing;

        public GridLayoutProvider(int rows, int cols, float spacing)
        {
            _rows = rows;
            _cols = cols;
            _spacing = spacing;
        }

        public List<Vector3> GetTilePositions()
        {
            List<Vector3> positions = new List<Vector3>();
            Vector3 startPos = new Vector3(-(_cols * _spacing) / 2f, 0, -(_rows * _spacing) / 2f);

            for (int x = 0; x < _cols; x++)
            {
                for (int z = 0; z < _rows; z++)
                {
                    positions.Add(startPos + new Vector3(x * _spacing, 0, z * _spacing));
                }
            }
            return positions;
        }
    }

    public class PyramidLayoutProvider : ILayoutProvider
    {
        private readonly float _spacingX;
        private readonly float _spacingY;
        private readonly float _spacingZ;

        public PyramidLayoutProvider(float spaceX, float spaceY, float spaceZ)
        {
            _spacingX = spaceX;
            _spacingY = spaceY;
            _spacingZ = spaceZ;
        }

        public List<Vector3> GetTilePositions()
        {
            List<Vector3> positions = new List<Vector3>();
            

            
  
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    positions.Add(new Vector3(x * _spacingX, y * _spacingY, 0));


            for (int x = 0; x < 2; x++)
                for (int y = 0; y < 2; y++)
                    positions.Add(new Vector3((x + 0.5f) * _spacingX, (y + 0.5f) * _spacingY, -_spacingZ));

       
            positions.Add(new Vector3(1.5f * _spacingX, 1.5f * _spacingY, -_spacingZ * 2));

            return positions;
        }
    }
}