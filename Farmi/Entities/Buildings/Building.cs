﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Khv.Engine.Structs;
using Khv.Game.Collision;
using Khv.Game.GameObjects;
using Khv.Engine;
using SerializedDataTypes.MapObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Khv.Maps.MapClasses.Processors;
using Farmi.Repositories;
using Farmi.Datasets;

namespace Farmi.Entities.Buildings
{
    internal sealed class Building : DrawableGameObject
    {
        #region Vars
        private Texture2D texture;
        private Color color;
        #endregion

        /// <summary>
        /// Muodostin kun ladataan suoraan kartasta olio.
        /// </summary>
        public Building(KhvGame game, MapObjectArguments args)
            : base(game)
        {
            // Pitäs ladata db:stä tietoja jo tässä

            TestInitialize(args);
        }
        public Building(KhvGame game)
            : base(game)
        {
            // Pitäs ladata db:stä tietoja jo tässä

            TestInitialize(null);
        }

        // Testi metodi initille.
        private void TestInitialize(MapObjectArguments args)
        {
            if (args == null)
            {
                position = Vector2.Zero;
            }
            else
            {
                position = args.Origin;
            }

            // Hakee tiedot repoista.
            RepositoryManager repositoryManager = game.Components.First(c => c is RepositoryManager) as RepositoryManager;
            BuildingDataset dataset = repositoryManager.GetDataSet<BuildingDataset>(s => s.Name == args.SerializedData.valuepairs[1].Value);

            if (!string.IsNullOrEmpty(dataset.Name))
            {
                texture = game.Content.Load<Texture2D>(@"Buildings\" + dataset.AssetName);
                size = dataset.Size;
                color = Color.White;
            }
            else
            {
                size = new Size(128, 64);
                texture = KhvGame.Temp;
                color = Color.Brown;
            }

            Collider = new BoxCollider(null, this);
        }

        public void InitializeFromData(string datasetName)
        {
            // Alustaa otuksen datasta tässä.
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Collider.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height), color);   
        }
    }
}
