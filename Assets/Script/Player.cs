using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player
	{
		// =======================
		// フィールド変数
		// =======================
		// 名前
		protected string name;
		// HP
		protected int hp;
		protected int defaultHP;
		// MP
		protected int mp;
		protected int defaultMP;
		// 攻撃力
		protected int str;

		// 防御力
		protected int def;

		// 幸運
		protected int luck;

		//速度
		protected int agi;

		// 麻痺
		protected bool paralyze;

		protected int paralyzeTurn;

		// 毒
		protected bool poison;

		//行動できるかどうか
		protected bool active;

		protected Strategy strategy;

		// 所属しているパーティー
		protected Party myParty;
		// =======================
		// コンストラクタ
		// =======================
		/**
		 * コンストラクタ
		 * @param name : プレイヤー名
		 */
		public Player(string name)
		{
			this.name = name;

			// キャラクターのパラメータ生成
			MakeCharacter();
		}

		// =======================
		// Getter / Setter
		// =======================
		/**
		 * プレイヤー名を取得する
		 * @return プレイヤー名
		 */
		public string GetName()
		{
			return this.name;
		}

		/**
		 * 現在HPを取得する
		 * @return 現在HP
		*/
		public int GetHP()
		{

			return this.hp;
		}
		/**
		 * 最大HPを取得する
		 * @return 最大HP
		 */
		public int GetDefaultHP()
		{
			return this.defaultHP;
		}
		/**
		 * 現在MPを取得する
		 * @return 現在MP
		*/
		public int GetMP()
		{
			return this.mp;
		}

		/**
		 * 最大MPを取得する
		 * @return 最大MP
		 */
		public int GetDefaultMP()
		{
			return this.defaultMP;
		}

		/**
		 * 攻撃力を取得する
		 * @return 攻撃力
		 */
		public int GetSTR()
		{
			return this.str;
		}

		/**
		 * 防御力を取得する
		 * @return 防御力
		 */
		public int GetDEF()
		{
			return this.def;
		}

		/**
		 * 幸運値を取得する
		 * @return 幸運値
		 */
		public int GetLUCK()
		{
			return this.luck;
		}

		/**
		 * 速さを取得する
		 * @return 速さ
		 */
		public int GetAGI()
		{
			return this.agi;
		}



		public bool isParalyze()
		{
			return this.paralyze;
		}

		public bool isActive()
		{
			return active;
		}

		public bool isPoison()
		{
			return this.poison;
		}


		public void SetParalyze(bool paralyze)
		{
			this.paralyze = paralyze;
			this.paralyzeTurn = 5;
		}
		public void RecoveryParalyze()
		{
			this.paralyze = false;
		}

		public int GetParalyzeTurn()
		{
			return this.paralyzeTurn;
		}
		public void ChangeParalyzeTurn()
		{
			if (paralyzeTurn > 0)
			{
				this.paralyzeTurn--;
			}
		}

		public void SetPoison(bool poison)
		{
			this.poison = poison;
		}




		/**
		 *自分のパーティーを取得する
		 *@return　自パーティー
		 */
		public Party GetMyParty()
		{
			return this.myParty;
		}


		/**
		 * 自分がどのパーティーに所属しているか設定する
		 * @param　myParty　：　自身の属するパーティー
		 */
		public void SetMyParty(Party myParty)
		{
			this.myParty = myParty;

		}

		/**
		 * 自分のパーティーの作戦を設定する
		 * @param strategy : 自分のパーティーの作戦
		 */
		public void SetStrategy(Strategy strategy)
		{
			this.strategy = strategy;
		}




		// =======================
		// protected メソッド
		// =======================
		/**
		 * 名前(name)からキャラクターに必要なパラメータを生成する
		 */
		protected virtual void MakeCharacter()
		{
			// ジョブごとにオーバーライドして処理を記述してください
		}

		/**
		 * 名前(name)からハッシュ値を生成し、指定された位置の数値を取り出す
		 * @param index : 何番目の数値を取り出すか
		 * @param max : 最大値(内部的に0～255の値を生成するが、0～maxまでの値に補正)
		 * @return 数値(0～max) ※maxも含む
		 */
		protected int GetNumber(int index, int max)
		{
			Debug.Log("GetNumberが呼び出されました");
			try
			{
				Debug.Log(this.name);
				// バイト配列に
				byte[] byteValue = Encoding.UTF8.GetBytes(this.name);

				// ハッシュ値の取得
				SHA1 crypto = new SHA1CryptoServiceProvider();
				byte[] hashValue = crypto.ComputeHash(byteValue);

				StringBuilder hashedText = new StringBuilder();
				for (int i = 0; i < hashValue.Length; i++)
				{
					hashedText.AppendFormat("{0:x2}", hashValue[i]);
				}
				Debug.Log("ハッシュ値：" + hashedText.ToString());
				Debug.Log("文字数：" + hashedText.Length.ToString());

				String hex = hashedText.ToString().Substring(index * 2, 2);
				Debug.Log(hex);

				int value = Int16.Parse(hex, System.Globalization.NumberStyles.HexNumber);
				Debug.Log(value);
				return value * max / 255;
			}
			catch (Exception e)
			{
				// エラー
				Console.WriteLine("例外をキャッチしました");
				Debug.Log("例外をキャッチしました");
				Console.WriteLine(e);
			}
			return 0;
		}

		// =======================
		// private メソッド
		// =======================

		// =======================
		// public メソッド
		// =======================
		/**
		 * 現在のステータスを System.out で表示する
		 */
		public string PrintStatus()
		{
			string status = "";
			status = string.Format("{0}(HP={1}/{2} : MP={3}/{4} : STR={5} : DEF={6} : LUCK={7} : AGI={8})\n", this.name, this.hp, this.defaultHP, this.mp, this.defaultMP, this.str, this.def, this.luck, this.agi);
		return status;
		}	

		/**
		 * Playerの行動を決定し行う
		 *
		 */
		public void Action(Player activePlayer, Party enemyParty)
		{

			strategy.StartegyAction(activePlayer, activePlayer.GetMyParty(), enemyParty);

		}

		/**
		 * 対象プレイヤー(passivePlayer)に攻撃を行う
		 * @param attacker : 攻撃側プレーヤー
		 * @param defender : 防御側プレイヤー
		 */
		public virtual void Attack(Player ActivePlayer, List<Player> passiveParty)
		{
			// ジョブごとにオーバーライドして処理を記述してください
		}

		/**
		 *対象プレイヤー(passivePlayer)に回復を行う
		 */
		public virtual bool HealHP(Player ActivePlayer, List<Player> passiveParty)
		{
			// ジョブごとにオーバーライドして処理を記述してください
			return false;
		}

		public virtual bool HealDebuff(Player ActivePlayer, Player passivePlayer)
		{
			// ジョブごとにオーバーライドして処理を記述してください
			return false;
		}

		/**
		 *対象プレイヤー(passivePlayer)に状態異常魔法を使う
		 */
		public bool Debuff(Player activePlayer, Player passivePlayer)
		{

			return false;
		}


		/**
		 * 対象プレイヤー(target)に対して与えるダメージを計算する
		 * @param target : 対象プレイヤー
		 * @return ダメージ値(0～)
		 */
		public int CalcDamage(Player target)
		{
			int critical = UnityEngine.Random.Range(1, 100);
			int damage;
			// 剣を持っているかどうか

			// 会心の一撃かどうか
			if (GetLUCK() > critical)
			{

				Console.WriteLine("会心の一撃！");

				return GetSTR();

			}

			damage = str - target.GetDEF();

			if (damage < 0)
			{
				return 0;
			}
			return damage;

		}

		/**
		 * ダメージを受ける
		 * @param damage : ダメージ値
		 */
		public void Damage(int damage)
		{
			// ダメージ値分、HPを減少させる
			this.hp = Math.Max(this.hp - damage, 0);
		}

		/**
		 * 会心の一撃
		 */

		/**
		* HPを回復する
		* @param damage : 回復値
		*/
		public void RecoverHP(int recoverHP)
		{
			// 回復量分、HPを上昇させる
			this.hp = Math.Min(this.hp + recoverHP, this.defaultHP);

		}

		/**
		 * MPを消費する
		 * @param usemp : 消費するMPの値
		 */
		public void UseMP(int usemp)
		{

			// 消費MP分、MPを減少させる
			this.mp = (Math.Max(this.mp - usemp, 0));
		}



		/**
		 * 毒状態になっている場合の処理
		 */
		public void ProcessPoison()
		{
			if (this.poison)
			{
				this.Damage(20);
			}
		}


		/**
		 * PlayerのHPがゼロになったときに表示する
		 */
		public void Down()
		{
			if (this.hp == 0)
			{
				Console.WriteLine(this.name + "は力尽きた...");
			}
		}

		public bool GetActive()
		{
			if (this.active)
			{
				return true;
			}
			return false;
		}
		public void ChangeActive(bool active)
		{
			this.active = active;
		}

	}