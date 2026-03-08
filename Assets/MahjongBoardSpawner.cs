using System.Collections.Generic;
using UnityEngine;
using MyAssets.Scripts.Spawning;

namespace MyAssets.Scripts
{
    public class MahjongBoardSpawner : MonoBehaviour
    {
        [Header("Prefabs & Assets")]
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private List<TableAsset> availableTileTypes;
        
        [Header("Layout Settings")]
        [SerializeField] private float tileWidth = 1.1f;
        [SerializeField] private float tileHeight = 0.5f;
        [SerializeField] private float tileLength = 1.5f;

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            
            if (_gameManager == null)
            {
                Debug.LogError("GameManager не найден!");
                return;
            }

            GenerateBoard();
        }

        public void GenerateBoard()
        {
        
            ILayoutProvider layoutProvider = new PyramidLayoutProvider(tileWidth, tileHeight, tileLength);
            List<Vector3> positionsToSpawn = layoutProvider.GetTilePositions();

            
            int tablesAmount = _gameManager.GetTablesAmount();
            if (positionsToSpawn.Count % tablesAmount != 0)
            {
                Debug.LogError($"Ошибка! Раскладка требует {positionsToSpawn.Count} костяшек, а это не делится на группы по {tablesAmount}!");
                return;
            }


            ITileContentProvider contentProvider = new RandomTileDeckProvider(availableTileTypes);
            List<TableAsset> deck = contentProvider.GetShuffledDeck(positionsToSpawn.Count, tablesAmount);

       
            for (int i = 0; i < positionsToSpawn.Count; i++)
            {
                
                GameObject spawnedObj = Instantiate(tilePrefab, positionsToSpawn[i], Quaternion.identity, transform);
                
                
                if (spawnedObj.TryGetComponent(out InteractableTable interactable))
                {
                    interactable.Init(deck[i]);
                }
            }

            Debug.Log($"Доска сгенерирована! Всего фишек: {positionsToSpawn.Count}");
        }
    }
}