using UnityEngine;
using NaughtyAttributes;

namespace MyAssets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        
        [SerializeField] private string currentGroup;
        [SerializeField, InfoBox("Количество табличек, которые надо выбрать, чтобы убрать их с поля.")]
        private int tablesAmount;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            
            DontDestroyOnLoad(this);
        }

        public void ChangeCurrentGroup(string newGroup)
        {
            currentGroup = newGroup;
        }

        public string GetCurrentGroup()
        {
            return currentGroup;
        }

        public int GetTablesAmount()
        {
            return tablesAmount;
        }
    }
}
