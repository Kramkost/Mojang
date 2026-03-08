using UnityEngine;
using UnityEngine.Serialization;

namespace MyAssets.Scripts
{
    [CreateAssetMenu(fileName = "tableAsset", menuName = "MyAssets/TableAsset")]
    public class TableAsset : ScriptableObject
    {
        [SerializeField] private string group;

        [SerializeField] private Material selectedTable;
        [SerializeField] private Material nonSelectedTable;
        
        public string Group => group;
        public Material SelectedTable => selectedTable;
        public Material NonSelectedTable => nonSelectedTable;
    }
}
