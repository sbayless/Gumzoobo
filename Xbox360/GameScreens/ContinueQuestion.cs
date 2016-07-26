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
    enum YesNoReason
    {
        NotLoggedIn,
        CannotConnectDrive,
        LostDrive,
        ShouldDelete
    }


    class ContinueQuestion : PopUpScreen
    {
        bool result = false;
        bool isFinished = false;
        YesNoReason reason;
        Texture2D buttonTexture;

        public bool Result
        {
            get { return result; }
        }

        public bool IsFinished
        {
            get { return isFinished; }
        }


        public ContinueQuestion(YesNoReason reason)
        {
            this.reason = reason;
            buttonTexture = GameSprite.game.Content.Load<Texture2D>(System.IO.Path.Combine(@"Textures/UI", "button48593266"));

            SetPopUpAnimation(new Vector2(1290, 360 - 400 / 2), new Vector2(640 - 500 / 2, 360 - 400 / 2), 0, 1000, 1000);

            MenuEntry entry;
            if (reason == YesNoReason.ShouldDelete)
                entry = new MenuEntry("Yes");
            else
                entry = new MenuEntry("Continue");
            entry.Selected += new EventHandler<EventArgs>(entry_Selected);
            entry.SetStartAnimation(new Vector2(1290, windowCorner.Y + 170), new Vector2((1280 - buttonTexture.Width) / 2, windowCorner.Y + 170), 0, 1000, 1000);
            entry.SetAnimationType(AnimationType.Slide);
            entry.Font = Fonts.HeaderFont;
            entry.Texture = buttonTexture;
            MenuEntries.Add(entry);

            if (reason == YesNoReason.ShouldDelete)
                entry = new MenuEntry("No");
            else
                entry = new MenuEntry("Quit");
            entry.Selected += new EventHandler<EventArgs>(entry_Selected);
            entry.SetStartAnimation(new Vector2(1290, windowCorner.Y + 170 + buttonTexture.Height), new Vector2((1280 - buttonTexture.Width) / 2, windowCorner.Y + 170 + buttonTexture.Height), 0, 1000, 1000);
            entry.SetAnimationType(AnimationType.Slide);
            entry.Font = Fonts.HeaderFont;
            entry.Texture = buttonTexture;
            MenuEntries.Add(entry);

        }

        void entry_Selected(object sender, EventArgs e)
        {
            if (sender.Equals(MenuEntries[0]))
            {
                // continue / Yes
                result = true;
            }
            else if (sender.Equals(MenuEntries[1]))
            {
                // quit / No
                result = false;
            }
            isFinished = true;
            ExitScreen();
        }


        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);

            // draw boarder
            Rectangle helpTexture = new Rectangle(0, 0, 500, 300);
            int boarderWidth = 12;

            SpriteBatch spriteBatch = Level.screenManager.SpriteBatch;
            spriteBatch.Begin();
            DrawBoarder(new Rectangle((int)windowCorner.X - boarderWidth, (int)windowCorner.Y - boarderWidth, helpTexture.Width + boarderWidth * 2, helpTexture.Height + boarderWidth * 2),
                InternalContentManager.GetTexture("BlueStripe"), boarderWidth, Color.White, spriteBatch);

            // draw text
            if (reason == YesNoReason.NotLoggedIn)
            {
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "You are not signed in.", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 50), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "If you continue you", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 90), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "will not be able to save.", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 130), Color.Black, 1.5f);
            }
            else if (reason == YesNoReason.CannotConnectDrive)
            {
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "No storage device was selected.", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 50), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "If you continue you", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 90), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "will not be able to save.", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 130), Color.Black, 1.5f);
            }
            else if (reason == YesNoReason.LostDrive)
            {
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "The storage device was lost.", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 50), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "If you continue you", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 90), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "will not be able to save.", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 130), Color.Black, 1.5f);
            }
            else if (reason == YesNoReason.ShouldDelete)
            {
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "Are you sure", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 50), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "that you want to delete", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 90), Color.Black, 1.5f);
                Fonts.DrawCenteredText(spriteBatch, Fonts.DescriptionFont, "this save game?", new Vector2(windowCorner.X + boarderWidth + helpTexture.Width / 2, windowCorner.Y + 130), Color.Black, 1.5f);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        protected override void OnCancel()
        {
            isFinished = true;
            result = false;
            base.OnCancel();
        }
    }
}