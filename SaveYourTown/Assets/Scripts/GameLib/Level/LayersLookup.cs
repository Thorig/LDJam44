using System.Collections.Generic;
using UnityEngine;

namespace GameLib.Level
{
    public class LayersLookup : MonoBehaviour
    {
        [SerializeField]
        protected List<string> layerMapKeys;

        [SerializeField]
        protected List<int> layerMapValues;

        public int giveLayerNumber(string layerName)
        {
            return layerMapValues[layerMapKeys.IndexOf(layerName)];
        }

        public void addLayer(string layerName, int number)
        {
            layerMapKeys.Add(layerName);
            layerMapValues.Add(number);
        }

        private void OnEnable()
        {
            init();
        }

        protected void Awake()
        {
            init();
        }

        public virtual void Start()
        {
            init();
        }

        protected virtual void init()
        {
            addLayer("Default", LayerMask.NameToLayer("Default"));
            addLayer("Player", LayerMask.NameToLayer("Player"));
            addLayer("Enemy", LayerMask.NameToLayer("Enemy"));
            addLayer("EnemyDetector", LayerMask.NameToLayer("EnemyDetector"));
            addLayer("Item", LayerMask.NameToLayer("Item"));
            addLayer("Trap", LayerMask.NameToLayer("Trap"));
            addLayer("Tile", LayerMask.NameToLayer("Tile"));
            addLayer("Slope", LayerMask.NameToLayer("Slope"));
        }
    }
}