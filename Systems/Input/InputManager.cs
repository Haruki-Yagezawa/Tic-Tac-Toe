using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystems
{
    public class InputManager : MonoBehaviour
    {

        [HideInInspector] public event Action OnPushAnyKeyEvent;
        [HideInInspector] public event Action OnPush1PLeftEvent;
        [HideInInspector] public event Action OnPush1PRightEvent;
        [HideInInspector] public event Action OnPush1PUpEvent;
        [HideInInspector] public event Action OnPush1PDownEvent;
        [HideInInspector] public event Action OnPush1PSelectEvent;
        [HideInInspector] public event Action OnPush1PPieceTypeChangeEvent;
        [HideInInspector] public event Action OnPush2PLeftEvent;
        [HideInInspector] public event Action OnPush2PRightEvent;
        [HideInInspector] public event Action OnPush2PUpEvent;
        [HideInInspector] public event Action OnPush2PDownEvent;
        [HideInInspector] public event Action OnPush2PSelectEvent;
        [HideInInspector] public event Action OnPush2PPieceTypeChangeEvent;


        private void Awake()
        {
            OnSubscribe();
        }

        private void OnDestroy()
        {
            onDispose();
        }

        private void OnSubscribe()
        {
            InputSystem.actions["AnyKey"].performed += OnPushAnyKey;
            InputSystem.actions["1PLeft"].performed += OnPush1PLeft;
            InputSystem.actions["1PRight"].performed += OnPush1PRight;
            InputSystem.actions["1PUp"].performed += OnPush1PUp;
            InputSystem.actions["1PDown"].performed += OnPush1PDown;
            InputSystem.actions["1PSelect"].performed += OnPush1PSelect;
            InputSystem.actions["1PPieceTypeChange"].performed += OnPush1PPieceTypeChange;
            InputSystem.actions["2PLeft"].performed += OnPush2PLeft;
            InputSystem.actions["2PRight"].performed += OnPush2PRight;
            InputSystem.actions["2PUp"].performed += OnPush2PUp;
            InputSystem.actions["2PDown"].performed += OnPush2PDown;
            InputSystem.actions["2PSelect"].performed += OnPush2PSelect;
            InputSystem.actions["2PPieceTypeChange"].performed += OnPush2PPieceTypeChange;

        }

        private void onDispose()
        {
            InputSystem.actions["AnyKey"].performed -= OnPushAnyKey;
            InputSystem.actions["1PLeft"].performed -= OnPush1PLeft;
            InputSystem.actions["1PRight"].performed -= OnPush1PRight;
            InputSystem.actions["1PUp"].performed -= OnPush1PUp;
            InputSystem.actions["1PDown"].performed -= OnPush1PDown;
            InputSystem.actions["1PSelect"].performed -= OnPush1PSelect;
            InputSystem.actions["1PPieceTypeChange"].performed -= OnPush1PPieceTypeChange;
            InputSystem.actions["2PLeft"].performed -= OnPush2PLeft;
            InputSystem.actions["2PRight"].performed -= OnPush2PRight;
            InputSystem.actions["2PUp"].performed -= OnPush2PUp;
            InputSystem.actions["2PDown"].performed -= OnPush2PDown;
            InputSystem.actions["2PSelect"].performed -= OnPush2PSelect;
            InputSystem.actions["2PPieceTypeChange"].performed -= OnPush2PPieceTypeChange;

        }




        private void OnPushAnyKey(InputAction.CallbackContext context)
        {
            OnPushAnyKeyEvent?.Invoke();
        }

        private void OnPush1PLeft(InputAction.CallbackContext context)
        {
            OnPush1PLeftEvent?.Invoke();
        }

        private void OnPush1PRight(InputAction.CallbackContext context)
        {
            OnPush1PRightEvent?.Invoke();
        }

        private void OnPush1PUp(InputAction.CallbackContext context)
        {
            OnPush1PUpEvent?.Invoke();
        }

        private void OnPush1PDown(InputAction.CallbackContext context)
        {
            OnPush1PDownEvent?.Invoke();
        }

        private void OnPush1PSelect(InputAction.CallbackContext context)
        {
            OnPush1PSelectEvent?.Invoke();
        }

        private void OnPush1PPieceTypeChange(InputAction.CallbackContext context)
        {
            OnPush1PPieceTypeChangeEvent?.Invoke();
        }


        private void OnPush2PLeft(InputAction.CallbackContext context)
        {
            OnPush2PLeftEvent?.Invoke();
        }

        private void OnPush2PRight(InputAction.CallbackContext context)
        {
            OnPush2PRightEvent?.Invoke();
        }

        private void OnPush2PUp(InputAction.CallbackContext context)
        {
            OnPush2PUpEvent?.Invoke();
        }

        private void OnPush2PDown(InputAction.CallbackContext context)
        {
            OnPush2PDownEvent?.Invoke();
        }

        private void OnPush2PSelect(InputAction.CallbackContext context)
        {
            OnPush2PSelectEvent?.Invoke();
        }

        private void OnPush2PPieceTypeChange(InputAction.CallbackContext context)
        {
            OnPush2PPieceTypeChangeEvent?.Invoke();
        }
    }
}