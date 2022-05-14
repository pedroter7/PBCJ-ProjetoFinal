using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPlayer : MonoBehaviour
{

    public float VelocidadeMovimento = 3.0f; //Equivale ao momento (impulso) dado ao player.
    Vector2 Movimento = new Vector2(); //Detectar movimento pelo teclado.

    Rigidbody2D rb2D; //Guarda o componente CorpoRigido do player.
    Animator animator; //Guarda o componente Animator do player

    private int _armaAtual = -1;     // Guarda a arma atual do player para controlar animacao

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEstado();
    }

    //Respons�vel por deixar o movimento mais suave
    private void FixedUpdate()
    {
        MoveCaractere();
    }

    //Atualiza a din�mica do movimento.
    private void MoveCaractere()
    {
        Movimento.x = Input.GetAxisRaw("Horizontal");
        Movimento.y = Input.GetAxisRaw("Vertical");
        Movimento.Normalize();
        rb2D.velocity = Movimento * VelocidadeMovimento;
    }

    private void UpdateEstado()
    {
        if (Mathf.Approximately(Movimento.x, 0) && (Mathf.Approximately(Movimento.y, 0)))
        {
            animator.SetBool("Caminhando", false);
        }
        else
        {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("DirX", Movimento.x);
        animator.SetFloat("DirY", Movimento.y);
        if (Player.WeaponAtual != _armaAtual)
        {
            _armaAtual = Player.WeaponAtual;
            MudarAnimArma();
        }
    }

    private void MudarAnimArma()
    {
        animator.SetInteger("Weapon", _armaAtual);
    }

}
