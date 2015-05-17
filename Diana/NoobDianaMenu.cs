using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using System.Drawing;


namespace NoobDiana
{

    public class NoobDianaMenu
    {
        public static Menu _menu;

        public static void Initialize()
        {
            _menu = new Menu("NoobDiana", "menu", true);

            var orbwalkerMenu = new Menu("Orbwalker", "orbwalker");
            Diana.Orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);

            _menu.AddSubMenu(orbwalkerMenu);

            var targetSelector = new Menu("Target Selector", "TargetSelector");
            TargetSelector.AddToMenu(targetSelector);

            _menu.AddSubMenu(targetSelector);

            var cMenu = new Menu("Combo", "Combo");
            cMenu.SubMenu("R").AddItem(new MenuItem("NoobDiana.Combo.R.Mode", "Mode").SetValue(new StringList(new[] { "Normal (Q->R)", "Misaya Combo (R->Q)" })));
            cMenu.SubMenu("R").AddItem(new MenuItem("NoobDiana.Combo.R", "Use R").SetValue(true));
            cMenu.SubMenu("R").AddItem(new MenuItem("NoobDiana.Combo.R.MisayaMinRange", "R Minimum Range for Misaya ").SetValue(new Slider(Convert.ToInt32(Diana.spells[Spells.R].Range * 0.8), 0, Convert.ToInt32(Diana.spells[Spells.R].Range))));

            cMenu.AddItem(new MenuItem("NoobDiana.Combo.Q", "Use Q").SetValue(true));
            cMenu.AddItem(new MenuItem("NoobDiana.Combo.W", "Use W").SetValue(true));
            cMenu.AddItem(new MenuItem("NoobDiana.Combo.E", "Use E").SetValue(true));
            cMenu.AddItem(new MenuItem("NoobDiana.Combo.Secure", "Use R to secure kill").SetValue(true));
            cMenu.AddItem(new MenuItem("NoobDiana.Combo.UseSecondRLimitation", "Max close enemies for secure kill with R").SetValue(new Slider(5, 1, 5)));
            cMenu.AddItem(new MenuItem("NoobDiana.Combo.Ignite", "Use Ignite").SetValue(true));
            cMenu.AddItem(new MenuItem("NoobDiana.ssssssssssss", ""));
            cMenu.AddItem(new MenuItem("NoobDiana.hitChance", "Hitchance Q").SetValue(new StringList(new[] { "Low", "Medium", "High", "Very High" }, 3)));
            //cMenu.AddItem(new MenuItem("NoobDiana.Combo.Leapcombo", "Leap Combo").SetValue(new KeyBind("T".ToCharArray()[0], KeyBindType.Press)));
            cMenu.AddItem(new MenuItem("ComboActive", "Combo!").SetValue(new KeyBind(32, KeyBindType.Press)));

            _menu.AddSubMenu(cMenu);

            var hMenu = new Menu("Harass", "Harass");
            hMenu.AddItem(new MenuItem("NoobDiana.Harass.Q", "Use Q").SetValue(true));
            hMenu.AddItem(new MenuItem("NoobDiana.Harass.W", "Use W").SetValue(true));
            hMenu.AddItem(new MenuItem("NoobDiana.Harass.E", "Use E").SetValue(true));
            hMenu.AddItem(new MenuItem("NoobDiana.Harass.Mana", "Minimum mana for harass")).SetValue(new Slider(55));

            _menu.AddSubMenu(hMenu);

            var lMenu = new Menu("Laneclear", "Laneclear");
            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.Q", "Use Q").SetValue(true));
            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.W", "Use W").SetValue(true));
            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.E", "Use E").SetValue(true));
            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.R", "Use R").SetValue(false));
            lMenu.AddItem(new MenuItem("xxx", ""));

            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.Count.Minions.Q", "Minions in range for Q").SetValue(new Slider(2, 1, 5)));
            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.Count.Minions.W", "Minions in range for W").SetValue(new Slider(2, 1, 5)));
            lMenu.AddItem(new MenuItem("NoobDiana.LaneClear.Count.Minions.E", "Minions in range for E").SetValue(new Slider(2, 1, 5)));

