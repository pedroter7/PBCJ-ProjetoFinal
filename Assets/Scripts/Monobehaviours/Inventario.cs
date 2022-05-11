using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
