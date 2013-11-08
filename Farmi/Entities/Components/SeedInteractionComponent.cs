﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Khv.Game.GameObjects;
using Microsoft.Xna.Framework;

namespace Farmi.Entities.Components
{
    public class SeedInteractionComponent : InteractionComponent
    {
        private readonly Seed owner;

        public SeedInteractionComponent(Seed owner)
        {
            this.owner = owner;
        }

        protected override void DoInteract(GameObject with, GameTime gameTime)
        {
            CropSpot spot = with as CropSpot;
            if (spot == null)
                return;
            // onko spotilla maaperää
            if (spot.Ground == null)
            {
                IsInteracting = false;
                return;
            }

            // on maaperä, niin istutetaan kasvi
            spot.Ground.Plant(owner);

            IsInteracting = false;
        }

        public override bool CanInteract(GameObject with)
        {
            return with is CropSpot;
        }
    }
}
