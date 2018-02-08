using Stacker.Enums;
using Stacker.Tetros;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Stacker
{
    public class TetroPool : MonoBehaviour
    {
        [SerializeField] private Transform poolPos;
        [SerializeField] private Tetro tetroPrefab;
        [LabelText("Tetro Data Files")]
        [SerializeField] private TetroData[] tetroDatas = new TetroData[7];

        private Tetro[] pool;

        public void PreWarm()
        {
            pool = new Tetro[tetroDatas.Length];
            for (int i = 0; i < pool.Length; i++)
            {
                Tetro t = Instantiate(tetroPrefab, poolPos.position, Quaternion.identity);
                t.SetUp(tetroDatas[i]);
                t.transform.parent = poolPos;
                t.SetPool(this);
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
            tetro.transform.SetParent(poolPos);
            tetro.transform.localPosition = Vector3.zero;
        }

    }
}