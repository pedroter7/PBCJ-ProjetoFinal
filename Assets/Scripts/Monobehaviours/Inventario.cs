using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{

    public Item carneItem;
    public Item moedaItem;
    public Item macaItem;
    public Item coxaItem;
    public Item queijoItem;
    public Item ovoItem;

    public GameObject slotPrefab;  //Objeto prefab do slot.
    public const int numSlot = 5; //Numero fixo de slots
    Image[] itemImages = new Image[numSlot]; //Array de imagens
    public Item[] Itens { get; private set; } //Array de itens
    GameObject[] slots = new GameObject[numSlot]; //Array de slots

    // Start is called before the first frame update
    void Start()
    {
        Itens = new Item[numSlot];
        CriaSlots();
        VerificaPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VerificaPrefs()
    {
        int carne = PlayerPrefs.GetInt(ItemKeys.carne);
        if (carne > 0)
        {
            carneItem.quantidade = carne;
            AddItem(carneItem);
        }

        int moeda = PlayerPrefs.GetInt(ItemKeys.moeda);
        if (moeda > 0)
        {
            print(moeda);
            moedaItem.quantidade = moeda;
            AddItem(moedaItem);
        }

        int coxa = PlayerPrefs.GetInt(ItemKeys.coxa);
        if (coxa > 0)
        {
            coxaItem.quantidade = coxa;
            AddItem(coxaItem);
        }

        int ovo = PlayerPrefs.GetInt(ItemKeys.ovo);
        if (ovo > 0)
        {
            ovoItem.quantidade = ovo;
            AddItem(ovoItem);
        }

        int maca = PlayerPrefs.GetInt(ItemKeys.maca);
        if (maca > 0)
        {
            macaItem.quantidade = maca;
            AddItem(macaItem);
        }

        int queijo = PlayerPrefs.GetInt(ItemKeys.queijo);
        if (queijo > 0)
        {
            queijoItem.quantidade = queijo;
            AddItem(queijoItem);
        }
    }

    //Cria a quantidade certa de slots, de acordo com a variavel numSlot.
    public void CriaSlots()
    {
        if (slotPrefab != null) //Verifica se o slotPrefab foi definido
        {
            for(int i = 0; i<numSlot; i++) //Loop para a quantidade de slots definida
            {
                GameObject newSlot = Instantiate(slotPrefab); //Instancia um slot
                newSlot.name = "ItemSlot_" + i;
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = newSlot;
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>(); 
            }
        }
    }

    //Adiciona um item no inventario
    public bool AddItem(Item item)
    {
        if (item.tipoItem == Item.TipoItem.POCAO_PODER)
        {
            return true;
        }
        for (int i = 0; i< Itens.Length; i++)
        {
            if (Itens[i] != null && Itens[i].tipoItem == item.tipoItem && item.empilhavel == true)
            {
                Itens[i].quantidade = Itens[i].quantidade + item.quantidade;
                Slot slotScript = slots[i].GetComponent<Slot>();
                Text qtdTexto = slotScript.QuantidadeTexto;
                qtdTexto.enabled = true;
                qtdTexto.text = Itens[i].quantidade.ToString();
                return true;
            }
            if (Itens[i] == null)
            {
                Itens[i] = Instantiate(item);
                Itens[i].quantidade = item.quantidade;
                Slot slotScript = slots[i].GetComponent<Slot>();
                Text qtdTexto = slotScript.QuantidadeTexto;
                qtdTexto.text = Itens[i].quantidade.ToString();
                itemImages[i].sprite = item.sprite;
                itemImages[i].enabled = true;
                return true;
            }
        }
        return false;
    }

}
