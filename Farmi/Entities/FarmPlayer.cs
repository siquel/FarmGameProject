﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmi.Entities.Components;
using Farmi.KahvipaussiEngine.Khv.Game.Collision;
using Farmi.World;
using Khv.Engine;
using Khv.Engine.Structs;
using Khv.Game;
using Khv.Game.Collision;
using Khv.Game.GameObjects;
using Khv.Gui.Components.BaseComponents;
using Khv.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenTK.Input;

namespace Farmi.Entities
{
    public sealed class FarmPlayer : Player
    {
        #region Vars
        private readonly FarmWorld world;
        private GameObject closest;
        private const float speed = 5f;


        private InputController controller;
        private InputControlSetup defaultInputSetup;
        private InputControlSetup shopInputSetup;
        #endregion

        #region Properties
        public bool CouldInteract
        {
            get
            {
                return closest != null;
            }
        }
        #endregion

        public FarmPlayer(KhvGame game, FarmWorld world, PlayerIndex index = PlayerIndex.One)
            : base(game, index)
        {
            defaultInputSetup = new InputControlSetup();
            shopInputSetup = new InputControlSetup();
            
            this.world = world;
            Position = new Vector2(500, 200);

            Size = new Size(32, 32);

            Collider = new BoxCollider(world, this,
                new BasicObjectCollisionQuerier(),
                new BasicTileCollisionQuerier());

            Collider.OnCollision += Collider_OnCollision;

        }

        void Collider_OnCollision(object sender, CollisionEventArgs result)
        {
            //Console.WriteLine(sender);
        }

        public void Initialize()
        {
            controller = new InputController(game.InputManager);
            controller.ChangeSetup(defaultInputSetup);
            InitDefaultSetup();

            Components.Add(new ExclamationMarkDrawer(game, this));
        }


        private void InitDefaultSetup()
        {
            var keymapper = defaultInputSetup.Mapper.GetInputBindProvider<KeyInputBindProvider>();
            keymapper.Map(new KeyTrigger("Move left", Keys.A, Keys.Left), (triggered, args) => MotionEngine.GoalVelocityX = VelocityFunc(args, -speed));
            keymapper.Map(new KeyTrigger("Move right", Keys.D, Keys.Right), (triggered, args) => MotionEngine.GoalVelocityX = VelocityFunc(args, speed));
            keymapper.Map(new KeyTrigger("Move up", Keys.W, Keys.Up), (triggered, args) => MotionEngine.GoalVelocityY = VelocityFunc(args, -speed));
            keymapper.Map(new KeyTrigger("Move down", Keys.S, Keys.Down), (triggered, args) => MotionEngine.GoalVelocityY = VelocityFunc(args, speed));
            keymapper.Map(new KeyTrigger("Interact", Keys.Space), (triggered, args) => TryInteract(args));

            var padmapper = defaultInputSetup.Mapper.GetInputBindProvider<PadInputBindProvider>();
            padmapper.Map(new ButtonTrigger("Move left", Buttons.LeftThumbstickLeft, Buttons.DPadLeft), (triggered, args) => MotionEngine.GoalVelocityX = -speed);
            padmapper.Map(new ButtonTrigger("Move right", Buttons.LeftThumbstickRight, Buttons.DPadRight), (triggered, args) => MotionEngine.GoalVelocityX = speed);
            padmapper.Map(new ButtonTrigger("Move up", Buttons.LeftThumbstickUp, Buttons.DPadUp), (triggered, args) => MotionEngine.GoalVelocityY = -speed);
            padmapper.Map(new ButtonTrigger("Move down", Buttons.LeftThumbstickDown, Buttons.DPadDown), (triggered, args) => MotionEngine.GoalVelocityX = speed);
        }

        private void TryInteract(InputEventArgs args)
        {
            if (args.State != InputState.Released)
            {
                return;
            }

            if (closest == null)
            {
                return;
            }

            (closest.Components.GetComponent(c => c is IInteractionComponent) as IInteractionComponent).Interact(this);

        }

        private float VelocityFunc(InputEventArgs args, float src)
        {
            if (args.State == InputState.Released)
            {
                return 0;
            }

            return src;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MotionEngine.Update(gameTime);
            Collider.Update(gameTime);

            closest = world.GetNearestInteractable(this, new Padding(10, 5));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            #region test
            /*  GameObject source = this;
            Padding radius = new Padding(10, 5);

            Rectangle r = new Rectangle((int)(source.Position.X - radius.Left), (int)(source.Position.Y - radius.Top), radius.Left + radius.Right , radius.Top + radius.Bottom * 2);
            spriteBatch.Draw(KhvGame.Temp, r, Color.Red);*/
            #endregion
            spriteBatch.Draw(KhvGame.Temp, new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height), Color.Turquoise);
            base.Draw(spriteBatch);
        }
    }
}
