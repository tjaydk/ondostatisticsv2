using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services
{
    public class MapIconChecker
    {
        /// <summary>
        /// Checks a string representation of a mapIcon id and returns an int representation 
        /// of the organisationType. Returns 0 by default
        /// </summary>
        /// <param name="mapIcon"></param>
        /// <returns></returns>
        public static int checkType(string mapIcon)
        {
            int ondoType;
            mapIcon = mapIcon.Substring(0, 2);

            switch (mapIcon)
            {
                case("01"):
                    ondoType = 1;
                    break;
                case ("02"):
                    ondoType = 2;
                    break;
                case ("03"):
                    ondoType = 2;
                    break;
                case ("04"):
                    ondoType = 2;
                    break;
                case ("05"):
                    ondoType = 2;
                    break;
                case ("06"):
                    ondoType = 3;
                    break;
                case ("07"):
                    ondoType = 2;
                    break;
                case ("08"):
                    ondoType = 2;
                    break;
                case ("09"):
                    ondoType = 2;
                    break;
                case ("10"):
                    ondoType = 2;
                    break;
                default:
                    ondoType = 0;
                    break;
            }

            return ondoType;
        }


        /// <summary>
        /// Returns a string representation of category from a mapIcon id. Returns "Ikke Angivet" by default
        /// </summary>
        /// <param name="mapIcon"></param>
        /// <returns></returns>
        public static string checkCategory(string mapIcon)
        {
            string catogory = "";

            switch(mapIcon)
            {
                case ("01001"):
                    catogory = "Ø eller mindre område";
                    break;
                case ("01002"):
                    catogory = "By eller Center";
                    break;
                case ("01003"):
                    catogory = "Sport";
                    break;
                case ("01004"):
                    catogory = "Festival";
                    break;
                case ("01005"):
                    catogory = "Produkt eller Leverandør";
                    break;
                case ("01999"):
                    catogory = "Andet Spotpunkt";
                    break;
                case ("02001"):
                    catogory = "Hus & Have";
                    break;
                case ("02002"):
                    catogory = "Tøj, Sko og Tasker";
                    break;
                case ("02003"):
                    catogory = "IT & Elektronik";
                    break;
                case ("02004"):
                    catogory = "Dagligvarer";
                    break;
                case ("02005"):
                    catogory = "Sport, Hobby og Fritid";
                    break;
                case ("02006"):
                    catogory = "Delikatesser og Vin";
                    break;
                case ("02007"):
                    catogory = "Personlig Pleje og Wellness";
                    break;
                case ("02008"):
                    catogory = "Optik, Ure og Smykker";
                    break;
                case ("02999"):
                    catogory = "Andre Varer";
                    break;
                case ("03001"):
                    catogory = "Restaurant";
                    break;
                case ("03002"):
                    catogory = "Cafe";
                    break;
                case ("03003"):
                    catogory = "Bar";
                    break;
                case ("03004"):
                    catogory = "Fastfood";
                    break;
                case ("03005"):
                    catogory = "Iskiosk";
                    break;
                case ("03999"):
                    catogory = "Andet Mad og Drikke";
                    break;
                case ("04001"):
                    catogory = "Hotel";
                    break;
                case ("04002"):
                    catogory = "Motel";
                    break;
                case ("04003"):
                    catogory = "Bed & Breakfast";
                    break;
                case ("04004"):
                    catogory = "Vandrehjem";
                    break;
                case ("04005"):
                    catogory = "Camping";
                    break;
                case ("04999"):
                    catogory = "Anden Overnatning";
                    break;
                case ("05001"):
                    catogory = "Museum";
                    break;
                case ("05002"):
                    catogory = "Forlystelse";
                    break;
                case ("05003"):
                    catogory = "Musik, Teater og Biograf";
                    break;
                case ("05999"):
                    catogory = "Andet Kultursted";
                    break;
                case ("06001"):
                    catogory = "Sportsklub";
                    break;
                case ("06999"):
                    catogory = "Anden Forening";
                    break;
                case ("07001"):
                    catogory = "Håndværker";
                    break;
                case ("07002"):
                    catogory = "Liberalt Erhverv";
                    break;
                case ("07999"):
                    catogory = "Andet Erhverv";
                    break;
                case ("88001"):
                    catogory = "Parkering";
                    break;
                case ("88002"):
                    catogory = "Toilet";
                    break;
                case ("88003"):
                    catogory = "Information";
                    break;
                case ("88004"):
                    catogory = "Handicap";
                    break;
                case ("88005"):
                    catogory = "Førstehjælp";
                    break;
                case ("88006"):
                    catogory = "Mødested";
                    break;
                case ("88007"):
                    catogory = "Bad";
                    break;
                case ("88008"):
                    catogory = "Køkken";
                    break;
                case ("88009"):
                    catogory = "Skifterum";
                    break;
                case ("88010"):
                    catogory = "Drikkevand";
                    break;
                case ("88011"):
                    catogory = "Camping";
                    break;
                case ("88012"):
                    catogory = "Genbrug";
                    break;
                case ("88013"):
                    catogory = "Legeplads";
                    break;
                case ("88014"):
                    catogory = "Offentlig Strand";
                    break;
                case ("88015"):
                    catogory = "Bederum";
                    break;
                case ("88016"):
                    catogory = "Tog, Bus og Transport";
                    break;
                case ("88017"):
                    catogory = "Pengeautomat";
                    break;
                case ("88999"):
                    catogory = "Andet";
                    break;
                case ("89001"):
                    catogory = "Privat";
                    break;
                case ("89002"):
                    catogory = "Offentlig";
                    break;
                case ("89003"):
                    catogory = "Social";
                    break;
                case ("89004"):
                    catogory = "Skole";
                    break;
                case ("89999"):
                    catogory = "Ukendt";
                    break;
                default:
                    catogory = "Ikke Angivet";
                    break;
            }

            return catogory;
        }


        /// <summary>
        /// Returns an string representation of cityCode from an ondoId
        /// </summary>
        /// <param name="ondoId"></param>
        /// <returns></returns>
        public static string getCityCode(int ondoId)
        {
            string cityCode = "";

            switch(ondoId)
            {
                case 10111:
                    cityCode = "003";
                    break;
                case 10112:
                    cityCode = "002";
                    break;
                case 10113:
                    cityCode = "004";
                    break;
            }

            return cityCode;
        }
    }
}
