﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmi.Datasets;
using Farmi.Entities.Buildings;
using Farmi.Entities.Components;
using Khv.Engine;
using Khv.Game.Collision;
using Khv.Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Farmi.Screens;

namespace Farmi.Entities
{
    class Door : DrawableGameObject
    {
        #region Vars
        private Texture2D texture;
        private Teleport teleport;
        #endregion

        #region Properties
        public Building OwningBuilding
        {
            get; 
            private set;
        }
        #endregion

        public Door(KhvGame game, Building owningBuilding, DoorDataset doorDataset, string mapContainedIn)
            : base(game)
        {
            OwningBuilding = owningBuilding;
            Position = owningBuilding.Position + doorDataset.Position;
            Size = doorDataset.Size;

            GameplayScreen screen = game.GameStateManager.Current as GameplayScreen;
            teleport = new Teleport(game, doorDataset.TeleportDataset, mapContainedIn);

            Collider = new BoxCollider(null, this);
            DoorInteractionComponent c = new DoorInteractionComponent();
            c.OnInteraction += () => Console.WriteLine("OnInteraction");
            c.OnInteractionBegin += () => Console.WriteLine("OnInteractionBegin");
            c.OnInteractionFinished += () => Console.WriteLine("OnInteractionFinished");
            Components.Add(c);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(KhvGame.Temp, new Rectangle((int) position.X, (int) position.Y, size.Width, size.Height), Color.Black);
        }

    }
}
