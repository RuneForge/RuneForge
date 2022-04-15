﻿using Microsoft.Xna.Framework.Content;

namespace RuneForge.Game.Entities.Interfaces
{
    public interface IComponentPrototypeReader
    {
        public ComponentPrototype ReadComponentPrototype(ContentReader contentReader);
    }

    public interface IComponentPrototypeReader<TComponentPrototype> : IComponentPrototypeReader
        where TComponentPrototype : ComponentPrototype
    {
        public TComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader);
    }
}
