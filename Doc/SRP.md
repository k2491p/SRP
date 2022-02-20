# SOLIDの原則とは？

SOLIDは

- 変更に強い
- 理解しやすい
などのソフトウェアを作ることを目的とした原則です。

次の5つの原則があります。

- Single Responsibility Principle (単一責任の原則)
- Open-Closed Principle (オープン・クローズドの原則)
- Liskov Substitution Principle (リスコフの置換原則)
- Interface Segregation Principle (インタフェース分離の原則)
- Dependency Inversion Principle (依存関係逆転の原則)

上記5つの原則の頭文字をとって**SOLID**の原則と言います。
今回の記事では**Single Responsibility Principle (単一責任の原則)**について解説します。
その他の原則に関しては下記参照。
※随時追加予定！

# 簡単に言うと...

「**意味がわかる最小のまとまりにクラスを分けようね**」ということです。
その方が**変更に強く理解しやすいコード**になるというわけです。
具体例を用いながら詳しく見ていきましょう。

# 責任が単一ではない例

太郎というクラスを作ってみます。
***
【仕様】
太郎には一人の娘がいます。
太郎の娘は、お腹が空くといたずらをします。
太郎は娘がいたずらをすると叱ります。
***

~~いたずらされただけで叱りつけるなんて...~~
これを一つのクラスとして表現してみましょう。

``` c#:Taro.cs
class Taro
{
	public string DaughterName { get; set; }
	public bool IsHungryDaughter { get; set; }

	// お腹が空いていると娘はいたずらをする
	private bool PlaysTrick()
	{
		return IsHungryDaughter;
	}

	// 叱る
	public void Warn()
	{
		// 娘がいたずらすると太郎は叱ります
		if (PlaysTrick())
		{
			Console.WriteLine("こらっ、" + DaughterName + "ちゃん！！　だめだぞ！！！");
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```

以下のような仕様変更がありました。

***
【仕様】
太郎には一人の娘がいます。
太郎の娘は、お腹が空くといたずらをします。
太郎は娘がいたずらをすると叱ります。

**また太郎は会社員でもあります。
太郎には一人の後輩がいます。
後輩は、お腹が空いていてつかれていると仕事をサボります。
太郎は後輩が仕事をサボると叱ります。**

**娘を叱るときは父親らしく、後輩を叱るときは先輩らしく叱ります。**
***

先程のソースに後輩を追加し、後輩を叱るロジックを追加してみましょう。

``` c#:Taro.cs
class Taro
{
	public string DaughterName { get; set; }
	public bool IsHungryDaughter { get; set; }
	public string JuniorColleagueName { get; set; }
	public bool IsHungryJuniorColleague { get; set; }
	public bool IsTieredJuniorColleague { get; set; }

	// お腹が空いていると娘はいたずらをする
	private bool PlaysTrick()
	{
		return IsHungryDaughter;
	}

	// お腹が空いて疲れていると後輩は仕事をサボる
	private bool SkipsWork()
	{
		return IsHungryJuniorColleague && IsTieredJuniorColleague;
	}

	// 叱る
	public void Warn(bool isDaughter)
	{
		if (isDaughter)
		{
			// 娘がいたずらすると太郎は叱ります
			if (PlaysTrick())
			{
				Console.WriteLine("こらっ、" + DaughterName + "ちゃん！！　だめだぞ！！！");
			}
			else
			{
				Console.WriteLine("特に叱る必要もないか...");
			}
		}
		else
		{
			// 後輩が仕事をサボると太郎は叱ります
			if (SkipsWork())
			{
				Console.WriteLine(JuniorColleagueName + "さん、僕も仕事をサボりたいよ。");
			}
			else
			{
				Console.WriteLine("特に叱る必要もないか...");
			}
		}
		
	}
}
```
~~後輩を叱るどころか心の声が漏れてる...~~
Warnメソッドの中の処理が少し似ていますね。  
メッセージ出力のメソッドを共通化してShowWarnMessageというメソッドを追加してみましょう。

```c#:Taro.cs
	// ... 略
	// 叱る
	public void Warn(bool isDaughter)
	{
		if (isDaughter)
		{
			// 娘がいたずらすると太郎は叱ります
			warnMessage = "こらっ、" + DaughterName + "ちゃん！！　だめだぞ！！！";
			ShowWarnMessage(PlaysTrick(), warnMessage);
		}
		else
		{
			// 後輩が仕事をサボると太郎は叱ります
			warnMessage = JuniorColleagueName + "さん、僕も仕事をサボりたいよ。";
            ShowWarnMessage(SkipsWork(), warnMessage);
		}
	}

	private void ShowWarnMessage(bool DoneBadThing, string warnMessage)
	{
		if (DoneBadThing)
		{
			Console.WriteLine(warnMessage);
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
	// ... 略
```
なんとなく上手くメソッドを共通化できて、良いソースになった気がしますね。
最後にもう一つ仕様変更を加えます。

