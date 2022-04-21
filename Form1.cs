﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.IO.Ports;
using WorldQuakeViewer.Properties;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using USGSQuakeClass;
using USGSFERegionsClass;
using USGSFERegionsClass2;
using CoreTweet;

namespace WorldQuakeViewer

{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void JsonTimer_Tick(object sender, EventArgs e)
        {
            //try
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "　動作開始");

                WebClient WC = new WebClient
                {
                    Encoding = Encoding.UTF8
                };

                string USGSQuakeJson_ = "[" + WC.DownloadString("https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_day.geojson") + "]";
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "　地震情報ダウンロード終了");
                double StartTime = Convert.ToDouble(DateTime.Now.ToString("yyyyMMddHHmmss.ffff"));
                List<USGSQuake> USGSQuakeJson = JsonConvert.DeserializeObject<List<USGSQuake>>(USGSQuakeJson_);
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "　地震情報デシアライズ終了");
                Console.WriteLine(USGSQuakeJson);
                Console.WriteLine(USGSQuakeJson[0].Features[0].Properties.Mmi);
                string MaxInt;
                if (USGSQuakeJson[0].Features[0].Properties.Mmi < 1.5)
                {
                    MaxInt = "I";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 2.5)
                {
                    MaxInt = "II";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 3.5)
                {
                    MaxInt = "III";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 4.5)
                {
                    MaxInt = "IV";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 5.5)
                {
                    MaxInt = "V";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 6.5)
                {
                    MaxInt = "VI";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 7.5)
                {
                    MaxInt = "VII";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 8.5)
                {
                    MaxInt = "VIII";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 9.5)
                {
                    MaxInt = "IX";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 10.5)
                {
                    MaxInt = "X";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi < 11.5)
                {
                    MaxInt = "XI";
                }
                else if (USGSQuakeJson[0].Features[0].Properties.Mmi >= 11.5)
                {
                    MaxInt = "XII";
                }
                else
                {
                    MaxInt = "-";
                }
                DateTimeOffset DataTime = DateTimeOffset.FromUnixTimeMilliseconds((long)USGSQuakeJson[0].Features[0].Properties.Time).ToLocalTime();
                string Time = Convert.ToString(DataTime).Replace("+0", "※UTC +0").Replace("+1", "※UTC +1").Replace("-0", "※UTC -0").Replace("+1", "※UTC -1");
                string Mag = USGSQuakeJson[0].Features[0].Properties.MagType + ":" + USGSQuakeJson[0].Features[0].Properties.Mag;
                double Lat = USGSQuakeJson[0].Features[0].Geometry.Coordinates[1];
                double Long = USGSQuakeJson[0].Features[0].Geometry.Coordinates[0];
                string Depth = $"深さ:{USGSQuakeJson[0].Features[0].Geometry.Coordinates[2]}km";
                string USGSFERegion_ = WC.DownloadString($"https://earthquake.usgs.gov/ws/geoserve/regions.json?latitude={Lat}&longitude={Long}&type=fe");
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "　震源情報ダウンロード終了");
                string Shingen1 = "震源: - - - - - ";
                try
                {
                    USGSFERegions USGSFERegion = JsonConvert.DeserializeObject<USGSFERegions>(USGSFERegion_);
                    Shingen1 = "震源:" + HypoName[USGSFERegion.Fe.Features[0].Properties.Number];
                }
                catch
                {
                    USGSFERegions2 USGSFERegion2 = JsonConvert.DeserializeObject<USGSFERegions2>(USGSFERegion_);
                    Shingen1 = "震源:" + USGSFERegion2.Fe.Features[0].Properties.Name;
                }
                string Shingen2 = $"({USGSQuakeJson[0].Features[0].Properties.Place})";

                int LocX;
                if (Long >= 0)
                {
                    LocX = (int)(Long + 180) * -5 + 200;
                }
                else
                {
                    LocX = (int)(180 - Long * -1) * -5 + 200;
                }
                int LocY;
                if (Lat >= 0)
                {
                    LocY = (int)(90 - Lat) * -5 + 300;
                }
                else
                {
                    LocY = (int)(Lat + 90) * -5 + 300;
                }
                MainImg.Location = new Point(LocX, LocY);
                Console.WriteLine("緯度" + Lat);
                Console.WriteLine("経度" + Long);
                Console.WriteLine("X" + LocX);
                Console.WriteLine("Y" + LocY);
                Console.WriteLine(MainImg.Location);
                string MMI = "";
                if (USGSQuakeJson[0].Features[0].Properties.Mmi != null)
                {
                    MMI = $"({Convert.ToString(USGSQuakeJson[0].Features[0].Properties.Mmi)})";
                }

                USGS0.Text = $"USGS地震情報　　{Time}";
                USGS1.Text = $"{Shingen1}\n{Shingen2}\n{Depth}\n{Mag}";
                USGS2.Text = $"{MaxInt}";
                USGS3.Text = $"改正メルカリ震度階級:  　　　　　{MMI.Replace("(","")}";

                if (USGSQuakeJson[0].Features[0].Properties.Mag >= 6.0)
                {
                    USGS0.ForeColor = Color.Yellow;
                    USGS1.ForeColor = Color.Yellow;
                    USGS2.ForeColor = Color.Yellow;
                    USGS3.ForeColor = Color.Yellow;
                    try
                    {
                        string TweetText = $"USGS地震情報【{Mag.Replace(":", "")}】{Time.Replace("※","(")})\n{Shingen1}{Shingen2}\n{Depth}\n改正メルカリ震度階級:{MaxInt}{MMI}";
                        DateTime DataTime2 = Convert.ToDateTime(Convert.ToString(DataTime).Remove(19, 7));
                        DateTime NowTime = DateTime.Now;
                        TimeSpan ReMainTime = NowTime - DataTime2;
                        if (Settings.Default.Tweet && TweetText != Settings.Default.TweetedText && ReMainTime <= TimeSpan.FromHours(12))
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            string tokens_json = File.ReadAllText($"Tokens.json");
                            Tokens_JSON Tokens_jsondata = JsonConvert.DeserializeObject<Tokens_JSON>(tokens_json);
                            Tokens tokens = Tokens.Create(Tokens_jsondata.ConsumerKey, Tokens_jsondata.ConsumerSecret, Tokens_jsondata.AccessToken, Tokens_jsondata.AccessSecret);
                            Status status = tokens.Statuses.Update(new { status = TweetText });
                        }
                        Settings.Default.TweetedText = TweetText;
                    }
                    catch
                    {
                    }
                    if (USGSQuakeJson[0].Features[0].Properties.Mag >= 8.0)
                    {
                        USGS0.ForeColor = Color.Red;
                        USGS1.ForeColor = Color.Red;
                        USGS2.ForeColor = Color.Red;
                        USGS3.ForeColor = Color.Red;
                    }
                }
                else
                {
                    USGS0.ForeColor = Color.White;
                    USGS1.ForeColor = Color.White;
                    USGS2.ForeColor = Color.White;
                    USGS3.ForeColor = Color.White;
                }
                
                /*震源印画像描画うまくできないので保留
                Bitmap ShingenPoint = new Bitmap(Resources.Point);
                ShingenPoint.MakeTransparent();
                ShingenImg.Image = ShingenPoint;
                ShingenImg.BackgroundImage = ShingenPoint;
                MainImg.Controls.Add(ShingenImg);
                ShingenImg.BackColor = Color.Transparent;
                ShingenImg.Size = new Size(40, 40);
                Console.WriteLine(ShingenImg.Size);
                Console.WriteLine(ShingenImg.Image);
                Console.WriteLine(ShingenImg.Location);

                //ShingenLabel1.Size = new Size(400, 400);
                //ShingenLabel2.Size = new Size(400, 400);
                ShingenLabel1.Controls.Add(MainImg);
                ShingenLabel2.Controls.Add(MainImg);
                ShingenLabel1.BackColor = Color.Transparent;
                ShingenLabel2.BackColor = Color.Transparent;

                Console.WriteLine(MainImg.Image);
                */
            }/*
            catch (Exception ex)
            {
            }*/
        }
        public Dictionary<int, string> HypoName = new Dictionary<int, string>
        {
{1,"アメリカ アラスカ州中央部"},
{2,"アメリカ アラスカ州南部"},
{3,"ベーリング海"},
{4,"ロシア コマンドル諸島"},
{5,"アリューシャン列島ニア諸島"},
{6,"アリューシャン列島ラット諸島"},
{7,"アリューシャン列島アンドリアノフ諸島"},
{8,"アメリカ プリビロフ諸島"},
{9,"アリューシャン列島フォックス諸島"},
{10,"アメリカ アラスカ州ユーニマク島"},
{11,"アメリカ ブリストル湾"},
{12,"アメリカ アラスカ半島"},
{13,"アメリカ アラスカ州コディアク島"},
{14,"アメリカ アラスカ州キーナイ半島"},
{15,"アラスカ湾"},
{16,"アリューシャン列島南方"},
{17,"アラスカ州南方"},
{18,"カナダ ユーコン準州南部"},
{19,"アメリカ アラスカ州南東部"},
{20,"アメリカ アラスカ州南東部沖"},
{21,"バンクーバー島西方"},
{22,"カナダ クイーンシャーロット諸島"},
{23,"カナダ ブリティッシュコロンビア州"},
{24,"カナダ アルバータ州"},
{25,"カナダ バンクーバー島"},
{26,"アメリカ ワシントン州沖"},
{27,"アメリカ ワシントン州沿岸"},
{28,"アメリカ ワシントン/オレゴン州境"},
{29,"アメリカ ワシントン州"},
{30,"アメリカ オレゴン州沖"},
{31,"アメリカ オレゴン州沿岸"},
{32,"アメリカ オレゴン州"},
{33,"アメリカ アイダホ州"},
{34,"アメリカ カリフォルニア州北部沖"},
{35,"アメリカ カリフォルニア州北部沿岸"},
{36,"アメリカ カリフォルニア州北部"},
{37,"アメリカ ネバダ州"},
{38,"アメリカ カリフォルニア州沖"},
{39,"アメリカ カリフォルニア州中部"},
{40,"アメリカ カリフォルニア/ネバダ州境"},
{41,"アメリカ ネバダ州"},
{42,"アメリカ アリゾナ州"},
{43,"アメリカ カリフォルニア州南部"},
{44,"アメリカ カリフォルニア/アリゾナ州境"},
{45,"カリフォルニア州(アメリカ)/メキシコ国境"},
{46,"アリゾナ州(アメリカ)/ソノラ州(メキシコ)境"},
{47,"メキシコ バハカリフォルニア州西方沖"},
{48,"メキシコ バハカリフォルニア州"},
{49,"メキシコ カリフォルニア湾"},
{50,"メキシコ ソノラ州"},
{51,"メキシコ中部沖"},
{52,"メキシコ中部沿岸"},
{53,"メキシコ レビージャヒヘード"},
{54,"メキシコ ハリスコ州沖"},
{55,"メキシコ ハリスコ州沿岸"},
{56,"メキシコ ミチョアカン州沿岸"},
{57,"メキシコ ミチョアカン州"},
{58,"メキシコ ゲレロ州沿岸"},
{59,"メキシコ ゲレロ州"},
{60,"メキシコ オアハカ州"},
{61,"メキシコ チアパス州"},
{62,"メキシコ/グアテマラ国境"},
{63,"メキシコ沖"},
{64,"メキシコ ミチョアカン州沖"},
{65,"メキシコ ゲレロ州沖"},
{66,"メキシコ オアハカ州沿岸"},
{67,"メキシコ オアハカ州沖"},
{68,"メキシコ チアパス州沖"},
{69,"メキシコ チアパス州沿岸"},
{70,"グアテマラ"},
{71,"グアテマラ沿岸"},
{72,"ホンジュラス"},
{73,"エルサルバドル"},
{74,"ニカラグア沿岸"},
{75,"ニカラグア"},
{76,"中央アメリカ沖"},
{77,"コスタリカ沖"},
{78,"コスタリカ"},
{79,"パナマ北方"},
{80,"パナマ/コスタリカ国境"},
{81,"パナマ"},
{82,"パナマ/コロンビア国境"},
{83,"パナマ南方"},
{84,"メキシコ ユカタン半島"},
{85,"キューバ"},
{86,"ジャマイカ"},
{87,"ハイチ"},
{88,"ドミニカ共和国"},
{89,"モナ海峡"},
{90,"プエルトリコ"},
{91,"バージン諸島"},
{92,"リワード諸島"},
{93,"ベリーズ"},
{94,"カリブ海"},
{95,"ウィンドワード諸島"},
{96,"コロンビア北岸"},
{97,"ベネズエラ沿岸"},
{98,"トリニダード・トバゴ"},
{99,"コロンビア北部"},
{100,"ベネズエラ マラカイボ湖"},
{101,"ベネズエラ"},
{102,"コロンビア西岸"},
{103,"コロンビア"},
{104,"エクアドル沖"},
{105,"エクアドル沿岸"},
{106,"コロンビア/エクアドル国境"},
{107,"エクアドル"},
{108,"ペルー北部沖"},
{109,"ペルー北部沿岸"},
{110,"ペルー/エクアドル国境"},
{111,"ペルー北部"},
{112,"ペルー/ブラジル国境"},
{113,"ブラジル アマゾナス州"},
{114,"ペルー沖"},
{115,"ペルー沿岸"},
{116,"ペルー中部"},
{117,"ペルー南部"},
{118,"ペルー/ボリビア国境"},
{119,"ボリビア北部"},
{120,"ボリビア中部"},
{121,"チリ北部沖"},
{122,"チリ北部沿岸"},
{123,"チリ北部"},
{124,"チリ/ボリビア国境"},
{125,"ボリビア南部"},
{126,"パラグアイ"},
{127,"チリ/アルゼンチン国境"},
{128,"アルゼンチン フフイ州"},
{129,"アルゼンチン サルタ州"},
{130,"アルゼンチン カタマルカ州"},
{131,"アルゼンチン トゥクマン州"},
{132,"アルゼンチン サンティアゴデルエステロ州"},
{133,"アルゼンチン北東部"},
{134,"チリ中部沖"},
{135,"チリ中部沿岸"},
{136,"チリ中部"},
{137,"アルゼンチン サンフアン州"},
{138,"アルゼンチン ラリオハ州"},
{139,"アルゼンチン メンドサ州"},
{140,"アルゼンチン サンルイス州"},
{141,"アルゼンチン コルドバ州"},
{142,"ウルグアイ"},
{143,"チリ南部沖"},
{144,"チリ南部"},
{145,"チリ南部/アルゼンチン国境"},
{146,"アルゼンチン南部"},
{147,"ティエラデルフエゴ"},
{148,"フォークランド諸島"},
{149,"ドレーク海峡"},
{150,"スコシア海"},
{151,"サウスジョージア島"},
{152,"サウスジョージア海膨"},
{153,"サウスサンドウィッチ諸島"},
{154,"サウスシェトランド諸島"},
{155,"南極半島"},
{156,"大西洋南西部"},
{157,"ウェッデル海"},
{158,"ニュージーランド 北島西方沖"},
{159,"ニュージーランド 北島"},
{160,"ニュージーランド 北島東方沖"},
{161,"ニュージーランド 南島西方沖"},
{162,"ニュージーランド 南島"},
{163,"ニュージーランド クック海峡"},
{164,"ニュージーランド 南島東方沖"},
{165,"マクオーリー島北方"},
{166,"オークランド諸島"},
{167,"マクオーリー島"},
{168,"ニュージーランド南方"},
{169,"サモア諸島"},
{170,"サモア諸島"},
{171,"フィジー諸島南方"},
{172,"トンガ諸島西方"},
{173,"トンガ諸島"},
{174,"トンガ諸島"},
{175,"トンガ諸島南方"},
{176,"ニュージーランド北方"},
{177,"ケルマデック諸島"},
{178,"ケルマデック諸島"},
{179,"ケルマデック諸島南方"},
{180,"フィジー諸島北方"},
{181,"フィジー諸島"},
{182,"フィジー諸島"},
{183,"サンタクルーズ諸島"},
{184,"サンタクルーズ諸島"},
{185,"バヌアツ諸島"},
{186,"バヌアツ諸島"},
{187,"ニューカレドニア"},
{188,"ローヤリティー諸島"},
{189,"ローヤリティー諸島南東方"},
{190,"パプアニューギニア ニューアイルランド"},
{191,"ソロモン諸島北方"},
{192,"パプアニューギニア ニューブリテン"},
{193,"ソロモン諸島"},
{194,"パプアニューギニア ダントルカストー"},
{195,"ソロモン諸島南方"},
{196,"インドネシア パプア"},
{197,"インドネシア パプア北岸"},
{198,"パプアニューギニア ニーニゴー諸島"},
{199,"パプアニューギニア アドミラルティ"},
{200,"パプアニューギニア ニューギニア北岸"},
{201,"インドネシア パプア"},
{202,"パプアニューギニア ニューギニア"},
{203,"ビスマルク海"},
{204,"インドネシア アルー諸島"},
{205,"インドネシア パプア南岸"},
{206,"パプアニューギニア ニューギニア南岸"},
{207,"パプアニューギニア ニューギニア東部"},
{208,"アラフラ海"},
{209,"パラオ"},
{210,"マリアナ諸島南方"},
{211,"本州南東方"},
{212,"小笠原諸島"},
{213,"硫黄列島"},
{214,"マリアナ諸島西方"},
{215,"マリアナ諸島"},
{216,"マリアナ諸島"},
{217,"ロシア カムチャツカ半島"},
{218,"ロシア カムチャツカ半島東岸"},
{219,"ロシア カムチャツカ半島東方沖"},
{220,"千島列島北西方"},
{221,"千島列島"},
{222,"千島列島東方"},
{223,"日本海東部"},
{224,"北海道"},
{225,"北海道南東沖"},
{226,"本州東部西岸"},
{227,"本州東部"},
{228,"本州東部東岸"},
{229,"本州東方沖"},
{230,"本州東部南岸"},
{231,"韓国"},
{232,"本州西部"},
{233,"本州西部南岸"},
{234,"南西諸島北西方"},
{235,"九州"},
{236,"四国"},
{237,"四国南東方"},
{238,"南西諸島"},
{239,"南西諸島南東方"},
{240,"小笠原諸島西方"},
{241,"フィリピン海"},
{242,"中国南東部沿岸"},
{243,"台湾"},
{244,"台湾"},
{245,"台湾北東方"},
{246,"南西諸島南西部"},
{247,"台湾南東方"},
{248,"フィリピン諸島"},
{249,"フィリピン諸島 ルソン"},
{250,"フィリピン諸島 ミンドロ"},
{251,"フィリピン諸島 サマル"},
{252,"フィリピン諸島 パラワン"},
{253,"スールー海"},
{254,"フィリピン諸島 パナイ"},
{255,"フィリピン諸島 セブ"},
{256,"フィリピン諸島 レイテ"},
{257,"フィリピン諸島 ネグロス"},
{258,"フィリピン諸島 スールー諸島"},
{259,"フィリピン諸島 ミンダナオ"},
{260,"フィリピン諸島東方"},
{261,"カリマンタン"},
{262,"セレベス海"},
{263,"インドネシア タラウド諸島"},
{264,"インドネシア ハルマヘラ北方"},
{265,"インドネシア スラウェシ ミナハサ半島"},
{266,"モルッカ海"},
{267,"インドネシア ハルマヘラ"},
{268,"インドネシア スラウェシ"},
{269,"スラ諸島"},
{270,"セラム海"},
{271,"インドネシア ブル"},
{272,"インドネシア セラム"},
{273,"インドネシア スマトラ南西方"},
{274,"インドネシア スマトラ南部"},
{275,"ジャワ海"},
{276,"スンダ海峡"},
{277,"インドネシア ジャワ"},
{278,"バリ海"},
{279,"フローレス海"},
{280,"バンダ海"},
{281,"インドネシア タニンバル諸島"},
{282,"インドネシア ジャワ南方"},
{283,"インドネシア バリ"},
{284,"インドネシア バリ南方"},
{285,"インドネシア スンバワ"},
{286,"インドネシア フローレス"},
{287,"インドネシア スンバ"},
{288,"サブ海"},
{289,"ティモール島"},
{290,"ティモール海"},
{291,"インドネシア スンバワ南方"},
{292,"インドネシア スンバ南方"},
{293,"ティモール南方"},
{294,"ミャンマー/インド国境"},
{295,"ミャンマー/バングラデシュ国境"},
{296,"ミャンマー"},
{297,"ミャンマー/中国国境"},
{298,"ミャンマー南岸"},
{299,"アジア南東部"},
{300,"中国 海南島"},
{301,"南シナ海"},
{302,"カシミール東部"},
{303,"カシミール/インド境界"},
{304,"カシミール/チベット自治区(中国)境界"},
{305,"チベット自治区西部(中国)/インド国境"},
{306,"チベット自治区(中国)"},
{307,"中国 スーチョワン(四川)省"},
{308,"インド北部"},
{309,"ネパール/インド国境"},
{310,"ネパール"},
{311,"インド シッキム州"},
{312,"ブータン"},
{313,"チベット自治区東部(中国)/インド国境"},
{314,"インド南部"},
{315,"インド/バングラデシュ国境"},
{316,"バングラデシュ"},
{317,"インド北東部"},
{318,"中国 ユンナン(雲南)省"},
{319,"ベンガル湾"},
{320,"キルギス/シンチアンウイグル自治区(中国)国境"},
{321,"中国 シンチアンウイグル自治区南部"},
{322,"中国 カンスー(甘粛)省"},
{323,"中国 ネイモンクー(内蒙古)自治区西部"},
{324,"カシミール/シンチアンウイグル自治区(中国)境界"},
{325,"中国 チンハイ(青海)省"},
{326,"ロシア シベリア南西部"},
{327,"ロシア バイカル湖"},
{328,"ロシア バイカル湖東方"},
{329,"カザフスタン東部"},
{330,"キルギス イシククル湖"},
{331,"カザフスタン/シンチアンウイグル自治区(中国)国境"},
{332,"中国 シンチアンウイグル自治区北部"},
{333,"ロシア/モンゴル国境"},
{334,"モンゴル"},
{335,"ロシア ウラル山脈"},
{336,"カザフスタン西部"},
{337,"コーカサス"},
{338,"カスピ海"},
{339,"ウズベキスタン"},
{340,"トルクメニスタン"},
{341,"トルクメニスタン/イラン国境"},
{342,"トルクメニスタン/アフガニスタン国境"},
{343,"トルコ/イラン国境"},
{344,"イラン/アルメニア/アゼルバイジャン国境"},
{345,"イラン"},
{346,"イラン/イラク国境"},
{347,"イラン"},
{348,"イラン"},
{349,"アフガニスタン"},
{350,"アフガニスタン"},
{351,"アラビア半島東部"},
{352,"ペルシャ湾"},
{353,"イラン"},
{354,"パキスタン南西部"},
{355,"オマーン湾"},
{356,"パキスタン沖"},
{357,"ウクライナ/モルドバ/ロシア南西部"},
{358,"ルーマニア"},
{359,"ブルガリア"},
{360,"黒海"},
{361,"ウクライナ クリミア"},
{362,"コーカサス"},
{363,"ギリシャ/ブルガリア国境"},
{364,"ギリシャ"},
{365,"エーゲ海"},
{366,"トルコ"},
{367,"トルコ/ジョージア/アルメニア国境"},
{368,"ギリシャ南部"},
{369,"ギリシャ ドデカネス諸島"},
{370,"ギリシャ クレタ"},
{371,"地中海東部"},
{372,"キプロス"},
{373,"死海"},
{374,"ヨルダン/シリア"},
{375,"イラク"},
{376,"ポルトガル"},
{377,"スペイン"},
{378,"ピレネー山脈"},
{379,"フランス南岸"},
{380,"フランス コルシカ"},
{381,"イタリア中央部"},
{382,"アドリア海"},
{383,"バルカン半島北西部"},
{384,"ジブラルタル西方"},
{385,"ジブラルタル海峡"},
{386,"スペイン バレアレス諸島"},
{387,"地中海西部"},
{388,"イタリア サルデーニャ"},
{389,"ティレニア海"},
{390,"イタリア南部"},
{391,"アルバニア"},
{392,"ギリシャ/アルバニア国境"},
{393,"ポルトガル マデイラ諸島"},
{394,"スペイン カナリア諸島"},
{395,"モロッコ"},
{396,"アルジェリア北部"},
{397,"チュニジア"},
{398,"イタリア シチリア"},
{399,"イオニア海"},
{400,"地中海中央部"},
{401,"リビア沿岸"},
{402,"北大西洋"},
{403,"大西洋中央海嶺北部"},
{404,"アゾレス諸島"},
{405,"アゾレス諸島"},
{406,"大西洋中央海嶺中部"},
{407,"アセンション島北方"},
{408,"アセンション島"},
{409,"南大西洋"},
{410,"大西洋中央海嶺南部"},
{411,"トリスタンダクーニャ諸島"},
{412,"ブーヴェ島"},
{413,"アフリカ南西方"},
{414,"大西洋南東部"},
{415,"アデン湾"},
{416,"ソコトラ島"},
{417,"アラビア海"},
{418,"インド ラクシャドウィープ"},
{419,"ソマリア北東部"},
{420,"北インド洋"},
{421,"カールスバーグ海嶺"},
{422,"モルディブ諸島"},
{423,"ラカディブ海"},
{424,"スリランカ"},
{425,"南インド洋"},
{426,"チャゴス諸島"},
{427,"モーリシャス/レユニオン"},
{428,"南西インド洋海嶺"},
{429,"中央インド洋海嶺"},
{430,"アフリカ南方"},
{431,"南アフリカ プリンスエドワード諸島"},
{432,"クロゼ諸島"},
{433,"ケルゲレン諸島"},
{434,"ブロークン海嶺"},
{435,"南東インド洋海嶺"},
{436,"ケルゲレン海台南部"},
{437,"オーストラリア南方"},
{438,"カナダ サスカチュワン州"},
{439,"カナダ マニトバ州"},
{440,"ハドソン湾"},
{441,"カナダ オンタリオ州"},
{442,"カナダ ハドソン海峡"},
{443,"カナダ ケベック州北部"},
{444,"デービス海峡"},
{445,"カナダ ラブラドル"},
{446,"ラブラドル海"},
{447,"カナダ ケベック州南部"},
{448,"カナダ ガスペ半島"},
{449,"カナダ ケベック州東部"},
{450,"カナダ アンチコスチ島"},
{451,"カナダ ニューブランズウィック州"},
{452,"カナダ ノバスコシア州"},
{453,"カナダ プリンスエドワード島"},
{454,"セントローレンス湾"},
{455,"カナダ ニューファンドランド"},
{456,"アメリカ モンタナ州"},
{457,"アメリカ アイダホ州"},
{458,"アメリカ モンタナ州ヘブゲン湖"},
{459,"アメリカ ワイオミング州イエローストーン"},
{460,"アメリカ ワイオミング州"},
{461,"アメリカ ノースダコタ州"},
{462,"アメリカ サウスダコタ州"},
{463,"アメリカ ネブラスカ州"},
{464,"アメリカ ミネソタ州"},
{465,"アメリカ アイオワ州"},
{466,"アメリカ ウィスコンシン州"},
{467,"アメリカ イリノイ州"},
{468,"アメリカ ミシガン州"},
{469,"アメリカ インディアナ州"},
{470,"カナダ オンタリオ州南部"},
{471,"アメリカ オハイオ州"},
{472,"アメリカ ニューヨーク州"},
{473,"アメリカ ペンシルバニア州"},
{474,"アメリカ バーモント/ニューハンプシャー州"},
{475,"アメリカ メーン州"},
{476,"アメリカ ニューイングランド南部"},
{477,"アメリカ メーン湾"},
{478,"アメリカ ユタ州"},
{479,"アメリカ コロラド州"},
{480,"アメリカ カンザス州"},
{481,"アメリカ アイオワ/ミズーリ州境"},
{482,"アメリカ ミズーリ/カンザス州境"},
{483,"アメリカ ミズーリ州"},
{484,"アメリカ ミズーリ/アーカンソー州境"},
{485,"アメリカ ミズーリ/イリノイ州境"},
{486,"アメリカ ミズーリ州"},
{487,"アメリカ ミズーリ州"},
{488,"アメリカ イリノイ州南部"},
{489,"アメリカ インディアナ州南部"},
{490,"アメリカ ケンタッキー州"},
{491,"アメリカ ウェストバージニア州"},
{492,"アメリカ バーニジア州"},
{493,"アメリカ チェサピーク湾"},
{494,"アメリカ ニュージャージー州"},
{495,"アメリカ アリゾナ州"},
{496,"アメリカ ニューメキシコ州"},
{497,"アメリカ テキサス州北西部/オクラホマ州境"},
{498,"アメリカ テキサス州西部"},
{499,"アメリカ オクラホマ州"},
{500,"アメリカ テキサス州中部"},
{501,"アメリカ アーカンソー/オクラホマ州境"},
{502,"アメリカ アーカンソー州"},
{503,"アメリカ ルイジアナ/テキサス州境"},
{504,"アメリカ ルイジアナ州"},
{505,"アメリカ ミシシッピ州"},
{506,"アメリカ テネシー州"},
{507,"アメリカ アラバマ州"},
{508,"アメリカ フロリダ州西部"},
{509,"アメリカ ジョージア州"},
{510,"アメリカ フロリダ/ジョージア州境"},
{511,"アメリカ サウスカロライナ州"},
{512,"アメリカ ノースカロライナ州"},
{513,"アメリカ東方沖"},
{514,"アメリカ フロリダ半島"},
{515,"バハマ諸島"},
{516,"アリゾナ州(アメリカ)/ソノラ州(メキシコ)境"},
{517,"ニューメキシコ州(アメリカ)/チワワ州(メキシコ)境"},
{518,"テキサス州(アメリカ)/メキシコ国境"},
{519,"アメリカ テキサス州南部"},
{520,"アメリカ テキサス州沿岸"},
{521,"メキシコ チワワ州"},
{522,"メキシコ北部"},
{523,"メキシコ中部"},
{524,"メキシコ ハリスコ州"},
{525,"メキシコ ベラクルス州"},
{526,"メキシコ湾"},
{527,"カンペチェ湾"},
{528,"ブラジル"},
{529,"ガイアナ"},
{530,"スリナム"},
{531,"仏領ギアナ"},
{532,"アイルランド"},
{533,"イギリス"},
{534,"北海"},
{535,"ノルウェー南部"},
{536,"スウェーデン"},
{537,"バルト海"},
{538,"フランス"},
{539,"ビスケー湾"},
{540,"オランダ"},
{541,"ベルギー"},
{542,"デンマーク"},
{543,"ドイツ"},
{544,"スイス"},
{545,"イタリア北部"},
{546,"オーストリア"},
{547,"チェコ及びスロバキア"},
{548,"ポーランド"},
{549,"ハンガリー"},
{550,"アフリカ北西部"},
{551,"アルジェリア南部"},
{552,"リビア"},
{553,"エジプト"},
{554,"紅海"},
{555,"アラビア半島西部"},
{556,"チャド"},
{557,"スーダン"},
{558,"エチオピア"},
{559,"アデン湾"},
{560,"ソマリア北西部"},
{561,"北西アフリカ南方沖"},
{562,"カメルーン"},
{563,"赤道ギニア"},
{564,"中央アフリカ共和国"},
{565,"ガボン"},
{566,"コンゴ共和国"},
{567,"コンゴ民主共和国"},
{568,"ウガンダ"},
{569,"ビクトリア湖"},
{570,"ケニア"},
{571,"ソマリア南部"},
{572,"タンガニーカ湖"},
{573,"タンザニア"},
{574,"マダガスカル北西方"},
{575,"アンゴラ"},
{576,"ザンビア"},
{577,"マラウイ"},
{578,"ナミビア"},
{579,"ボツワナ"},
{580,"ジンバブエ"},
{581,"モザンビーク"},
{582,"モザンビーク海峡"},
{583,"マダガスカル"},
{584,"南アフリカ共和国"},
{585,"レソト"},
{586,"スワジランド"},
{587,"南アフリカ沖"},
{588,"オーストラリア北西方"},
{589,"オーストラリア西方"},
{590,"オーストラリア ウェスタンオーストラリア"},
{591,"オーストラリア ノーザンテリトリー"},
{592,"オーストラリア サウスオーストラリア"},
{593,"オーストラリア カーペンタリア湾"},
{594,"オーストラリア クィーンズランド"},
{595,"コーラル海"},
{596,"ニューカレドニア北西方"},
{597,"ニューカレドニア"},
{598,"オーストラリア南西方"},
{599,"オーストラリア南方沖"},
{600,"オーストラリア南岸"},
{601,"オーストラリア ニューサウスウェールズ"},
{602,"オーストラリア ビクトリア"},
{603,"オーストラリア南東岸"},
{604,"オーストラリア東岸"},
{605,"オーストラリア東方"},
{606,"オーストラリア ノーフォーク島"},
{607,"ニュージーランド北西方"},
{608,"オーストラリア バス海峡"},
{609,"オーストラリア タスマニア"},
{610,"オーストラリア南東方"},
{611,"北太平洋"},
{612,"ハワイ諸島"},
{613,"ハワイ諸島"},
{614,"ミクロネシア連邦"},
{615,"マーシャル諸島"},
{616,"マーシャル諸島"},
{617,"マーシャル諸島"},
{618,"キリバス ギルバート諸島"},
{619,"ジョンストン島"},
{620,"キリバス ライン諸島"},
{621,"キリバス パルミラ島"},
{622,"キリバス キリティマティ"},
{623,"ツバル"},
{624,"キリバス フェニックス諸島"},
{625,"トケラウ諸島"},
{626,"クック諸島北部"},
{627,"クック諸島"},
{628,"ソシエテ諸島"},
{629,"トゥブアイ諸島"},
{630,"マルキーズ諸島"},
{631,"トゥアモトウ諸島"},
{632,"南太平洋"},
{633,"ロモノソフ海嶺"},
{634,"北極海"},
{635,"グリーンランド(カラーリットヌナート)北岸"},
{636,"グリーンランド(カラーリットヌナート)東部"},
{637,"アイスランド"},
{638,"アイスランド"},
{639,"ヤンマイエン島"},
{640,"グリーンランド海"},
{641,"スバールバル北方"},
{642,"ノルウェー海"},
{643,"ノルウェー スバールバル"},
{644,"フランツヨーゼフランド北方"},
{645,"ロシア フランツヨーゼフランド"},
{646,"ノルウェー北部"},
{647,"バレンツ海"},
{648,"ロシア ノバヤゼムリャ"},
{649,"カラ海"},
{650,"ロシア シベリア北西部沿岸"},
{651,"セーベルナヤゼムリャ北方"},
{652,"ロシア セーベルナヤゼムリャ"},
{653,"ロシア シベリア北部沿岸"},
{654,"セーベルナヤゼムリャ東方"},
{655,"ラプテフ海"},
{656,"ロシア シベリア南東部"},
{657,"ロシア東部/中国北東部国境"},
{658,"中国北東部"},
{659,"北朝鮮"},
{660,"日本海"},
{661,"ロシア 沿海地方"},
{662,"ロシア サハリン島"},
{663,"オホーツク海"},
{664,"中国南東部"},
{665,"黄海"},
{666,"中国南東部東方沖"},
{667,"ノボシビルスク(ニューシベリアン)諸島北方"},
{668,"ロシア ノボシビルスク(ニューシベリアン)諸島"},
{669,"東シベリア海"},
{670,"ロシア シベリア東部北岸"},
{671,"ロシア シベリア東部"},
{672,"チュクチ海"},
{673,"ベーリング海峡"},
{674,"アメリカ セントローレンス島"},
{675,"ボーフォート海"},
{676,"アメリカ アラスカ北部"},
{677,"カナダ ユーコン準州北部"},
{678,"カナダ クイーンエリザベス諸島"},
{679,"カナダ ノースウェスト準州"},
{680,"グリーンランド(カラーリットヌナート)西部"},
{681,"バフィン湾"},
{682,"カナダ バフィン島"},
{683,"中央太平洋南東部"},
{684,"東太平洋海膨南部"},
{685,"イースター島"},
{686,"西チリ海膨"},
{687,"チリ ファンフェルナンデス群島"},
{688,"ニュージーランド北島東方"},
{689,"ニュージーランド チャタム諸島"},
{690,"チャタム諸島南方"},
{691,"太平洋/南極海嶺"},
{692,"太平洋南部"},
{693,"中央太平洋東部"},
{694,"東太平洋海嶺中部"},
{695,"ガラパゴス諸島西方"},
{696,"ガラパゴス諸島"},
{697,"ガラパゴス諸島"},
{698,"ガラパゴス諸島南西方"},
{699,"ガラパゴス諸島南東方"},
{700,"タスマニア南方"},
{701,"マクオーリー島西方"},
{702,"バレニー諸島"},
{703,"インド アンダマン諸島"},
{704,"インド ニコバル諸島"},
{705,"インドネシア スマトラ北部西方沖"},
{706,"インドネシア スマトラ北部"},
{707,"マレー半島"},
{708,"タイ湾"},
{709,"アフガニスタン"},
{710,"パキスタン"},
{711,"カシミール南西部"},
{712,"インド/パキスタン国境"},
{713,"カザフスタン中部"},
{714,"ウズベキスタン"},
{715,"タジキスタン"},
{716,"キルギス"},
{717,"アフガニスタン/タジキスタン国境"},
{718,"アフガニスタン ヒンドゥークシ"},
{719,"タジキスタン/シンチアンウイグル自治区(中国)境"},
{720,"カシミール北西部"},
{721,"フィンランド"},
{722,"ノルウェー/ムルマンスク(ロシア)境"},
{723,"フィンランド/カレリア共和国(ロシア)境"},
{724,"バルト諸国/ベラルーシ/ロシア北西部"},
{725,"ロシア シベリア北西部"},
{726,"ロシア シベリア北・中部"},
{727,"南極 ビクトリアランド"},
{728,"ロス海"},
{729,"南極大陸"},
{730,"東太平洋海膨北部"},
{731,"ホンジュラス北方"},
{732,"サウスサンドウィッチ諸島東方"},
{733,"タイ"},
{734,"ラオス"},
{735,"カンボジア"},
{736,"ベトナム"},
{737,"トンキン湾"},
{738,"レイキャネス海嶺"},
{739,"アゾレス/セントビンセント岬海嶺"},
{740,"オーエン断裂帯"},
{741,"インド洋三重会合点"},
{742,"インド/南極海嶺西部"},
{743,"西サハラ"},
{744,"モーリタニア"},
{745,"マリ"},
{746,"セネガル/ガンビア"},
{747,"ギニア"},
{748,"シエラレオネ"},
{749,"リベリア"},
{750,"コートジボワール"},
{751,"ブルキナファソ"},
{752,"ガーナ"},
{753,"ベナン/トーゴ"},
{754,"ニジェール"},
{755,"ナイジェリア"},
{756,"イースター島南東方"},
{757,"ガラパゴス三重会合点"},
{758,"!震源名リストにありません"}
        };
    }
}
