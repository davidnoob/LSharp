using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace NoobDiana
{
    internal class Drawings
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            var drawOff = NoobDianaMenu._menu.Item("NoobDiana.Draw.off").GetValue<bool>();
            var drawQ = NoobDianaMenu._menu.Item("NoobDiana.Draw.Q").GetValue<Circle>();
            var drawW = NoobDianaMenu._menu.Item("NoobDiana.Draw.W").GetValue<Circle>();
            var drawE = NoobDianaMenu._menu.Item("NoobDiana.Draw.E").GetValue<Circle>();
            var drawR = NoobDianaMenu._menu.Item("NoobDiana.Draw.R").GetValue<Circle>();
            var drawRMisaya = NoobDianaMenu._menu.Item("NoobDiana.Draw.RMisaya").GetValue<Circle>();
            var misayaRange = NoobDianaMenu._menu.Item("NoobDiana.Combo.R.MisayaMinRange").GetValue<Slider>().Value;

            if (drawOff)
                return;

            if (drawQ.Active)
                if (Diana.spells[Spells.Q].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Diana.spells[Spells.Q].Range, Color.White);

            if (drawE.Active)
                if (Diana.spells[Spells.E].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Diana.spells[Spells.E].Range, Color.White);

            if (drawW.Active)
                if (Diana.spells[Spells.W].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Diana.spells[Spells.W].Range, Color.White);

            if (drawR.Active)
                if (Diana.spells[Spells.R].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Diana.spells[Spells.R].Range, Color.White);

            if (drawRMisaya.Active)
                if (Diana.spells[Spells.R].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, misayaRange, Color.White);
        }
    }
}