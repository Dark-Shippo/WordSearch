namespace WordFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string CategoriesAndWordsFilePath = "Words.txt";

            string[] CatergoriesAndWordsString =
                {
                    "Guild Wars 2", "Abbadon", "Melandru", "Balthazar", "Dwayna", "Grenth", "Kormir", "Lyssa", "Dhuum", "Aurene", "Jormag", "Mordremoth", "Glint", "Zhaitan", "Kralkatorrik", "Primordius",
                    "Rimworld", "Thrumbo", "Boomalope", "Alphabeaver", "Boomrat", "Mechanoid", "Megascarab", "MegaSloth", "WarQueen", "Spelopede", "Toxalope", "Animatree", "Termite", "Militor", "Tesseron", "Diabolus",
                    "Sly Cooper", "Sly", "Carmelita", "Bentley", "Murray", "Muggshot", "Pandaking", "Penelope", "Neyla", "Dimitri", "Clockwerk", "Contessa", "Guru", "Rajan", "Octavio", "JeanBison",
                    "Ratchet And Clank", "Ratchet", "Clank", "Nefarious", "Qwark", "Plumber", "Talwyn", "Skidd", "Starlene", "Protopet", "Sasha", "Rivet", "Azimuth", "Courtney", "Angela", "Skrunch",
                    "Dynasty Warriors", "CaoCao", "XunYu", "ZhugeLiang", "XiahouDun", "LuBu", "LiuBei", "GuanYinping", "GuanYu", "ZhangFei", "TaishiCi", "SunCe", "SunJian", "Diaochan", "PangTong", "ZhangLiao",
                    "World Of Warcraft", "Arthas", "Thrall", "Jaina", "Khadgar", "Sylvanas", "Medivh", "Alexstraza", "GulDan", "Sargeras", "Illidan", "Anduin", "Varian", "Ragnaros", "Elune", "Alleria",
                    "League Of Legends", "Ziggs", "Pantheon", "ChoGath", "Aatrox", "Anivia", "Briar", "Braum", "Draven", "Ekko", "Hecarim", "Nautilus", "Illaoi", "Jax", "Kallista", "Sona",
                    "Soulsborne", "Gwynn", "Ornstein", "Smough", "Manus", "Kalameet", "Sif", "Artorias", "Gyndolin", "Gwynevere", "Nito", "Gael", "Priscilla", "Seath", "Patches", "Radahn",
                    "Remnant From The Ashes", "Abomination", "Amalgam", "Annihilation", "Harvester", "Bane", "Bastion", "Bloat", "Cancer", "Cinderclad", "Corruptor", "Custodian", "Defiler", "Dran", "Pan", "Fae",
                    "Marvel", "Hulk", "Wolverine", "Nightcrawler", "Magneto", "Ironman", "Starlord", "Groot", "Rocket", "Collector", "Jeff", "Deadpool", "Darwin", "Cyclops", "Blackpanther", "fingfangfoom",
                };

            File.WriteAllLines(CategoriesAndWordsFilePath, CatergoriesAndWordsString);
        }
    }
}
