﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmi.Entities.Components;
using Farmi.Entities.Items;
using Farmi.Entities.Scripts;
using Khv.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Farmi.dat.scripts
{
    internal class SeedBehaviour : ToolBehaviourScript
    {
        internal SeedInteractionComponent seedInteractionComponent;
        public SeedBehaviour(KhvGame game, Tool owner) : base(game, owner)
        {
            seedInteractionComponent = new SeedInteractionComponent(owner);
            owner.Components.Add(seedInteractionComponent);
        }

        protected override void HookEvents()
        {
            base.HookEvents();
        }


        public override void Update(GameTime gameTime)
        {
                
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
