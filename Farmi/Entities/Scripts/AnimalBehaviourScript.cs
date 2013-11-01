﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Khv.Scripts.CSharpScriptEngine.ScriptClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Farmi.Entities.Animals;
using Khv.Engine;

namespace Farmi.Entities.Scripts
{
    public abstract class AnimalBehaviourScript : IScript
    {
        #region Vars
        protected readonly KhvGame game;
        protected readonly Animal owner;
        #endregion

        public AnimalBehaviourScript(KhvGame game, Animal owner)
        {
            this.game = game;
            this.owner = owner;
        }

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
