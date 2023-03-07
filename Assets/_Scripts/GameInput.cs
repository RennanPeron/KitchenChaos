using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();    
    }

    public Vector2 GetMovementVectorNormalized(){
        // Vetor de 2 direções, x e y, para os input. Como o teclado não tem botões nas três direções não faz sentido atribuir os inputs as três dimensões, e sim receber os inputs e depois passá-lo para as três dimensões em moveDir.
        // Recebe o input de WASD feito no PlayerInputActions dentro do Unity e do script gerado automaticamente pelo PlayerInputActions.
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Pra que serve o .normalized? Para evitar que situações de movimento diagonal, tipo apertar W e D, as velocidades somem e o personagem se mova mais rápido nas diagonais, desse jeito a velocidade será sempre a média, ou seja, a velocidade padrão.
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