            _menu.AddSubMenu(lMenu);

            var jMenu = new Menu("Jungleclear", "Jungleclear");
            jMenu.AddItem(new MenuItem("NoobDiana.JungleClear.Q", "Use Q").SetValue(true));
            jMenu.AddItem(new MenuItem("NoobDiana.JungleClear.W", "Use W").SetValue(true));
            jMenu.AddItem(new MenuItem("NoobDiana.JungleClear.E", "Use E").SetValue(true));
            jMenu.AddItem(new MenuItem("NoobDiana.JungleClear.R", "Use R").SetValue(false));

            _menu.AddSubMenu(jMenu);

            var miscMenu = new Menu("Misc", "Misc");
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.off", "Turn drawings off").SetValue(false));
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.Q", "Draw Q").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.W", "Draw W").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.E", "Draw E").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.R", "Draw R").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.RMisaya", "Draw Misaya Combo Range").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("NoobDiana.Draw.Text", "Draw Text").SetValue(true));
            miscMenu.AddItem(new MenuItem("NoobDiana.misc.Notifications", "Use Notifications").SetValue(true));
            miscMenu.AddItem(new MenuItem("ezeazeezaze", ""));

            var switchComboMenu = new MenuItem("NoobDiana.Hotkey.ToggleComboMode", "Toggle Combo Mode Hotkey").SetValue(new KeyBind(84, KeyBindType.Press));
            miscMenu.AddItem(switchComboMenu);
            switchComboMenu.ValueChanged += (sender, eventArgs) =>
            {
                if (eventArgs.GetNewValue<KeyBind>().Active)
                    Diana.Orbwalker.ActiveMode = Orbwalking.OrbwalkingMode.Combo;
                else
                    Diana.Orbwalker.ActiveMode = Orbwalking.OrbwalkingMode.None;

            };

            var dmgAfterE = new MenuItem("NoobDiana.DrawComboDamage", "Draw combo damage").SetValue(true);
            var drawFill = new MenuItem("NoobDiana.DrawColour", "Fill colour", true).SetValue(new Circle(true, Color.FromArgb(204, 204, 0, 0)));
            miscMenu.AddItem(drawFill);
            miscMenu.AddItem(dmgAfterE);

            DrawDamage.DamageToUnit = Diana.GetComboDamage;
            DrawDamage.Enabled = dmgAfterE.GetValue<bool>();
            DrawDamage.Fill = drawFill.GetValue<Circle>().Active;
            DrawDamage.FillColor = drawFill.GetValue<Circle>().Color;

            dmgAfterE.ValueChanged += delegate (object sender, OnValueChangeEventArgs eventArgs)
            {
                DrawDamage.Enabled = eventArgs.GetNewValue<bool>();
            };

            drawFill.ValueChanged += delegate (object sender, OnValueChangeEventArgs eventArgs)
            {
                DrawDamage.Fill = eventArgs.GetNewValue<Circle>().Active;
                DrawDamage.FillColor = eventArgs.GetNewValue<Circle>().Color;
            };

            _menu.AddSubMenu(miscMenu);

            //Here comes the moneyyy, money, money, moneyyyy
            var credits = new Menu("Credits", "DavidNoob");
            credits.AddItem(new MenuItem("NoobDiana.Paypal", "if you would like to donate via paypal:"));
            credits.AddItem(new MenuItem("NoobDiana.Email", "infomation1994@gmail.com"));
            _menu.AddSubMenu(credits);

            _menu.AddItem(new MenuItem("OMG", ""));
            _menu.AddItem(new MenuItem("Holly Shit", String.Format("Version: {0}",Diana.ScriptVersion)));
            _menu.AddItem(new MenuItem("David", "Made By DavidNoob"));

            _menu.AddToMainMenu();

            Console.WriteLine("Menu Loaded");
        }
    }
}