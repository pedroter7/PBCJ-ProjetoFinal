using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Perambular : MonoBehaviour
{

    public float VelocidadePerseguicao;                 // Velocidade do personagem NPC na area de spot
    public float VelocidadePerambular;                  // Velocidade do personagem NPC perambulando
    public float IntervaloMudancaDirecao;               // Intervalo para alterar a direcao do personagem NPC
    public bool PerseguePlayer;                         // Indica se o personagem NPC persegue o player
    private float _velocidadeCorrente;                  // Velocidade atual do personagem NPC
    private Coroutine _moverCoroutine;
    private Rigidbody2D _rb2D;                          // Armazena o componente Rigidbody2D
    private Animator _animator;                         // Armazena o componente animator
    private Transform _alvoTransform;                   // Armazena o componente Transform do alvo do personagem NPC
    private Vector3 _posicaoFinal;
    private float _anguloAtual;                         // Angulo atribuido
    CircleCollider2D _circleCollider;                   // Armazena o componente de spot

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _velocidadeCorrente = VelocidadePerambular;
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(_rb2D.position, _posicaoFinal, Color.red);
    }

    private void OnDrawGizmos()
    {
        if (_circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, _circleCollider.radius);
        }
    }

    //Gera movimentacao para o caractere.
    public IEnumerator RotinaPerambular()
    {
        while (true)
        {
            EscolheNovoPontoFinal();
            if (_moverCoroutine != null)
            {
                StopCoroutine(_moverCoroutine);
            }
            _moverCoroutine = StartCoroutine(Mover(_rb2D, _velocidadeCorrente));
            yield return new WaitForSeconds(IntervaloMudancaDirecao);
        }
    }

    //Calcula proximo ponto ao qual o caractere ira caminhar
    private void EscolheNovoPontoFinal()
    {
        _anguloAtual += Random.Range(0, 360);
        _anguloAtual = Mathf.Repeat(_anguloAtual, 360);
        _posicaoFinal += Vector3ParaAngulo(_anguloAtual);
    }

    private Vector3 Vector3ParaAngulo(float anguloEntradaGraus)
    {
        var anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);
    }

    //Movimentacao do caractere
    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - _posicaoFinal).sqrMagnitude;
        while (distanciaFaltante > float.Epsilon)
        {
            if (_alvoTransform != null)
            {
                _posicaoFinal = _alvoTransform.position;
            }
            if (rbParaMover != null)
            {
                _animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, _posicaoFinal, velocidade * Time.deltaTime);
                _rb2D.MovePosition(novaPosicao);
                distanciaFaltante = (transform.position - _posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        _animator.SetBool("Caminhando", false);
    }

    //Trigger de colisao com o player para criar perseguicao.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PerseguePlayer)
        {
            _velocidadeCorrente = VelocidadePerseguicao;
            _alvoTransform = collision.gameObject.transform;
            if (_moverCoroutine != null)
            {
                StopCoroutine(_moverCoroutine);
            }
            _moverCoroutine = StartCoroutine(Mover(_rb2D, _velocidadeCorrente));
        }
    }
    //Quando sai do trigger, muda animação e velocidade do caractere
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("Caminhando", false);
            _velocidadeCorrente = VelocidadePerambular;
            if (_moverCoroutine != null)
            {
                StopCoroutine(_moverCoroutine);
            }
            _alvoTransform = null;
        }
    }

}
