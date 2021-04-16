using Merlin2.Actors.Items;
using Merlin2.Actors.State;
using Merlin2.Effects;
using Merlin2.Exceptions;
using Merlin2.Spells;
using Merlin2d.Game;
using Merlin2d.Game.Enums;
using Merlin2d.Game.Items;
using System;

namespace Merlin2.Actors
{
    public class Player : AbstractCharacter, IWizard, IVisitable

    {
        private int mana = 100;
        private int maxMana = 100;
        private SpellDirector spellDirector = new SpellDirector(SpellDataProvider.GetInstance());
        private bool isCasting = false;
        private IPlayerState state;
        private Backpack backpack = new Backpack(5);
        private int passCount = 0;
        private int castTime = 0;
        private int manaRegenTime = 0;
        private Message manaMsg;

        public Player(string name, int x, int y) : base(name, x, y)
        {
            state = new LivingState(this);
            backpack.AddItem(new SpeedPotion());
            backpack.ShiftRight();
        }

        public new void Die()
        {
            state = new DyingState(this);
        }

        public void Cast(ISpell spell)
        {
            if (mana - spell.GetCost() > 0)
            {
                ChangeMana(-spell.GetCost());
                spell.Cast();
            }
            else
            {
                Console.WriteLine("no mana");
            }
        }

        public override void Update()
        {
            base.Update();
            state.Update();
            GetWorld().RemoveMessage(manaMsg);
            manaMsg = new Message(GetMana().ToString(), GetX(), GetY() - 40, 16, Color.Blue, MessageDuration.Indefinite);
            GetWorld().AddMessage(manaMsg);
            if (mana < maxMana && manaRegenTime == 0)
            {
                ChangeMana(1);
                manaRegenTime = 15;
            }
            if (Input.GetInstance().IsKeyDown(Input.Key.D) && castTime == 0)
            {
                this.Cast(spellDirector.Build("fireball", this));
                castTime = 30;
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.E))
            {
                GetWorld().GetActors().ForEach(a =>
                {
                    if (a is IUsable && this.IntersectsWithActor(a))
                    {
                        ((IUsable)a).Use(this);
                    }
                });
            }

            if (passCount == 0)
            {
                GetWorld().GetActors().ForEach(a =>
                {
                    if (a is IItem && this.IntersectsWithActor(a))
                    {
                        try
                        {
                            backpack.AddItem((IItem)a);
                            a.RemoveFromWorld();
                        }
                        catch (FullInventoryException e)
                        {
                            //this blow your cpu if dont do something, so pass count is needed
                            GetWorld().AddMessage(new Message("Inventory is full", a.GetX(), a.GetY() - 20, 14, Color.Red, MessageDuration.Short));
                            passCount = 300;
                        }
                    }
                });
            }
            //prevent cpu blowing in AddMessage
            if (passCount > 0)
            {
                passCount--;
            }

            if (castTime > 0)
            {
                castTime--;
            }
            if (manaRegenTime > 0)
            {
                manaRegenTime--;
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.C))
            {
                IItem item = backpack.GetItem();
                IUsable usable = item as IUsable;
                if (item != null)
                {
                    usable.Use(this);
                    backpack.RemoveItem(item);
                }
            }

            if (Input.GetInstance().IsKeyPressed(Input.Key.Z))
            {
                backpack.ShiftLeft();
            }

            if (Input.GetInstance().IsKeyPressed(Input.Key.X))
            {
                backpack.ShiftRight();
            }

            if (GetHealth() <= 0)
            {
                base.Die();
            }
        }

        protected override void AddedToWorld()
        {
            GetWorld().ShowInventory(backpack);
            GetWorld().AddMessage(new Message("Fireball - D", 100, 30, 14, Color.White, MessageDuration.Indefinite));
            GetWorld().AddMessage(new Message("Switch items in BP - Z, X", 100, 60, 14, Color.White, MessageDuration.Indefinite));
            GetWorld().AddMessage(new Message("Use potion - C", 100, 90, 14, Color.White, MessageDuration.Indefinite));
            GetWorld().AddMessage(new Message("Move - ARROWS and SPACE", 100, 120, 14, Color.White, MessageDuration.Indefinite));
        }

        public void ChangeMana(int delta)
        {
            this.mana += delta;
        }

        public int GetMana()
        {
            return mana;
        }

        public bool IsCasting()
        {
            return isCasting;
        }

        public void ChangeCasting(bool cast)
        {
            this.isCasting = cast;
        }

        public void Accept(IVisitor damageDealer)
        {
            damageDealer.VisitPlayer(this);
        }
    }
}