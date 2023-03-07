using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   // SerializeField = É exibido no motor da Unity, mesmo o atributo estando como privado.
   // E pq não usar public? Isso pode causar alguns problemas mais tarde, pois, qualquer classe poderá enxergá-lo, e alterá-lo. Para escrevermos um código limpo e de fácil entendimento, vamos evitar ao máximo usar o public.
   // moveSpeed será multiplicado pelo deltaTime para aumentar a velocidade do personagem.
   [SerializeField] private float moveSpeed = 7f;

   // isWalking é do tipo bool, ou seja, é true ou false, e será passado para uma função pública para verificar se o jogador está andando.
   [SerializeField] private bool isWalking;

   // Cria o atributo que vai armazenar os inputs para mover o player
   [SerializeField] private GameInput gameInput;

   // Update() significa que essas linhas de códigos serão lidas a todo frame. Start() só no frame inicial.
   private void Update() {

      // Recebe o input da classe GameInput
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();

      // Passando os inputs para as três dimensões.
      Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

      // Muda a posição do personagem depenendo dos valores dos vetores, o deltaTime serve para padronizar a velocidade do personagem pelo tempo pressionado e não pelos frames, sem ele o personagem corre mais rápido quanto mais frames o jogo rodar, e o moveSpeed é justamente para controlar-mos a velocidade.
      transform.position += moveDir * moveSpeed * Time.deltaTime;

      // Se os vetores (x,y,z) for diferente de zero, quer dizer que o personagem está se movendo, o isWalking se turna true.
      isWalking = moveDir != Vector3.zero;

      // um atributo para fazer o personagem girar em seu próprio eixo enquanto se move.
      float rotateSpeed = 10f;

      // Rotação do personagem.
      // Lerp e Slerp = 
      transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
   }

   public bool IsWalking() {
      // O Player está andando?
      return isWalking;
   }
}
