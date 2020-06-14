using System.Text.RegularExpressions;

namespace precure_shiritori
{
    /// <summary>
    /// プリキュアデータ
    /// </summary>
    class Precure
    {
        /// <summary>
        /// プリキュア名
        /// </summary>
        public string PrecureName { get; private set; }
        /// <summary>
        /// 読み
        /// </summary>
        public string PrecureRuby { get; private set; }
        /// <summary>
        /// 読みの最初の文字
        /// </summary>
        public char First { get; private set; }
        /// <summary>
        /// 読みの最後の文字
        /// </summary>
        public char Last { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">プリキュア名</param>
        public Precure(string name)
        {
            PrecureName = name;
            PrecureRuby = Regex.Replace(name, "^キュア", "");
            First = ToSeion(ToChokuon(PrecureRuby[0]));
            Last = ToSeion(ToChokuon(PrecureRuby[PrecureRuby.Length - 1]));
            // 長音一文字の単語はない前提
            if (Last == 'ー' || Last == '－')
            {
                Last = ToSeion(ToChokuon(PrecureRuby[PrecureRuby.Length - 2]));
            }
        }

        // ッは促音だが、拗音(Youon)という変数名とする
        private const string Youon = "ァィゥェォヵヶッャュョヮ";
        private const string Chokuon = "アイウエオカケツヤユヨワ";
        private char ToChokuon(char c)
        {
            int ix = Youon.IndexOf(c);
            if (ix > 0)
            {
                return Chokuon[ix];
            }
            return c;
        }

        //　半濁音を含んでいますが定数名はDakuon（濁音）にします
        private const string Dakuon = "ガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポ";
        private const string Seion  = "カキクケコサシスセソタチツテトハヒフヘホハヒフヘホ";
        private char ToSeion(char c)
        {
            int ix = Dakuon.IndexOf(c);
            if (ix > 0)
            {
                return Seion[ix];
            }
            return c;
        }

        public override string ToString()
        {
            return this.PrecureName.ToString();
        }
    }
}
