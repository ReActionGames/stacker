using HenderStudios.Events;
using Sirenix.OdinInspector;
using Stacker.Enums;
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

        public Tile[] Tiles
        {
            get
            {
                return tiles;
            }
        }

        private void Start()
        {
            grid = FindObjectOfType<TetroGrid>();
            ApplyColor();
            Active = false;
        }

        public void SetPool(TetroPool tetroPool)
        {
            pool = tetroPool;
        }

        public void StartFalling(float fallSpeed)
        {
            Active = true;
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
            Active = false;
        }
    }
}