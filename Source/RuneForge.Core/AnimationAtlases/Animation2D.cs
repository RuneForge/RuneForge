using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework;

namespace RuneForge.Core.AnimationAtlases
{
    public class Animation2D : ICloneable
    {
        private readonly ReadOnlyCollection<AnimationRegion2D> m_animationRegions;
        private TimeSpan m_elapsedTime;
        private int m_currentIndex;

        public string Name { get; }

        public bool Looped { get; }
        public bool Reversed { get; }

        public bool Completed { get; private set; }

        public bool CanBeUpdated { get; }

        public Animation2D(string name, IList<AnimationRegion2D> animationRegions, bool looped, bool reversed, bool canBeUpdated)
        {
            m_animationRegions = animationRegions != null
                ? new ReadOnlyCollection<AnimationRegion2D>(animationRegions) : throw new ArgumentNullException(nameof(animationRegions));
            m_elapsedTime = TimeSpan.Zero;
            m_currentIndex = 0;

            Name = !string.IsNullOrEmpty(name)
                ? name : throw new ArgumentException("Name of the animation can not be null or empty.", nameof(name));

            Looped = looped;
            Reversed = reversed;

            Completed = false;

            CanBeUpdated = canBeUpdated;
        }
        public Animation2D(string name, IList<AnimationRegion2D> animationRegions, bool looped, bool reversed)
            : this(name, animationRegions, looped, reversed, false)
        {
        }
        public Animation2D(string name, IList<AnimationRegion2D> animationRegions)
            : this(name, animationRegions, false, false, false)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!CanBeUpdated)
                throw new InvalidOperationException("You can not update a read-only animation. Clone it to get an updateable copy.");

            if (Completed)
                return;

            m_elapsedTime += gameTime.ElapsedGameTime;

            AnimationRegion2D currentAnimationRegion = GetCurrentAnimationRegion();
            if (m_elapsedTime < currentAnimationRegion.FrameTime)
                return;

            m_elapsedTime -= currentAnimationRegion.FrameTime;
            if (m_currentIndex < m_animationRegions.Count - 1)
                m_currentIndex++;
            else
            {
                m_elapsedTime = TimeSpan.Zero;
                if (Looped && m_currentIndex == m_animationRegions.Count - 1)
                    m_currentIndex = 0;
                else
                    Completed = true;
            }
        }

        public void Reset()
        {
            if (!CanBeUpdated)
                throw new InvalidOperationException("You can not reset a read-only animation. Clone it to get an updateable copy.");

            m_elapsedTime = TimeSpan.Zero;
            m_currentIndex = 0;

            Completed = false;
        }

        public AnimationRegion2D GetCurrentAnimationRegion()
        {
            return m_animationRegions[GetAdjustedIndex(m_currentIndex)];
        }

        public Animation2D CreateUpdateableCopy()
        {
            return new Animation2D(Name, m_animationRegions, Looped, Reversed, true)
            {
                m_elapsedTime = m_elapsedTime,
                m_currentIndex = m_currentIndex,

                Completed = Completed,
            };
        }

        private int GetAdjustedIndex(int index)
        {
            return !Reversed ? index : m_animationRegions.Count - 1 - index;
        }

        #region ICloneable Interface Implementation

        public object Clone()
        {
            return CreateUpdateableCopy();
        }

        #endregion
    }
}
