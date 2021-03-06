using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script responsavel pelas caracteristicas do player, como manusear colisoes e gerenciar sua vida, healthbar e itens pegos.
/// </summary>
public class Player : Caractere
{
    public Inventario inventarioPrefab; //Aramazena o prefab do inventario do player
    public Inventario Inventario { get; private set; } //Armazena o inventario instanciado
    public HealthBar healthBarPrefab;  //Referencia do prefab HealthBar
    HealthBar healthBar; //Armazena a healthBar instanciada
    public PontosDano pontosDano; // Tem o valor da vida do objeto

    private static bool _estaComBonus = false;  // Variavel para dar bonus ao player nas instanciacoes

    // Controla a arma que o player esta usando
    public static readonly string WEAPON_PLAYER_PREFS_KEY = "WEAPON_PLAYER";
    public static readonly int WEAPON_PLAYER_ARMA = 0;
    public static readonly int WEAPON_PLAYER_ESPADA = 1;
    public static int WeaponAtual = -1; // Interface com outros scripts

    //Ao iniciar o player, configura seus componentes
    private void Start()
    {
        Inventario = Instantiate(inventarioPrefab);

        float saudePlayerPrefs = PlayerPrefs.GetFloat(ItemKeys.playerHealth);
        string message = string.Format("Saude do prefs = " + saudePlayerPrefs);
        print(message);
        if(saudePlayerPrefs > 0)
        {
            string printMessage = string.Format("Saude inicial = " + saudePlayerPrefs);
            print(printMessage);
            pontosDano.valor = saudePlayerPrefs;
        }
        else
        {
            pontosDano.valor = InicioPontosDano;
        }

        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        if (_estaComBonus) BonusRecebido();
        if (WeaponAtual == -1)
        {
            WeaponAtual = WEAPON_PLAYER_ARMA;
        }
    }

    public float GetHealth()
    {
        float value = pontosDano.valor;
        return value;
    }

    //Reseta o player, sua vida e seu inventario
    public override void ResetCaractere()
    {
        Inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = InicioPontosDano;
    }

    //Mata o player, destruindo seu gameObject
    public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(Inventario.gameObject);
        SceneManager.LoadScene("TelaDerrota");
    }

    //Responsavel por dar o dano no player, dando uma animacaozinha breve
    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if (pontosDano.valor <= float.Epsilon)
            {
                KillCaractere();
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else break;
        }
    }

    //Reconhece trigger de contato com coletaveis, tratando a coleta para cada caso.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            Item danoObjeto = collision.gameObject.GetComponent<Consumable>().item;
            if (danoObjeto!=null)
            {
                bool DeveDesaparecer = false;
                switch (danoObjeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        if (DeveDesaparecer)
                        {
                            int playerMoedas = PlayerPrefs.GetInt(ItemKeys.moeda);
                            PlayerPrefs.SetInt(ItemKeys.moeda, playerMoedas + 1);
                        }
                        break;
                    case Item.TipoItem.HEALTH:
                        DeveDesaparecer = AjusteDanoObjeto(danoObjeto.quantidade);
                        break;
                    case Item.TipoItem.MACA:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        if (DeveDesaparecer)
                        {
                            int playerMacas = PlayerPrefs.GetInt(ItemKeys.maca);
                            PlayerPrefs.SetInt(ItemKeys.maca, playerMacas + 1);
                        }
                        break;
                    case Item.TipoItem.COXA:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        if (DeveDesaparecer)
                        {
                            int playerCoxas = PlayerPrefs.GetInt(ItemKeys.coxa);
                            PlayerPrefs.SetInt(ItemKeys.coxa, playerCoxas + 1);
                        }
                        break;
                    case Item.TipoItem.CARNE:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        if (DeveDesaparecer)
                        {
                            int playerCarnes = PlayerPrefs.GetInt(ItemKeys.carne);
                            PlayerPrefs.SetInt(ItemKeys.carne, playerCarnes + 1);
                        }
                        break;
                    case Item.TipoItem.QUEIJO:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        if (DeveDesaparecer)
                        {
                            int playerQueijos = PlayerPrefs.GetInt(ItemKeys.queijo);
                            PlayerPrefs.SetInt(ItemKeys.queijo, playerQueijos + 1);
                        }
                        break;
                    case Item.TipoItem.OVO:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        if (DeveDesaparecer)
                        {
                            int playerOvos = PlayerPrefs.GetInt(ItemKeys.ovo);
                            PlayerPrefs.SetInt(ItemKeys.ovo, playerOvos + 1);
                        }
                        break;
                    case Item.TipoItem.POCAO_PODER:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        _estaComBonus = true;
                        BonusRecebido();
                        break;
                    case Item.TipoItem.ESPADA:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        ColetaEspada();
                        break;
                    case Item.TipoItem.FRANGO_FINAL:
                        DeveDesaparecer = Inventario.AddItem(danoObjeto);
                        GameOver();
                        break;
                    default:
                        break;
                }
                if (DeveDesaparecer) collision.gameObject.SetActive(false);
            }
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("TelaVitoria");
    }

    private void ColetaEspada()
    {
        WeaponAtual = WEAPON_PLAYER_ESPADA;
    }

    /// <summary>
    /// Da bonus de poder e de vida para o player
    /// </summary>
    private void BonusRecebido()
    {
        // 2x vida
        pontosDano.valor = 2 * MaxPontosDano;
    }

    //Ajusta a vida do usuario com coletaveis de sa?de
    private bool AjusteDanoObjeto(int quantidade)
    {
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            return true;
        }
        else return false;
    }
}
