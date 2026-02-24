using System.Collections.Generic;
using UnityEngine;

namespace Solution {
    public class Inventory : MonoBehaviour
    {

        public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();

        // เพิ่มไอเท็ม
        public void AddItem(ItemData item, int amount)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] += amount;
            }
            else
            {
                inventory.Add(item, amount);
            }
            Debug.Log("Added " + amount + " " +  item + "Total : " + inventory[item]); 
        }

        // ลบไอเท็ม
        public void UseItem(ItemData item, int amount)
        {
            if (HasItem(item,amount))
            {
                inventory[item] -= amount;
                if (inventory[item] <= 0)
                {
                    inventory.Remove(item);
                    Debug.Log($"Remove {item.ItemName} form Inventory");
                }
                else
                {
                    Debug.Log($"Remove {item.ItemName}  " + amount + "total in Inventory");
                }
            }
            else
            {
                Debug.Log("Cannot remove " + item.ItemName + " not found 404");
            }
        }
        public bool HasItem(ItemData item, int amount)
        {
            return inventory.ContainsKey(item) && inventory[item] >= amount;
        }
        // ตรวจสอบจำนวนไอเท็ม
        public int GetItemCount(ItemData item)
        {
           
            return 0;
        }

        // แสดงรายการทั้งหมดในคลัง
        public void PrintInventory()
        {
           
        }
    }
}

