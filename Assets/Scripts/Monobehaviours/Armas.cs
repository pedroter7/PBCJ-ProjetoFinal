using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsavel por controlar e animar a arma
/// </summary>
[RequireComponent(typeof(Animator))]
public class Armas : MonoBehaviour
{

    public GameObject MunicaoPrefab;                // Armazena o prefab da municao
    private static List<GameObject> _municaoPool;   // Pool de municao
    public int TamanhoPool;                         // Tamanho do pool
    public float VelocidadeArma;                    // Velocidade da municao

    private bool _atirando;                         // Se a arma esta atirando

    [HideInInspector]
    public Animator animator;

    private Camera _cameraLocal;

    private float _slopePositivo;
    private float _slopeNegativo;

    enum Quadrante //Quadrante no qual a arma dispara
    {
        Leste,
        Sul,
        Oeste,
        Norte
    }

    //adiciona municao pro player
    public void Awake()
    {
        if (_municaoPool == null)
        {
            _municaoPool = new List<GameObject>();
        }
        for (int i = 0; i < TamanhoPool; ++i)
        {
            GameObject municaoO = Instantiate(MunicaoPrefab);
            municaoO.SetActive(false);
            _municaoPool.Add(municaoO);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _atirando = false;
        _cameraLocal = Camera.main;
        Vector2 abaixoEsquerda = _cameraLocal.ScreenToWorldPoint(new Vector2(0,0));
        Vector2 acimaDireita = _cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 acimaEsquerda = _cameraLocal.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 abaixoDireita = _cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        _slopePositivo = PegaSlope(abaixoEsquerda, acimaDireita);
        _slopeNegativo = PegaSlope(acimaEsquerda, abaixoDireita);
    }

    //Direciona o trajetil da arma para acima slope positivo
    private bool AcimaSlopePositivo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = _cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (_slopePositivo*posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (_slopePositivo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    //Direciona o trajetil da arma para acima slope negativo
    private bool AcimaSlopeNegativo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = _cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (_slopeNegativo*posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (_slopeNegativo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    //Pega o quadrante correto para disparar o trajetil
    private Quadrante PegaQuadrante()
    {
        Vector2 posicaoMouse = Input.mousePosition;
        Vector2 posicaoPlayer = transform.position;
        bool acimaSlopePositivo = AcimaSlopePositivo(posicaoMouse);
        bool acimaSlopeNegativo = AcimaSlopeNegativo(posicaoMouse);
        if (!acimaSlopePositivo && acimaSlopeNegativo)
        {
            return Quadrante.Leste;
        } 
        else if (!acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Sul;
        } 
        else if (acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Oeste;
        } 
        else
        {
            return Quadrante.Norte;
        }
    }

    //Atualiza o estado da animação da arma
    private void UpdateEstado()
    {
        if (_atirando)
        {
            Vector2 vetorQuadrante;
            Quadrante quadrante = PegaQuadrante();
            switch (quadrante)
            {
                case Quadrante.Leste:
                    vetorQuadrante = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrante.Sul:
                    vetorQuadrante = new Vector2(0.0f, -1.0f);
                    break;
                case Quadrante.Oeste:
                    vetorQuadrante = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrante.Norte:
                    vetorQuadrante = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    vetorQuadrante = new Vector2(0.0f, 0.0f);
                    break;
            }
            animator.SetBool("Atirando", true);
            animator.SetFloat("AtiraX", vetorQuadrante.x);
            animator.SetFloat("AtiraY", vetorQuadrante.y);
            _atirando = false;
        } 
        else
        {
            animator.SetBool("Atirando", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _atirando = true;
            DisparaMunicao();
        }

        UpdateEstado();
    }

    //Pega o slope de acordo com a posicao passada
    private float PegaSlope(Vector2 ponto1, Vector2 ponto2)
    {
        return (ponto2.y - ponto1.y)/(ponto2.x - ponto1.x);
    }

    //Faz o spawn da municao na cena
    public GameObject SpawnMunicao(Vector3 posicao)
    {
        foreach (var municao in _municaoPool)
        {
            if (municao.activeSelf == false)
            {
                municao.SetActive(true);
                municao.transform.position = posicao;
                return municao;
            }
        }

        return null;
    }

    //Responsavel por capturar o disparo da arma
    private void DisparaMunicao()
    {
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject municao = SpawnMunicao(transform.position);
        if (municao != null)
        {
            Arco arcoScript = municao.GetComponent<Arco>();
            float duracaoTrajetoria = 1.0f/VelocidadeArma;
            StartCoroutine(arcoScript.ArcoTrajetoria(posicaoMouse, duracaoTrajetoria));
        }
    }

    private void OnDestroy()
    {
        _municaoPool = null;
    }

}
