using UnityEngine;
using Stacker.Enums;
using Sirenix.OdinInspector;
using Stacker.Tetros;
using UnityEngine.Events;

namespace Stacker
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos;
        [SerializeField] private TetroPool tetroPool;
        [SerializeField] private float fallSpeed;

        [SerializeField] private UnityEvent onTetroDie;

        public UnityEvent OnTetroDie
        {
            get
            {
                return onTetroDie;
            }
        }
        
        public void PreWarmPool()
        {
            Tetro[] tetros = tetroPool.PreWarm();
            foreach (var tetro in tetros)
            {
                tetro.OnDie.AddListener(InvokeOnTetroDie);
            }
        }

        private void InvokeOnTetroDie()
        {
            OnTetroDie?.Invoke();
        }

        [Button(ButtonSizes.Medium)]
        public Tetro SpawnRandomTetro()
        {
            int length = System.Enum.GetNames(typeof(TetroType)).Length;
            int i = Random.Range(1, length);
            TetroType type = (TetroType)i;
            Tetro tetro = tetroPool.GetTetro(type);
            tetro.transform.position = spawnPos.position;
            tetro.gameObject.SetActive(true);
            tetro.StartFalling(fallSpeed);
            return tetro;
        }
    }
}