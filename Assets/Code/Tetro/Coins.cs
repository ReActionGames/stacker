using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Coins : MonoBehaviour
    {
        [SerializeField] private Tetro tetro;
        [SerializeField] private List<Tile> activeCoinPositions;

        private Tile[] tiles;
        private GameSettings settings;

        private void Awake()
        {
            tetro = GetComponent<Tetro>();
            tetro.OnStartFalling.AddListener(SetUp);
            tetro.OnDie.AddListener(OnDie);
            tiles = tetro.Tiles;
            settings = FindObjectOfType<GameController>().GameSettings;
        }

        private void SetUp()
        {
            activeCoinPositions.Clear();
            foreach (Tile tile in tiles)
            {
                tile.SetHasCoin(false);
                float chance = Random.Range(0.0f, 1.0f);
                if (chance < settings.CoinSpawnChance)
                {
                    SpawnCoin(tile);
                }
            }
        }

        private void SpawnCoin(Tile tile)
        {
            tile.SetHasCoin(true);
            activeCoinPositions.Add(tile);
        }

        private void OnDie()
        {
            TetroGrid grid = FindObjectOfType<TetroGrid>();
            foreach (Tile tile in activeCoinPositions)
            {
                grid.SetCellCoin(tile.transform.position);
            }
        }
    }
}