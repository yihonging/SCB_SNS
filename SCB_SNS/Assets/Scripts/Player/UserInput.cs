using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;









public class UserInput :Singleton<UserInput>
{

    public bool shootyAndMove;
    private PlayerInput _playerInput;

    private InputAction _shootyAndMove;


   protected override void Awake()
    {
        base.Awake();
        _playerInput=GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupInput();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }
    void SetupInput()
    {
        _shootyAndMove=_playerInput.actions["Move"];
    }
    void UpdateInput()
    {
        shootyAndMove=_shootyAndMove.WasPerformedThisFrame();
    }


}
