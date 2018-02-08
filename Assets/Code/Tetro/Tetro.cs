using HenderStudios.Events;
using Sirenix.OdinInspector;
using Stacker.Enums;
using Stacker.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Stacker.Tetros
{
    public class Tetro : MonoBehaviour
    {
        [SerializeField]
        private TetroType tetroType;

        [SerializeField] private Tile[] tiles;
        [SerializeField] private UnityEvent onStartFalling;
        [SerializeField] private UnityEvent onDie;
        
        private TetroGrid grid;
        private TetroPool pool;
        private TetroData data;

        public UnityEvent OnStartFalling
        {
            get
            {
                return onStartFalling;
            }
        }
        public UnityEvent OnDie
        {
            get
            {
                return onDie;
            }
        }
        public bool Active
        {
            get; private set;
        }
        public bool Falling
        {
            get; set;
        }

        public Tile[] Tiles
        {
            get
            {
                return tiles;
            }
        }

        private void Awake()
        {
            grid = FindObjectOfType<TetroGrid>();
        }

        public void SetUp(TetroData data)
        {
            this.data = data;
            name = $"Tetro ({data.Type})";
            tetroType = data.Type;
            SetTilePositions(data.DefaultTilePositions);
            ApplyColor();
            Active = false;
        }

        private void SetTilePositions(Vector3[] positions)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.localPosition = positions[i];
            }
        }

        private void SetTilePositions(Vector2[] positions)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.localPosition = positions[i];
            }
        }

        public void SetRotation(int index)
        {
            index = Mathf.Clamp(index, 0, data.TileRotationPositions.GetLength(0)); // Just in case "index" is out of bounds
            //SetTilePositions(data.TileRotationPositions[index]); // TODO Refractor "TileRotationPositions" as a jagged array instead of a 2D array
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.localPosition = data.TileRotationPositions[index, i];
            }
        }

        public void SetPool(TetroPool tetroPool)
        {
            pool = tetroPool;
        }

        public void StartFalling(float fallSpeed)
        {
            Active = true;
            //Falling = true;
            EventManager.TriggerEvent(EventNames.NewTetroFalling, new Message(this));
            OnStartFalling?.Invoke();
        }

        [Button]
        public void ApplyColor()
        {
            foreach (var tile in tiles)
            {
                tile.SetColor(tetroType);
            }
        }

        public TetroType GetTetroType()
        {
            return tetroType;
        }

        public void EndFall()
        {
            Die();
        }

        public void Die()
        {
            OnDie?.Invoke();
            foreach (var tile in tiles)
            {
                grid.SetCellFull(tile.transform.position, tetroType);
            }
            pool?.ReturnTetro(this);
            //Falling = false;
            Active = false;
        }
    }
}