```tex:仕様
太郎には一人の娘がいます。
太郎の娘は、お腹が空くといたずらをします。
\sout{太郎は娘がいたずらをすると叱ります。}
\textbf{太郎は眠いときに娘にいたずらされると叱ります。}

また太郎は会社員でもあります。
太郎には一人の後輩がいます。
後輩は、お腹が空いていてつかれていると仕事をサボります。
太郎は後輩が仕事をサボると叱ります。

娘を叱るときは父親らしく、後輩を叱るときは先輩らしく叱ります。
```

~~自分が眠いときに叱る父親よ...~~
さて、上記仕様を追加してみましょう。

``` c#:Taro.cs
class Taro
{
	public bool IsSleepyTaro { get; set; }
	public string DaughterName { get; set; }
	public bool IsHungryDaughter { get; set; }
	public string JuniorColleagueName { get; set; }
	public bool IsHungryJuniorColleague { get; set; }
	public bool IsTieredJuniorColleague { get; set; }

	private bool PlaysTrick()
	{
		return IsHungryDaughter;
	}

	private bool SkipsWork()
	{
		return IsHungryJuniorColleague && IsTieredJuniorColleague;
	}

	public void Warn(bool isDaughter)
	{
		string warnMessage;
		if (isDaughter)
		{
			warnMessage = "こらっ、" + DaughterName + "ちゃん！！　だめだぞ！！！";
			ShowWarnMessage(PlaysTrick(), warnMessage);
		
		}
		else
		{
			warnMessage = JuniorColleagueName + "さん、僕も仕事をサボりたいよ。";
			ShowWarnMessage(SkipsWork(), warnMessage);
		}
	}

	private void ShowWarnMessage(bool DoneBadThing, string warnMessage)
	{
		// 太郎が眠いと叱るという処理をここに追加
		if (DoneBadThing && IsSleepyTaro)
		{
			Console.WriteLine(warnMessage);
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```
これで娘がいたずらをして、太郎が眠いときのみ娘をしかるというロジックは完成しました。
しかし、以上の修正により「後輩を叱る」ときのロジックにも影響が出てきます。
後輩を叱るときは太郎が眠いかどうかは関係ありません。
上記のロジックでは、後輩が仕事をサボっても太郎が眠くなければ叱らないということになってしまいます。
もちろん、`IsSleepyTaro`を入れる場所によってはバグは起きません。
しかし、ここで重要なのは、「娘に関する処理が後輩に関する処理と共通した処理を持っており、娘の処理を変更するために後輩に関する処理を知らなければいけない」ということです。
言い換えれば「関係ない処理を把握しなければ仕様変更ができない」ということになります。

# 責任が単一ではない例の問題点及び改善点
問題点は「**意味がわかる最小のまとまりにクラスが分かれていない**」ことです。
具体的には下記の4つの問題があります。

1. 役割の違う太郎が1つのクラスにまとまっている
2. 太郎が娘に関する情報やふるまいを持っている
3. 太郎が後輩に関する情報やふるまいを持っている
4. 娘と後輩が共通のロジックを持っている

1つずつ確認していきましょう。
## 1. 役割の違う太郎が1つのクラスにまとまっている
役割の違う太郎とは、

- 父親としての太郎(家庭内における父親という立場の太郎)
- 労働者としての太郎(職場内における先輩という立場の太郎)

です。
どこに居ようと太郎が太郎である事実は変わりません。
しかし、家庭内における太郎の役割と職場内における太郎の役割は全く同一のものでしょうか？
もちろん、違うものです。
一見、太郎が持つふるまいは「叱る」というもので、同じような機能に見えます。
しかし、同じような機能に見えるだけであり、役割は全く別のものです。
たとえば、今後の仕様変更で

- 娘を学校に送っていく
- 後輩を飲みに誘う

などのふるまいを追加していくとき、関係ない役割の処理が1つのクラスにまとまってしまうことになります。
これは、単一責任の原則の
「**意味がわかる最小のまとまりにクラスを分ける**」
に反しています。
もっと平たく言えば

- 変更に強い
- 理解しやすい

というソフトウェアを作る上での目的に反しています。
なぜなら、違う役割が混在しており、違う役割を知らなければ変更ができないからです。

上記を踏まえた上で、太郎クラスを

- 父親としての太郎クラス
- 先輩としての太郎クラス

に分けてみます。
2つのクラスに分けるにあたって、共通メソッドであるShowWarnMessageは共通クラス(TaroHelperクラス)に切り出しておきます。

