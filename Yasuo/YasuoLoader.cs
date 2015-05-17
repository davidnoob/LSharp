﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using Yasuo.Evade;

namespace Yasuo
{
    public class YasuoLoader
    {
        private static void Main(string[] args)
        {
            if (args != null)
            {
                CustomEvents.Game.OnGameLoad += Game_OnGameLoad; // => On Game Loads
            }
        }
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        private static void Game_OnGameLoad(EventArgs args)
        {
            // => Champion Check
            if (!ObjectManager.Player.ChampionName.Equals("Yasuo"))
            {
                return;
            }

            // => Set Values
            Yasuo.Player = ObjectManager.Player;
            Yasuo.Menu = new YasuoMenu();
            Yasuo.Game = new YasuoGame(Yasuo.Player, Yasuo.Menu);

            // => Class Caller
            new YasuoSpells(); // => Spell Initialization
            new YasuoEvade();

            var enemies = ObjectManager.Get<Obj_AI_Hero>().Where(e => e.IsEnemy);

            foreach (var spell in enemies.SelectMany(e1 => SpellDatabase.Spells.Where(s => s.ChampionName == e1.BaseSkinName))) {
                // => Windwall
                if (spell.CollisionObjects.Any(e2 => e2 == CollisionObjectTypes.YasuoWall))
                {
                    var spellActualName = spell.ChampionName;
                    var slot = "?";
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            spellActualName += " Q";
                            slot = "Q";
                            break;
                        case SpellSlot.W:
                            spellActualName += " W";
                            slot = "W";
                            break;
                        case SpellSlot.E:
                            spellActualName += " E";
                            slot = "E";
                            break;
                        case SpellSlot.R:
                            spellActualName += " R";
                            slot = "R";
                            break;
                    }
                    var theSpell = new Yasuo.MenuData
                    {
                        ChampionName = spell.ChampionName,
                        SpellName = spell.SpellName,
                        SpellDisplayName = spellActualName,
                        DisplayName = spellActualName,
                        Slot = slot,
                        IsWindwall = true
                    };
                    theSpell.AddToMenu();
                    Yasuo.MenuWallsList.Add(theSpell);
                }

                // => Evade
                var eVspellActualName = spell.ChampionName;
                var eVslot = "?";
                switch (spell.Slot)
                {
                    case SpellSlot.Q:
                        eVspellActualName += " Q";
                        eVslot = "Q";
                        break;
                    case SpellSlot.W:
                        eVspellActualName += " W";
                        eVslot = "W";
                        break;
                    case SpellSlot.E:
                        eVspellActualName += " E";
                        eVslot = "E";
                        break;
                    case SpellSlot.R:
                        eVspellActualName += " R";
                        eVslot = "R";
                        break;
                }
                var eVtheSpell = new Yasuo.MenuData
                {
                    ChampionName = spell.ChampionName,
                    SpellName = spell.SpellName,
                    SpellDisplayName = eVspellActualName,
                    DisplayName = eVspellActualName,
                    Slot = eVslot,
                    IsWindwall = false
                };
                eVtheSpell.AddToMenu();
                Yasuo.MenuDashesList.Add(eVtheSpell);
            }

            // => Events
            Game.OnUpdate += Yasuo.Game.OnGameUpdate; // => On Game Update

            // => Notify
            Game.PrintChat(
                "<font color=\"#be4cb7\"><b>Noob</b></font> | <font color=\"#1762a1\">Yasuo</font> loaded.");
        }
    }
}
