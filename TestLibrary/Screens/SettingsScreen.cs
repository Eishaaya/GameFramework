using BaseGameLibrary.Inputs;
using BaseGameLibrary.Visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

using static BaseGameLibrary.Visual.ActionButton;

namespace BaseGameLibrary
{
    class SettingsScreen : ScreenBase
    {
        ButtonBase defaltButt;
        ButtonBase arrowButt;
        ButtonBase applyButt;
        ButtonBase menuButt;
        //List<Button> bindButtons;
        List<ValueLabelBase> bindLabels;
        List<Setting> defaults;
        List<Setting> arrows;
        //List<string> keyTypes;
        //List<Keys> oldBinds;
        int index;
        public List<Toggler> toggles;
        //List<bool> toggOns;

        public SettingsScreen(ButtonBase d, ButtonBase a, ButtonBase ap, ButtonBase menuButton, Texture2D b, List<Setting> dk, List<Setting> ak, List<string> keyTypes, List<bool> togs, Toggler template, SpriteFont font, SoundEffect effect)
            : base(effect)
        {
            toggles = new List<Toggler>();
            bindLabels = new List<ValueLabelBase>();            
            applyButt = ap;
            defaults = dk;
            arrows = ak;
            menuButt = menuButton;
            Settings.AddRange(defaults.WithIDs());
            defaltButt = d;
            arrowButt = a;
            index = -1;
            //toggOns = togs;


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
              //  toggOns[i] = !toggOns[i];
                toggles.Add(new Toggler(template.Image, template.Location + offSet, template.Color, template.Rotation, template.Effect, template.Origin, template.Scale, template.Depth, template.HoverColor, template.ClickedColor,
                    new Sprite(template.Ball.Image, template.Ball.Location + offSet, template.Ball.Color, template.Ball.Rotation, template.Ball.Effect, template.Ball.Origin, template.Ball.Scale, template.Ball.Depth),
                    new Sprite(template.BottomColor.Image, template.BottomColor.Location + offSet, template.BottomColor.Color, template.BottomColor.Rotation, template.BottomColor.Effect, template.BottomColor.Origin, template.BottomColor.Scale, template.BottomColor.Depth),
                    new ScalableSprite(template.MovingColor.Image, template.MovingColor.Location + offSet, template.MovingColor.Color, template.MovingColor.Rotation, template.MovingColor.Effect, template.MovingColor.Origin, template.MovingColor.Scale2D, template.MovingColor.Depth, template.MovingColor.Scale), font, keyTypes[i + Settings.Count], 50, 0, 0, togs[i]));
            }
        }
        public override void Start()
        {
            base.Start();
 //           oldBinds = Settings;
            for (int i = 0; i < toggles.Count; i++)
            {
                //toggOns[i] = !toggles[i].On;
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
        public override void Update(GameTime time, Screenmanager manny, CursorRoot cursor)
        {
            base.Update(time, manny, cursor);
            if (heldMouse)
            {
                return;
            }
            if (index < 0)
            {
                if (defaltButt.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
                {
                    Settings.Switch(defaults.WithIDs());
                    for (int i = 0; i < Settings.Count; i++)
                    {
                        bindLabels[i].Text(Settings[(Index)i].KeyValue);
                    }
                }
                else if (arrowButt.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
                {
                    Settings.Switch(arrows.WithIDs());
                    for (int i = 0; i < Settings.Count; i++)
                    {
                        bindLabels[i].Text(Settings[(Index)i].KeyValue);
                    }
                }
                else if (applyButt.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
                {
                    manny.BindsChanged = true;
                    for (int i = 0; i < toggles.Count; i++)
                    {
                        toggles[i].On = !toggles[i].On;
                    }
                    manny.Back();
                    return;
                }
                else if (menuButt.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
                {
                    for (int i = 0; i < Settings.Count; i++)
                    {
                        Settings[(Index)i].Revert();
                    }
                    manny.Back();
                    for (int i = 0; i < toggles.Count; i++)
                    {
                        //toggles[i].On = !toggOns[i];
                    }
                    return;
                }

                buttonManager.Update(mousePos, heldMouse, mouseClicks);

                for (int i = 0; i < toggles.Count; i++)
                {
                    if (toggles[i].Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
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
                if (Idaho.GetPressedKeyCount() > 0)
                {
                    var temp = Settings[(Index)index]; //i is confuzzed
                    temp.KeyValue = Idaho.GetPressedKeys()[0];
                    bindLabels[index].Text(Settings[(Index)index].KeyValue);
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
                bindLabels[i].Draw(batch);
            }
            for (int i = 0; i < toggles.Count; i++)
            {
                toggles[i].Draw(batch);
            }
        }
    }
}
