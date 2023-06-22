using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem 
{
    void InitGame();
    void EnterGame();

    void UpdateGame();

    void LateUpdateGame();

    void LeaveGame();
}
