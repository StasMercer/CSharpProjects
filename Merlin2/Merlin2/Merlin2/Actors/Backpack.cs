using Merlin2.Exceptions;
using Merlin2d.Game.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Merlin2.Actors
{
    public class Backpack : IInventory
    {
        private int capacity = 5;

        private IItem[] items;

        public Backpack(int capacity)
        {
            this.capacity = capacity;
            items = new IItem[capacity];
        }

        public void AddItem(IItem item)
        {
            if (this.Count() == capacity)
            {
                throw new FullInventoryException();
            }
            for (int i = 0; i < capacity; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    break;
                }
            }
            Console.WriteLine($"items in bp = {this.Count()}");
        }

        public int GetCapacity()
        {
            return capacity;
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (items[i] != null)
                {
                    yield return items[i];
                }
                else
                {
                    yield break;
                }
            }
        }

        public IItem GetItem()
        {
            return items[0];
        }

        public void RemoveItem(IItem item)
        {
            if (items.Contains(item))
            {
                List<IItem> itemsList = items.ToList();
                itemsList.Remove(item);
                itemsList.CopyTo(items, 0);
            }
        }

        public void RemoveItem(int index)
        {
            if (index >= capacity || index < 0) return;
            List<IItem> itemsList = items.ToList();
            itemsList.RemoveAt(index);
            itemsList.CopyTo(items, 0);
        }

        public void ShiftLeft()
        {
            this.Skip(1).Append(items[0]).ToArray().CopyTo(items, 0);
        }

        public void ShiftRight()
        {
            this.SkipLast(1).Prepend(this.Last()).ToArray().CopyTo(items, 0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (items[i] != null)
                {
                    yield return items[i];
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}