``` c#:TaroFather.cs
class TaroFather
{
	public bool IsSleepyTaro { get; set; }
	public string DaughterName { get; set; }
	public bool IsHungryDaughter { get; set; }

	private bool PlaysTrick()
	{
		return IsHungryDaughter;
	}

	public void Warn()
	{
		string warnMessage;
		warnMessage = "こらっ、" + DaughterName + "ちゃん！！　だめだぞ！！！";
		TaroHelper.ShowWarnMessage(PlaysTrick() && IsSleepyTaro, warnMessage);
	}
}
```

``` c#:TaroWorker.cs
class TaroWorker
{
	public string JuniorColleagueName { get; set; }
	public bool IsHungryJuniorColleague { get; set; }
	public bool IsTieredJuniorColleague { get; set; }

	private bool SkipsWork()
	{
		return IsHungryJuniorColleague && IsTieredJuniorColleague;
	}

	public void Warn(bool isDaughter)
	{
		string warnMessage;
		warnMessage = JuniorColleagueName + "さん、僕も仕事をサボりたいよ。";
		TaroHelper.ShowWarnMessage(SkipsWork(), warnMessage);
	}
}
```

``` c#:TaroHelper.cs
class TaroHelper
{
	public static void ShowWarnMessage(bool DoneBadThing, string warnMessage)
	{
		if (DoneBadThing)
		{
			Console.WriteLine(warnMessage);
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```

## 2. 太郎が娘に関する情報やふるまいを持っている
次は、1.で分けた「TaroFatherクラス」の問題点について考えてみましょう。
太郎は娘に関する情報やふるまいを持っています。

- DaughterName(娘の名前：娘の情報)
- PlaysTric(いたずらをする：娘のふるまい)

太郎の役割は「娘がいたずらをしたら叱る」というものです。
娘の名前やいたずらをした理由を知らなくても「娘がいたずらをしたら叱る」という役割を果たすことができます。
つまり、太郎は役割を果たすのに必要最低限より多い情報を持ってしまっています。
意味がわかる最小のまとまりにクラスを分けていくと、

- TaroFatherクラス
- Daughterクラス

にわけられます。

``` c#:TaroFather.cs
class TaroFather
{
	public Daughter Daughter { get; set; }
	public bool IsSleepy { get; set; }

	public void Warn()
	{
		string warnMessage;
		warnMessage = "こらっ、" + Daughter.Name + "ちゃん！！　だめだぞ！！！";
		TaroHelper.ShowWarnMessage(Daughter.PlaysTrick(), warnMessage);
	}
}
```

``` c#:Daughter.cs
public class Daughter
{
	public string Name { get; set; }
	public bool IsHangry { get; set; }

	public bool PlaysTrick()
	{
		return IsHangry;
	}
}
```

## 3. 太郎が後輩に関する情報やふるまいを持っている
2.と同様に太郎と後輩を2つのクラスに分けていきましょう。

``` c#:TaroWorker.cs
class TaroWorker
{
	public JuniorColleague JuniorColleague { get; set; }

	public void Warn(bool isDaughter)
	{
		string warnMessage;
		warnMessage = JuniorColleague.Name + "さん、僕も仕事をサボりたいよ。";
		TaroHelper.ShowWarnMessage(JuniorColleague.SkipsWork(), warnMessage);
	}
}
```

``` c#:JuniorColleague.cs
public class JuniorColleague
{
	public JuniorColleague(string name)
	{
		Name = name;
	}

	public string Name { get; }
	public bool IsHangry { get; set; } = true;
	public bool IsTiered { get; set; } = true;

	public bool SkipsWork()
	{
		return IsHangry && IsTiered;
	}
}
```

## 4. 娘と後輩が共通のロジックを持っている
残る問題は娘と後輩が共通のロジックを持っているTaroHelperです。

``` c#:TaroHelper.cs
class TaroHelper
{
	public static void ShowWarnMessage(bool DoneBadThing, string warnMessage)
	{
		if (DoneBadThing)
		{
			Console.WriteLine(warnMessage);
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```

もともと「同じような叱るという動作において、同じようなメッセージを表示する」という理由から一つのメソッドとして共通化されていました。
しかし、実は同じように見えるだけで使われる場面や果たすべき役割は全く別です。
たとえば、**娘を叱る必要がない場合**のメッセージを変更するとします。
もちろん、**娘を叱る必要がない場合**しか考えないため、**後輩を叱る必要がない場合**についてのメッセージは変えません。
変更箇所は`Console.WriteLine("特に叱る必要もないか...");`となりますが、ここを変えてしまうと、**後輩を叱る必要がない場合**のメッセージが変わってしまいます。
たとえ同じように見えるものであっても、使われる場面や果たすべき役割が違う場合は共通化せず別のロジックとして扱う必要があります。
上記を踏まえ、TaroHelperは削除して、TaroFatherクラスとTaroWorkerクラスのWarnメソッドにロジックを書いていきます。

