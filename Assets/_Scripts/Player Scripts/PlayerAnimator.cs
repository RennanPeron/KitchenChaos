using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Cria um atributo animator do tipo Animator vazio.
    private Animator animator;

    // Cria um campo do tipo Player vazio para ser preenchido na Unity no PlayerVisuals com o "objeto do Unity" Player.
    // Lembrando que o PlayerVisuals está dentro de Player e o que recebe os input é o Player, o PlayerVisual só serve para as animações e modelo.
    [SerializeField] private Player player;

    // Cria o atributo do tipo string só para evitar erro de digitação e para debug, caso de algo errado.
    private const string IS_WALKING = "IsWalking";

    // Awake é uma função que é ativada quando a cena é inicializada, antes de Start(). Nesse caso é ativa quando o Player está na cena ativa no momento.
    private void Awake() {
        // O atributo recebe o Animator selecionado lá no Unity.
        animator = GetComponent<Animator>();
    }

    // A cada frame, verifica se o Player está se movendo. Explicação no script do Player.
    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}
