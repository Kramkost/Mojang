using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyAssets.Scripts
{
    public class InteractableTable : MonoBehaviour
    {
        [SerializeField, InfoBox("Это значение нельзя менять в play моде", EInfoBoxType.Warning)]
        private bool useAsset;
        
        [SerializeField, ShowIf(nameof(useAsset))]
        private TableAsset tableAsset;
        
        [SerializeField, HideIf(nameof(useAsset))]
        private string groupName;
        [SerializeField, HideIf(nameof(useAsset))]
        private Material selectedTable;
        [SerializeField, HideIf(nameof(useAsset))]
        private Material nonSelectedTable;
        
        private bool _isSelected;
        private MeshRenderer _meshRenderer;
        private bool _use;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public string GetGroup()
        {
            var group = useAsset ? tableAsset.Group :  groupName;
            return group;
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
            if (useAsset)
            {
                _meshRenderer.material = _isSelected ? tableAsset.SelectedTable : tableAsset.NonSelectedTable;
            }
            else
            {
                _meshRenderer.material = _isSelected ? selectedTable : nonSelectedTable;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _use = useAsset;
        }

        private void Update()
        {
            if (useAsset != _use) useAsset = _use;
        }

    public void Init(TableAsset asset)
   { 
    useAsset = true;
    tableAsset = asset;
    _use = true;
    UnSelected(); 
   }
    }


}
