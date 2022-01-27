using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class SettingsScreen : Screen
    {
        Button defaltButt;
        Button arrowButt;
        Button applyButt;
        Button menuButt;
        List<Button> bindButtons;
        List<Label> bindLabels;
        List<Keys> defaults;
        List<Keys> arrows;
        List<string> keyTypes;
        List<Keys> oldBinds;
        int index;
        public List<Toggler> toggles;
        List<bool> toggOns;

        public SettingsScreen(Button d, Button a, Button ap, Button menuButton, Texture2D b, List<Keys> dk, List<Keys> ak, List<string> kt, List<bool> togs, Toggler template, SpriteFont font, SoundEffect effect)
            : base(effect)
        {
            bindButtons = new List<Button>();
            toggles = new List<Toggler>();
            bindLabels = new List<Label>();
            keyTypes = new List<string>();
            applyButt = ap;
            defaults = dk;
            arrows = ak;
            menuButt = menuButton;
            Settings = defaults;
            defaltButt = d;
            arrowButt = a;
            keyTypes = kt;
            oldBinds = Settings;
            index = -1;
            toggOns = togs;

            for (int i = 0; i < defaults.Count / 2; i++)
            {
                bindLabels.Add(new Label(font, Color.White, new Vector2(150, i * 50 + 250), $"{keyTypes[i]} : {Settings[i]}"));
                bindButtons.Add(new Button(b, new Vector2(150, i * 50 + 250), Color.Black, 0, SpriteEffects.None, new Vector2(0, 0), 1, .1f, Color.DarkGray, Color.Gray));
            }
            for (int i = defaults.Count / 2; i < defaults.Count; i++)
            {
                bindLabels.Add(new Label(font, Color.White, new Vector2(350, (i - defaults.Count / 2) * 50 + 250), $"{keyTypes[i]} : {Settings[i]}"));
                bindButtons.Add(new Button(b, new Vector2(350, (i - defaults.Count / 2) * 50 + 250), Color.Black, 0, SpriteEffects.None, new Vector2(0, 0), 1, .1f, Color.DarkGray, Color.Gray));
            }
            int finalRow = 0;
            for (int i = 0; i < togs.Count; i++)
            {
                int j = i / 3;
                Vector2 offSet = new Vector2(100 + i * 150 - j * 450, j * 75 + 550);
                if (i % 3 == 0 && togs.Count - i < 3 || finalRow != 0)
                {
                    if (finalRow == 0)
                    {
                        finalRow = 75 * (togs.Count - i - 1);
                    }
                    offSet = new Vector2(offSet.X + finalRow, offSet.Y);
                }
                toggOns[i] = !toggOns[i];
                toggles.Add(new Toggler(template.Image, template.Location + offSet, template.Color, template.Rotation, template.Effect, template.Origin, template.Scale, template.Depth, template.HoverColor, template.ClickedColor,
                    new Sprite(template.Ball.Image, template.Ball.Location + offSet, template.Ball.Color, template.Ball.Rotation, template.Ball.Effect, template.Ball.Origin, template.Ball.Scale, template.Ball.Depth),
                    new Sprite(template.BottomColor.Image, template.BottomColor.Location + offSet, template.BottomColor.Color, template.BottomColor.Rotation, template.BottomColor.Effect, template.BottomColor.Origin, template.BottomColor.Scale, template.BottomColor.Depth),
                    new ScalableSprite(template.MovingColor.Image, template.MovingColor.Location + offSet, template.MovingColor.Color, template.MovingColor.Rotation, template.MovingColor.Effect, template.MovingColor.Origin, template.MovingColor.Scale2D, template.MovingColor.Depth, template.MovingColor.Scale), font, keyTypes[i + Settings.Count], 50, 0, 0, togs[i]));
            }
        }
        public override void Start()
        {
            base.Start();
            oldBinds = Settings;
            for (int i = 0; i < toggles.Count; i++)
            {
                toggOns[i] = !toggles[i].On;
                toggles[i].Done = false;
            }
        }
        public override List<bool> GetBools()
        {
            var allBools = new List<bool>();
            for (int i = 0; i < toggles.Count; i++)
            {
                allBools.Add(!toggles[i].On);
            }
            return allBools;
        }
        public override void Update(GameTime time, Screenmanager manny)
        {
            base.Update(time, manny);
            if (heldMouse)
            {
                return;
            }
            if (index < 0)
            {
                if (defaltButt.Check(mousy.Position.ToVector2(), mouseRightClick))
                {
                    Settings = defaults;
                    for (int i = 0; i < Settings.Count; i++)
                    {
                        bindLabels[i].Text($"{keyTypes[i]} : {Settings[i]}");
                    }
                }
                else if (arrowButt.Check(mousy.Position.ToVector2(), mouseRightClick))
                {
                    Settings = arrows;
                    for (int i = 0; i < Settings.Count; i++)
                    {
                        bindLabels[i].Text($"{keyTypes[i]} : {Settings[i]}");
                    }
                }
                else if (applyButt.Check(mousy.Position.ToVector2(), mouseRightClick))
                {
                    manny.BindsChanged = true;
                    for (int i = 0; i < toggles.Count; i++)
                    {
                        toggles[i].On = !toggles[i].On;
                    }
                    manny.Back();
                    return;
                }
                else if (menuButt.Check(mousy.Position.ToVector2(), mouseRightClick))
                {
                    Settings = oldBinds;
                    manny.Back();
                    for (int i = 0; i < toggles.Count; i++)
                    {
                        toggles[i].On = !toggOns[i];
                    }
                    return;
                }

                for (int i = 0; i < Settings.Count; i++)
                {
                    if (bindButtons[i].Check(mousy.Position.ToVector2(), mouseRightClick))
                    {
                        bindLabels[i].Text($"{keyTypes[i]} : ");
                        index = i;
                    }
                }
                for (int i = 0; i < toggles.Count; i++)
                {
                    if (toggles[i].Check(mousy.Position.ToVector2(), mouseRightClick))
                    {
                        if (i == 0)
                        {
                            if (toggles[i].On)
                            {
                                StopMusic();
                                playMusic = false;
                            }
                            else
                            {
                                playMusic = true;
                                Music.Resume();
                            }
                        }
                    }
                }
            }
            else
            {
                if (!bindButtons[index].Check(mousy.Position.ToVector2(), mouseRightClick) && mouseRightClick)
                {
                    bindLabels[index].Text($"{keyTypes[index]} : {Settings[index]}");
                    index = -1;
                    return;
                }
                if (Maryland.GetPressedKeyCount() > 0)
                {
                    Settings[index] = Maryland.GetPressedKeys()[0];
                    bindLabels[index].Text($"{keyTypes[index]} : {Settings[index]}");
                    index = -1;
                    return;
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            defaltButt.Draw(batch);
            arrowButt.Draw(batch);
            menuButt.Draw(batch);
            applyButt.Draw(batch);
            for (int i = 0; i < Settings.Count; i++)
            {
                bindButtons[i].Draw(batch);
                bindLabels[i].Print(batch);
            }
            for (int i = 0; i < toggles.Count; i++)
            {
                toggles[i].Draw(batch);
            }
        }
    }
}
