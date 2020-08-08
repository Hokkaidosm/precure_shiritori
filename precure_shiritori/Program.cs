using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace precure_shiritori
{
    class Program
    {
        /// <summary>
        /// rubicure/config のパス
        /// </summary>
        static readonly string rubicureConfigPath = "../../rubicure/config/";
        static void Main(string[] args)
        {
            List<Precure> precureList = LoadPrecure();
            List<PrecureChainList> precureChainLists = new List<PrecureChainList>();
            DateTime dt = DateTime.Now;
            foreach (Precure startPrecure in precureList)
            {
                PrecureChainSolver pc = new PrecureChainSolver(precureList);
                precureChainLists.Add(pc.Solve(startPrecure));
            }
            var ansList = precureChainLists.OrderByDescending(P => P.Count);
            int count = 0;
            foreach (var ans in ansList)
            {
                if (ans.Count != count)
                {
                    Console.WriteLine("============== {0}人 =================", ans.Count);
                    count = ans.Count;
                }
                ans.GetPrecureList().ToList().ForEach(precure => Console.WriteLine($"{precure.PrecureName}（{precure.PrecureRuby}）"));
                Console.WriteLine("---------------------------------------------");
            }
            Console.WriteLine("所要時間：" + (DateTime.Now - dt));
            Console.ReadLine();
        }

        /// <summary>
        /// 全てのプリキュアのデータを読み込む.
        /// </summary>
        /// <returns>プリキュアデータのリスト</returns>
        static List<Precure> LoadPrecure()
        {
            List<Precure> precureList = new List<Precure>();
            var rootPath = rubicureConfigPath + "girls/";
            string[] files = Directory.GetFiles(rootPath, "*.yml", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                var input = new StreamReader(file, Encoding.UTF8);
                var deserializer = new Deserializer();
                Dictionary<string, Dictionary<string, object>> girls_tmp = deserializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(input);
                foreach (var girl in girls_tmp)
                {
                    if (girl.Value.Count == 1)
                    {
                        // エイリアスは飛ばす
                        continue;
                    }
                    string precureName = null;
                    if (girl.Value.TryGetValue("precure_name", out object precureNameObj))
                    {
                        precureName = (string)precureNameObj;
                    }
                    precureList.Add(new Precure(precureName));
                }
            }

            // キュアアースの暫定対応（rubicureに入ったら消す）
            if (!precureList.Where(p => p.PrecureName == "キュアアース").Any())
            {
                precureList.Add(new Precure("キュアアース"));
            }

            return precureList;
        }
    }
}
