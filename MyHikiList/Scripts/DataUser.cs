using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHikiList.Scripts {
    internal static class DataUser {

        public static string NameUser = "Anonim";

        public static HashSet<long> idWatchAnime = new HashSet<long>();
        public static HashSet<long> idWillWatchAnime = new HashSet<long>();
        public static HashSet<long> idWathedAnime = new HashSet<long>();
        public static HashSet<long> idBadAnime = new HashSet<long>();
    
        public static void AddWatchAnime(long id) { 
            RemoveAnimeAll(id);
            idWatchAnime.Add(id);
        }

        public static void AddWillWatchAnime(long id) {
            RemoveAnimeAll(id);
            idWillWatchAnime.Add(id);
        } 

        public static void AddWatchedAnime(long id) {
            RemoveAnimeAll(id);
            idWathedAnime.Add(id);
        }

        public static void AddBadAnime(long id) {
            RemoveAnimeAll(id);
            idBadAnime.Add(id);
        }

        private static void RemoveAnimeAll(long id) {
            if(idWatchAnime.Contains(id)) idWatchAnime.Remove(id);
            if(idWillWatchAnime.Contains(id)) idWillWatchAnime.Remove(id);
            if(idWathedAnime.Contains(id)) idWathedAnime.Remove(id);
            if (idBadAnime.Contains(id)) idBadAnime.Remove(id);
        }

    }
}
