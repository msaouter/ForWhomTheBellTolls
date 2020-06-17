// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControl/BellsInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BellsInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BellsInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BellsInput"",
    ""maps"": [
        {
            ""name"": ""Toll"",
            ""id"": ""2759be12-a9e5-4128-bbe1-057d5d3fc6eb"",
            ""actions"": [
                {
                    ""name"": ""Dyson"",
                    ""type"": ""Button"",
                    ""id"": ""b2275821-3278-468f-bd1f-310cd79f7423"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Statue"",
                    ""type"": ""Button"",
                    ""id"": ""3c51f438-1439-46f6-a092-28c1ab17a413"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Stele"",
                    ""type"": ""Button"",
                    ""id"": ""4ba83845-00ec-4f8e-b25a-703509c02e6d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arch"",
                    ""type"": ""Button"",
                    ""id"": ""c55398f9-3ac3-45d3-b82d-925fbb6f225d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sundial"",
                    ""type"": ""Button"",
                    ""id"": ""b47591a4-0e06-414c-8e01-5aaafc951ce6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""House"",
                    ""type"": ""Button"",
                    ""id"": ""9fa7a943-2a04-491a-bbe3-8f36b755ec23"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""a13a3312-3cf0-42fc-b5c7-00a51e43089f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c8920c94-9ba3-4a0d-885d-4bf2d6861de5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dyson"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8a02847-60e8-4d25-84b7-1ff49155a7f8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Statue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63238d8f-962c-4e9d-af89-2ac81d07ca7c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stele"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d367b910-98b2-4807-aeea-8a836e437819"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b85f598e-043c-4665-80ff-0e0183e72222"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sundial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7979e0d1-f829-4e08-bbf2-5ec8097ab439"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""House"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60f09832-195f-49ac-94d2-a709050d7d5f"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Toll
        m_Toll = asset.FindActionMap("Toll", throwIfNotFound: true);
        m_Toll_Dyson = m_Toll.FindAction("Dyson", throwIfNotFound: true);
        m_Toll_Statue = m_Toll.FindAction("Statue", throwIfNotFound: true);
        m_Toll_Stele = m_Toll.FindAction("Stele", throwIfNotFound: true);
        m_Toll_Arch = m_Toll.FindAction("Arch", throwIfNotFound: true);
        m_Toll_Sundial = m_Toll.FindAction("Sundial", throwIfNotFound: true);
        m_Toll_House = m_Toll.FindAction("House", throwIfNotFound: true);
        m_Toll_Restart = m_Toll.FindAction("Restart", throwIfNotFound: true);
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

    // Toll
    private readonly InputActionMap m_Toll;
    private ITollActions m_TollActionsCallbackInterface;
    private readonly InputAction m_Toll_Dyson;
    private readonly InputAction m_Toll_Statue;
    private readonly InputAction m_Toll_Stele;
    private readonly InputAction m_Toll_Arch;
    private readonly InputAction m_Toll_Sundial;
    private readonly InputAction m_Toll_House;
    private readonly InputAction m_Toll_Restart;
    public struct TollActions
    {
        private @BellsInput m_Wrapper;
        public TollActions(@BellsInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dyson => m_Wrapper.m_Toll_Dyson;
        public InputAction @Statue => m_Wrapper.m_Toll_Statue;
        public InputAction @Stele => m_Wrapper.m_Toll_Stele;
        public InputAction @Arch => m_Wrapper.m_Toll_Arch;
        public InputAction @Sundial => m_Wrapper.m_Toll_Sundial;
        public InputAction @House => m_Wrapper.m_Toll_House;
        public InputAction @Restart => m_Wrapper.m_Toll_Restart;
        public InputActionMap Get() { return m_Wrapper.m_Toll; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TollActions set) { return set.Get(); }
        public void SetCallbacks(ITollActions instance)
        {
            if (m_Wrapper.m_TollActionsCallbackInterface != null)
            {
                @Dyson.started -= m_Wrapper.m_TollActionsCallbackInterface.OnDyson;
                @Dyson.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnDyson;
                @Dyson.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnDyson;
                @Statue.started -= m_Wrapper.m_TollActionsCallbackInterface.OnStatue;
                @Statue.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnStatue;
                @Statue.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnStatue;
                @Stele.started -= m_Wrapper.m_TollActionsCallbackInterface.OnStele;
                @Stele.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnStele;
                @Stele.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnStele;
                @Arch.started -= m_Wrapper.m_TollActionsCallbackInterface.OnArch;
                @Arch.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnArch;
                @Arch.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnArch;
                @Sundial.started -= m_Wrapper.m_TollActionsCallbackInterface.OnSundial;
                @Sundial.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnSundial;
                @Sundial.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnSundial;
                @House.started -= m_Wrapper.m_TollActionsCallbackInterface.OnHouse;
                @House.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnHouse;
                @House.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnHouse;
                @Restart.started -= m_Wrapper.m_TollActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_TollActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_TollActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_TollActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dyson.started += instance.OnDyson;
                @Dyson.performed += instance.OnDyson;
                @Dyson.canceled += instance.OnDyson;
                @Statue.started += instance.OnStatue;
                @Statue.performed += instance.OnStatue;
                @Statue.canceled += instance.OnStatue;
                @Stele.started += instance.OnStele;
                @Stele.performed += instance.OnStele;
                @Stele.canceled += instance.OnStele;
                @Arch.started += instance.OnArch;
                @Arch.performed += instance.OnArch;
                @Arch.canceled += instance.OnArch;
                @Sundial.started += instance.OnSundial;
                @Sundial.performed += instance.OnSundial;
                @Sundial.canceled += instance.OnSundial;
                @House.started += instance.OnHouse;
                @House.performed += instance.OnHouse;
                @House.canceled += instance.OnHouse;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
            }
        }
    }
    public TollActions @Toll => new TollActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface ITollActions
    {
        void OnDyson(InputAction.CallbackContext context);
        void OnStatue(InputAction.CallbackContext context);
        void OnStele(InputAction.CallbackContext context);
        void OnArch(InputAction.CallbackContext context);
        void OnSundial(InputAction.CallbackContext context);
        void OnHouse(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
