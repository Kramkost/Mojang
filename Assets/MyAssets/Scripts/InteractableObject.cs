using UnityEngine;
using UnityEngine.Serialization;

namespace MyAssets.Scripts
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private TableAsset tableAsset;
        
        private bool _isSelected;
        
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public string GetGroup()
        {
            return tableAsset.Group;
        }

        public void Selected()
        {
            _isSelected = true;
            ChangeMaterial();
        }

        public void UnSelected()
        {
            _isSelected = false;
            ChangeMaterial();
        }

        private void ChangeMaterial()
        {
            _meshRenderer.material = _isSelected ? tableAsset.SelectedTable : tableAsset.NonSelectedTable;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
