using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField]
    private UIShop uiShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponent<IShopCustomer>();

        if(collision.CompareTag("Player"))
        {
            PauseMenu.Instance.canPause = false;
            uiShop.Show(shopCustomer);
            CursorController.instance.ActivateMenuCursor();
        }

        if (shopCustomer != null)
        {
            collision.GetComponentInChildren<Weapon>().enabled = false;
            AudioManager.Instance.PlayMusicWithCrossFade(GameAssets.i.secondaryMusic, 1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        IShopCustomer shopCustomer = collision.GetComponent<IShopCustomer>();

        if (shopCustomer != null)
        {
            PauseMenu.Instance.canPause = true;
            collision.GetComponentInChildren<Weapon>().enabled = true;
            uiShop.Hide();
            AudioManager.Instance.PlayMusic(GameController.staticMusic);
            CursorController.instance.ActivateBullseyeClear();
        }
    }
}
