using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystems
{
    public class InputManager : MonoBehaviour
    {

        [HideInInspector] public event Action OnPushLeftEvent;
        [HideInInspector] public event Action OnPushRightEvent;
        [HideInInspector] public event Action OnPushUpEvent;
        [HideInInspector] public event Action OnPushDownEvent;
        [HideInInspector] public event Action OnPushSelectEvent;
        [HideInInspector] public event Action OnPushAnyKeyEvent;
        [HideInInspector] public event Action OnPushPieceTypeChangeEvent;


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
            InputSystem.actions["Left"].performed += OnPushLeft;
            InputSystem.actions["Right"].performed += OnPushRight;
            InputSystem.actions["Up"].performed += OnPushUp;
            InputSystem.actions["Down"].performed += OnPushDown;
            InputSystem.actions["Select"].performed += OnPushSelect;
            InputSystem.actions["AnyKey"].performed += OnPushAnyKey;
            InputSystem.actions["PieceTypeChange"].performed += OnPushPieceTypeChange;

        }

        private void onDispose()
        {
            InputSystem.actions["Left"].performed -= OnPushLeft;
            InputSystem.actions["Right"].performed -= OnPushRight;
            InputSystem.actions["Up"].performed -= OnPushUp;
            InputSystem.actions["Down"].performed -= OnPushDown;
            InputSystem.actions["Select"].performed -= OnPushSelect;
            InputSystem.actions["Select"].performed -= OnPushAnyKey;
            InputSystem.actions["PieceTypeChange"].performed -= OnPushPieceTypeChange;

        }




        private void OnPushLeft(InputAction.CallbackContext context)
        {
            OnPushLeftEvent?.Invoke();
        }

        private void OnPushRight(InputAction.CallbackContext context)
        {
            OnPushRightEvent?.Invoke();
        }

        private void OnPushUp(InputAction.CallbackContext context)
        {
            OnPushUpEvent?.Invoke();
        }

        private void OnPushDown(InputAction.CallbackContext context)
        {
            OnPushDownEvent?.Invoke();
        }

        private void OnPushSelect(InputAction.CallbackContext context)
        {
            OnPushSelectEvent?.Invoke();
        }

        private void OnPushAnyKey(InputAction.CallbackContext context)
        {
            OnPushAnyKeyEvent?.Invoke();
        }

        private void OnPushPieceTypeChange(InputAction.CallbackContext context)
        {
            OnPushPieceTypeChangeEvent?.Invoke();
        }

    }
}