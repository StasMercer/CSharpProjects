using Merlin2.Actors;
using Merlin2.Actors.Characters;
using Merlin2.Actors.Items;
using Merlin2.Commands;
using Merlin2.Factories;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;

namespace Merlin2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GameContainer container = new GameContainer("Magic", 1024, 750, true);
            container.SetEndGameMessage(new Message("You Loose!", container.GetWidth() / 2, container.GetHeight() / 2), false);
            container.SetEndGameMessage(new Message("You Win!", container.GetWidth() / 2, container.GetHeight() / 2), true);
            container.AddWorld("resources/maps/map.tmx");
            container.AddWorld("resources/maps/map01.tmx");
            container.AddWorld("resources/maps/map02.tmx");
            IWorld world1 = container.GetWorld(0);
            IWorld world2 = container.GetWorld(1);
            IWorld world3 = container.GetWorld(2);

            ActorFactory actorFactory = new ActorFactory();


            Action<IWorld> createRelations = world =>
            {
                List<IActor> doors = world.GetActors().FindAll(x => x is Door);
                List<IActor> switches = world.GetActors().FindAll(x => x is Switch);
                List<IActor> plates = world.GetActors().FindAll(x => x is PreassurePlate);
                foreach (Switch sw in switches)
                {
                    foreach (Door d in doors)
                    {
                        if (d.GetName().Split("-")[1] == sw.GetName().Split("-")[1])
                        {
                            sw.Subcribe(d);
                        }
                    }
                }
                foreach (PreassurePlate pl in plates)
                {
                    foreach (Door d in doors)
                    {
                        if (d.GetName().Split("-")[1] == pl.GetName().Split("-")[1])
                        {
                            pl.Subcribe(d);
                        }
                    }
                }
            };

            Action<IWorld> setTargets = world =>
            {
                List<IActor> enemies = world.GetActors().FindAll(a => a is IEnemy);
                IActor player = world.GetActors().Find(a => a is Player);
                foreach (IEnemy e in enemies)
                {
                    e.SetTarget((AbstractCharacter)player);
                }
            };

            for (int i = 0; i < container.GetWorldCount(); i++)
            {
                IWorld w = container.GetWorld(i);
                w.SetFactory(actorFactory);
                w.SetPhysics(new Gravity());
                w.AddInitAction(createRelations);
                w.AddInitAction(setTargets);
                w.SetEndCondition(world =>
                {
                    
                    if (world.GetActors().FindAll(a => a is SkullKiller || a is Villian || a is Bull).Count == 0)
                    {
                        return MapStatus.Finished;
                    }
                    if (world.GetActors().FindAll(a => a is Player).Count == 0)
                    {
                        return MapStatus.Failed;
                    }

                    return MapStatus.Unfinished;
                });
            }
  
            /*Action<IWorld> setCamera = world =>
            {
                world.CenterOn(world.GetActors().Find(a => a.GetName() == "player"));
            };*/

           /* world1.SetEndCondition(w =>
            {
                //debug
                return MapStatus.Finished;
                if (w.GetActors().FindAll(a => a is SkullKiller).Count == 0)
                {
                    return MapStatus.Finished;
                }
                if (w.GetActors().FindAll(a => a is Player).Count == 0)
                {
                    return MapStatus.Failed;
                }

                return MapStatus.Unfinished;
            });

            world2.SetEndCondition(w =>
            {
                return MapStatus.Finished;
                if (w.GetActors().FindAll(a => a is Villian).Count == 0)
                {
                    return MapStatus.Finished;
                }
                if (w.GetActors().FindAll(a => a is Player).Count == 0)
                {
                    return MapStatus.Failed;
                }

                return MapStatus.Unfinished;
            });*/
            
            
            /*world.AddInitAction(setCamera);*/

            container.Run();
        }
    }
}