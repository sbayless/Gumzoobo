using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BubbleGame
{
    class Drone : Enemy
    {
        int elapsedPauseTime;
        int pauseStart = 4000;
        int pauseEnd = 4500;


        public Drone(Vector2 startPosition, Direction currentDirection)
            : base(startPosition, "Drone", new Point(35,51), new Point(17, 25), 4, new Vector2(17f, 25f), currentDirection)
        {
            // add this enemies animations
            AddAnimation(new Animation("EnemyRight", 1, 4, 100, true, SpriteEffects.FlipHorizontally));
            AddAnimation(new Animation("EnemyLeft", 1, 4, 100, true, SpriteEffects.None));
            AddAnimation(new Animation("EnemyStuck", 8, 8, 100, true, SpriteEffects.None, .25f));
            AddAnimation(new Animation("EnemyStuckLate", 9, 9, 100, true, SpriteEffects.None, .25f));
            AddAnimation(new Animation("EnemyDead", 1, 1, 100, true, SpriteEffects.None, .7f));
            AddAnimation(new Animation("EnemyFrozenRight", 1, 1, 100, false, SpriteEffects.FlipHorizontally, Color.Blue));
            AddAnimation(new Animation("EnemyFrozenLeft", 1, 1, 100, false, SpriteEffects.None, Color.Blue));
            AddAnimation(new Animation("EnemyStoppedRight", 1, 1, 100, false, SpriteEffects.FlipHorizontally));
            AddAnimation(new Animation("EnemyStoppedLeft", 1, 1, 100, false, SpriteEffects.None));

            elapsedPauseTime = BubbleGame.rand.Next(0, 4000);
        }



        public override void DetermineMove(GameTime gameTime)
        {
            base.DetermineMove(gameTime);

            if (isDead)
            {
                return;
            }

            // update movement speed for pauses
            if (!Stuck && !isFrozen)
            {
                elapsedPauseTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedPauseTime < pauseStart)
                {
                    movementSpeed = 3f;
                }
                else if (elapsedPauseTime < pauseEnd)
                {
                    movementSpeed = 0f;
                    PlayAnimation("EnemyStopped", currentDirection);
                    return;
                }
                else
                {
                    elapsedPauseTime = 0;
                    PlayAnimation("Enemy", currentDirection);
                    return;
                }
            }

            // check if we can still move in the direction that we want to
            if (0 == CanMove(currentDirection, (int)movementSpeed))
            {
                if (currentDirection == Direction.Right)
                {
                    nextDirection = Direction.Left;
                }
                else
                {
                    nextDirection = Direction.Right;
                }
            }
        }


       
    }
}