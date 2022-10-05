using System;
using UnityEditor;
using UnityEngine;

namespace Grid
{
    public class EnableGridVisual : MonoBehaviour
    {
        [SerializeField] private GameObject gridVisualizer;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                EnableOrDisable();
            if (Input.GetKeyDown(KeyCode.Escape))
                DisabledGrid();
        }

        private void DisabledGrid()
        {
            gridVisualizer.SetActive(false);
        }

        public void EnableOrDisable()
        {
           gridVisualizer.SetActive(!gridVisualizer.activeInHierarchy);
        }
    }
}