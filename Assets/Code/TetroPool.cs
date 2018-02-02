using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stacker
{
    public class TetroPool : MonoBehaviour
    {
        [SerializeField] private Transform poolPos;
        [SerializeField] private Tetro[] tetroPrefabs;

        private Tetro[] pool;

        public void PreWarm()
        {
            pool = new Tetro[tetroPrefabs.Length];
            for (int i = 0; i < tetroPrefabs.Length; i++)
            {
                Tetro t = Instantiate(tetroPrefabs[i], poolPos.position, Quaternion.identity);
                t.transform.parent = poolPos;
                pool[i] = t;
            }
        }

        public Tetro GetTetro(TetroType type)
        {
            Tetro tetro = pool.Where(t => t.GetTetroType() == type)
                                      .FirstOrDefault();
            tetro.transform.parent = null;
            tetro.gameObject.SetActive(true);
            return tetro;
        }

        public void ReturnTetro(Tetro tetro)
        {
            tetro.gameObject.SetActive(false);
            tetro.transform.SetParent(poolPos, false);
        }

    }
}