``` c#:TaroFather.cs
class TaroFather
{
	// ...略

	public void Warn()
	{
		if (Daughter.PlaysTrick() && IsSleepy)
		{
			Console.WriteLine("こらっ、" + Daughter.Name + "ちゃん！！　だめだぞ！！！");
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```

``` c#:TaroWorker.cs
class TaroWorker
{
	// ...略

	public void Warn()
	{
		if (JuniorColleague.SkipsWork())
		{
			Console.WriteLine(JuniorColleague.Name + "さん、僕も仕事をサボりたいよ。");
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```

# 「意味がわかる最小のまとまりにクラスを分ける」とは
元々は様々な情報やふるまいを持っていTaroクラスを

- TaroFather
- Daughter
- TaroWorker
- JuniorColleague

という4つのクラスに分けていきました。
確かに「意味ごとに小さなクラスに分ける」ということはできています。
では、「意味がわかる**最小**のまとまり」になっているでしょうか。
例えば、TaroWorkerというクラスは

- public JuniorColleague JuniorColleague
- public void Warn()

という1つの情報と1つのふるまいを持っています。
これを仮に2つのクラスにわけてみたらどうでしょうか。

``` c#:TaroWorker.cs
class TaroWorker
{
	public JuniorColleague JuniorColleague { get; set; }
}
```

``` c#:TaroWorkerHelper
class TaroWorkerHelper
{
	public void Warn(JuniorColleague juniorColleague)
	{
		if (juniorColleague.SkipsWork())
		{
			Console.WriteLine(juniorColleague.Name + "さん、僕も仕事をサボりたいよ。");
		}
		else
		{
			Console.WriteLine("特に叱る必要もないか...");
		}
	}
}
```

TaroWorkerクラスを見ると1つもふるまいを持っていません。
このクラスだけを見たとき、労働者としての太郎はふるまいを行わないと解釈できます。
しかし、実際にはTaroWorkerHelperクラスに太郎のふるまいがあります。
TaroWorkerHelperクラスの存在を知っていれば、太郎のふるまいがあることがわかりますが、TaroWorkerクラスとTaroWorkerHelperクラスをセットで見て初めてわかるということになります。
これでは「**意味がわかる**最小のまとまり」とは言えません。
したがって、TaroWorkerクラスは「意味がわかる最小のまとまり」と言えるでしょう。

# まとめ
単一責任の原則とは「**意味がわかる最小のまとまりにクラスを分けようね**」ということでした。
そうすることで下記のようなメリットが得られます。

- 変更に強い
- 理解しやすい

# 補足
本記事の中での単一責任の原則はざっくりとした理解を補助するもので正確性は欠いています。
正しい説明では
> モジュールを変更する理由はたったひとつだけであるべきである。
> モジュールはたったひとつのアクターに足して責務を負うべきである。

などと表現されています。
もう少し詳しいことが知りたい場合は、参考文献にあげているような本や記事を読んでみてください。

# 参考文献
- 本
  - [Clean Architecture 達人に学ぶソフトウェアの構造と設計](https://www.amazon.co.jp/Clean-Architecture-%E9%81%94%E4%BA%BA%E3%81%AB%E5%AD%A6%E3%81%B6%E3%82%BD%E3%83%95%E3%83%88%E3%82%A6%E3%82%A7%E3%82%A2%E3%81%AE%E6%A7%8B%E9%80%A0%E3%81%A8%E8%A8%AD%E8%A8%88-Robert-C-Martin/dp/4048930656)
- サイト
  - [単一責任原則で無責任な多目的クラスを爆殺する](https://qiita.com/MinoDriven/items/76307b1b066467cbfd6a)
  - [単一責任の原則とは何だったのか](https://qiita.com/magicant/items/2cf4acf592e54374e327)
  - [きれいなコードを書くためにSOLID原則を学びました① ~単一責任の原則~](https://qiita.com/suzuki0430/items/b13c7c1f637e7cd2146d)
  - [良い設計の第一歩!!単一責任の原則について](https://qiita.com/halkt/items/bd69962b605a00c3c842)
  - [単一責任の原則（Single responsibility principle）について、もう一度考える](https://www.ogis-ri.co.jp/otc/hiroba/others/OOcolumn/single-responsibility-principle.html)
  - [よくわかるSOLID原則1: S（単一責任の原則）](https://note.com/erukiti/n/n67b323d1f7c5)
- 動画
  - [オブジェクト指向の原則１：単一責務の原則とオープンクローズドの原則](https://www.udemy.com/course/objectfive1/)