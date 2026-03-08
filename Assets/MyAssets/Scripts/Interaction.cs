using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace MyAssets.Scripts
{
    public class Interaction : MonoBehaviour
    {
        private Ray _ray;
        private RaycastHit _hit;
        private GameInput _gameInput;
        
        [SerializeField] private List<GameObject> gameObjects;
        
        private GameManager _gameManager;

        private void OnEnable()
        {
            _gameInput.Gameplay.Interact.performed += Interact;
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.Interact.performed -= Interact;
        }

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        private void Start()
        {
            if (_gameManager == null)
            {
                Debug.LogError("НЕ НАЙДЕН GAME MANAGER В СКРИПТЕ Interaction");
            }
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                var tapPoint = ctx.ReadValue<Vector2>();

                if (Camera.main != null) _ray = Camera.main.ScreenPointToRay(tapPoint);
                if (Physics.Raycast(_ray, out _hit))
                {
                    if (_hit.collider.gameObject.CompareTag("Interactable"))
                    {
                        var table = _hit.collider.gameObject;
                        
                        if (table.TryGetComponent(out InteractableObject interactableObject))
                        {
                            bool haveObject = gameObjects.Find(x => x == table);

                            if (haveObject)
                            {
                                UnSelect(table);
                            }
                            else
                            {
                                if (interactableObject.GetGroup() == _gameManager.GetCurrentGroup() || _gameManager.GetCurrentGroup() == "None")
                                {
                                    gameObjects.Add(table);
                                    interactableObject.Selected();
                                    _gameManager.ChangeCurrentGroup(interactableObject.GetGroup());

                                    if (gameObjects.Count == _gameManager.GetTablesAmount())
                                    {
                                        foreach (var obj in gameObjects.ToList())
                                        {
                                            DestroyTable(obj);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var obj in gameObjects.ToList())
                                    {
                                        UnSelect(obj);
                                    }
                                    
                                    _gameManager.ChangeCurrentGroup("None");
                                }
                            }
                        }
                        else Debug.LogError("НА ОБЪЕКТЕ НЕТ СКРИПТА ВЗАИМОДЕЙСТВИЯ!!!");
                    }
                }
                else
                {
                    _gameManager.ChangeCurrentGroup("None");
                    foreach (var table in gameObjects.ToList())
                    {
                        UnSelect(table);
                    }
                }
            }
        }

        private void UnSelect(GameObject table)
        {
            table.GetComponent<InteractableObject>().UnSelected();
            gameObjects.Remove(table);
        }

        private void DestroyTable(GameObject table)
        {
            gameObjects.Remove(table);
            table.GetComponent<InteractableObject>().Destroy();
        }
    }
}   
