// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Core/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3ed6f7af-7635-43bb-8f6e-73032f8186ec"",
            ""actions"": [
                {
                    ""name"": ""ToggleDebug"",
                    ""type"": ""Button"",
                    ""id"": ""286bbc36-f1d5-406b-88ff-f1e2192db9ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ConfirmInput"",
                    ""type"": ""Button"",
                    ""id"": ""444afadb-41e1-4b5f-98da-697f1f107e51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ace6463b-5418-4d10-98d6-1ddd5765cba2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""12d46b40-3cfa-4b53-8637-224327bc1ee5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""081829c8-7dee-4b8e-b8ae-a93d02202a47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DialogueNextSentence"",
                    ""type"": ""Button"",
                    ""id"": ""4dea7dd9-4d3e-433d-9735-07e4a742fe9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0b0cda2a-0f53-491b-8462-29deecd6682c"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffb8c82d-b716-4988-a72a-04461ccc3ca3"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04f051a8-68a1-4f22-9852-4ea8e88b2967"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""547859d5-ed11-4190-869e-9e180f6a8541"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7a8b22d7-e6a6-40f4-8682-9d07a75546ca"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ca253c73-4994-45d2-80ea-3407cff20a53"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6c7cc3ad-90cd-497b-b801-6a4a455716a7"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f39a48da-77ae-486d-9943-db08331a7a58"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DialogueNextSentence"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_ToggleDebug = m_Player.FindAction("ToggleDebug", throwIfNotFound: true);
        m_Player_ConfirmInput = m_Player.FindAction("ConfirmInput", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_DialogueNextSentence = m_Player.FindAction("DialogueNextSentence", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_ToggleDebug;
    private readonly InputAction m_Player_ConfirmInput;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_DialogueNextSentence;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleDebug => m_Wrapper.m_Player_ToggleDebug;
        public InputAction @ConfirmInput => m_Wrapper.m_Player_ConfirmInput;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @DialogueNextSentence => m_Wrapper.m_Player_DialogueNextSentence;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @ToggleDebug.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ToggleDebug.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ToggleDebug.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ConfirmInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConfirmInput;
                @ConfirmInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConfirmInput;
                @ConfirmInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConfirmInput;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @DialogueNextSentence.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDialogueNextSentence;
                @DialogueNextSentence.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDialogueNextSentence;
                @DialogueNextSentence.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDialogueNextSentence;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleDebug.started += instance.OnToggleDebug;
                @ToggleDebug.performed += instance.OnToggleDebug;
                @ToggleDebug.canceled += instance.OnToggleDebug;
                @ConfirmInput.started += instance.OnConfirmInput;
                @ConfirmInput.performed += instance.OnConfirmInput;
                @ConfirmInput.canceled += instance.OnConfirmInput;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @DialogueNextSentence.started += instance.OnDialogueNextSentence;
                @DialogueNextSentence.performed += instance.OnDialogueNextSentence;
                @DialogueNextSentence.canceled += instance.OnDialogueNextSentence;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnToggleDebug(InputAction.CallbackContext context);
        void OnConfirmInput(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDialogueNextSentence(InputAction.CallbackContext context);
    }
}
