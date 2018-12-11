using Newtonsoft.Json;
using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SalamiTV.DAL
{
    public static class JSONDeserialize
    {
        public static void testc()
        {
            // hämtar mappnamnet och filändelsen
            string path2 = System.IO.Path.Combine("TvPrograms\\", ".js.gz");        // read file into a string and deserialize JSON to a type

        }

        public static void DeserializeJSON()
        {
            string path = System.IO.Path.Combine("TvPrograms\\", ".js.gz");        // read file into a string and deserialize JSON to a type

            TvProgram tvProgram = JsonConvert.DeserializeObject<TvProgram>(File.ReadAllText(Path.GetExtension()));
        }

        //Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));

        //// deserialize JSON directly from a file
        //using (StreamReader file = File.OpenText(@"c:\movie.json"))
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
    }
}