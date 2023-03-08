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

   // Campo da Layer Mask dos Counter
   [SerializeField] private LayerMask countersLayerMask;
   private Vector3 lastInteractiveDir;
   private ClearCounter clearCounter;

   // Update() significa que essas linhas de códigos serão lidas a todo frame. Start() só no frame inicial.
   private void Update() {
      HandleMovement();
      HandleInteractions();
   }

   private void HandleMovement() {

      // Recebe o input da classe GameInput
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();

      // Passando os inputs para as três dimensões.
      Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

      // COLISÕES
      float playerHeight = 2f;
      float playerRadius = .7f;
      float moveDistance = moveSpeed * Time.deltaTime;

      bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

      // Verifica se pode andar
      if (!canMove){
         // Não pode se mover na direção moveDir
         // Pode se mover no eixo X?
         Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
         canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

         if(canMove) {
            // Se sim, então a direção se torna sómente o eixo X
            moveDir = moveDirX;
         } else {
            // Senão, pode se mover no eixo Z?
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

            if(canMove) {
               moveDir = moveDirZ;
            } else {
               // Senão, não faz nada...
            }
         }
      }

      // Verifica se ele pode ser mover, ou seja, não está colidindo com nada e então...
      // Muda a posição do personagem depenendo dos valores dos vetores, o deltaTime serve para padronizar a velocidade do personagem pelo tempo pressionado e não pelos frames, sem ele o personagem corre mais rápido quanto mais frames o jogo rodar, e o moveSpeed é justamente para controlar-mos a velocidade.
      if(canMove) {
         transform.position += moveDir * moveDistance;
      }

      // Se os vetores (x,y,z) for diferente de zero, quer dizer que o personagem está se movendo, o isWalking se turna true.
      isWalking = moveDir != Vector3.zero;

      // um atributo para fazer o personagem girar em seu próprio eixo enquanto se move.
      float rotateSpeed = 10f;

      // Rotação do personagem.
      // Lerp e Slerp = 
      transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
      
   }

   private void HandleInteractions() {
      // Criando novamente para não interferir no movimento/colisão
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();

      Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

      float interactiveDistance = 2f;

      // Salvando o objecto colidido antes de parar de se mover para manter a intereção mesmo sem se mover.
      if(moveDir != Vector3.zero) {
         lastInteractiveDir = moveDir;
      }

      // Raycast é outro tipo de colisão que cria um "raio" fino e detecta se há algo naquela direção.
      // Usamos o tipo detecta uma LayerMask, que é interessante por detectar mesmo se tiver algo na frente, tipo uma parede invisível.
      if(Physics.Raycast(transform.position, lastInteractiveDir, out RaycastHit raycastHit, interactiveDistance, countersLayerMask)){
         if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
            clearCounter.Interact();
         }
      }
   }

   public bool IsWalking() {
      // O Player está andando?
      return isWalking;
   }
}
