using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
namespace MP_Task_3___Texas_Holdem_Poker
{
    public partial class Form1 : Form
    {
    
       
        string path = (System.Environment.CurrentDirectory) + @"\Assets\";
        void InitCommunityPB()
        {
            Card back = new Card(0, 5);
            for (int i = 0; i < 5; i++)
            {
                PictureBox PBtmp = ((PictureBox)Controls.Find("com" + (i + 1) + "_PictureBox", true)[0]);
                PBtmp.Image = Image.FromFile(path + "table.jpg");
              //  PBtmp.SizeMode = PictureBoxSizeMode.CenterImage;
                PBtmp.Refresh();

            }
        }
        void InitPlayersPB()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 2; j++)
                {
                    PictureBox PBtmp = ((PictureBox)Controls.Find("P" + (i + 1) + "C" + (j + 1) + "_PictureBox", true)[0]);
                    PBtmp.Visible = true;
                    PBtmp.Image = Image.FromFile(path + "bg.jpg");
                   // PBtmp.SizeMode = PictureBoxSizeMode.CenterImage;
                    PBtmp.Refresh();
                }
        }
        public Form1()
        {
            InitializeComponent();
            pokerPictureBox.Image = Image.FromFile(path + "bg.jpg");
            tablePictureBox.Image = Image.FromFile(path + "table.jpg");
            tablePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            InitPlayersPB();
            InitCommunityPB();
        }

        void UpdatePlayers()
        {
            for (int i = 0; i < Poker.numOfPlayers; i++)
                for (int j = 0; j < 2; j++)
                {
                    PictureBox PBtmp = ((PictureBox)Controls.Find("P" + (i + 1) + "C" + (j + 1) + "_PictureBox", true)[0]);
                    PBtmp.Visible =true;
                    PBtmp.Image = Poker.myPlayers[i].myCards[j].GetCardImage(true);
                    PBtmp.SizeMode = PictureBoxSizeMode.StretchImage;
                    PBtmp.Refresh();
                }
        }
        void Deactivateplayer(int i)
        {
            for (int j = 0; j < 2; j++)
            {
                PictureBox PBtmp = ((PictureBox)Controls.Find("P" + (i + 1) + "C" + (j + 1) + "_PictureBox", true)[0]);
                PBtmp.Visible = true;
                PBtmp.Image = Poker.myPlayers[i].myCards[j].GetCardImage(false);
                PBtmp.SizeMode = PictureBoxSizeMode.StretchImage;
                PBtmp.Refresh();
               
            }
        }
        void UpdateCommunity()
        {
            for (int i = 0; i < Poker.communityCards.Count; i++)
                {
                    PictureBox PBtmp = ((PictureBox)Controls.Find("com" + (i + 1) + "_PictureBox", true)[0]);
                    PBtmp.Image = Poker.communityCards[i].GetCardImage(true);
                    PBtmp.SizeMode = PictureBoxSizeMode.StretchImage;
                    PBtmp.Refresh();
                }
        }
        void FlipCommunity()
        {
            Card back = new Card(0, 5);
            for (int i = 0; i < 5; i++)
            {
                PictureBox PBtmp = ((PictureBox)Controls.Find("com" + (i + 1) + "_PictureBox", true)[0]);
                PBtmp.Image = back.GetCardImage(false);
                PBtmp.SizeMode = PictureBoxSizeMode.StretchImage;
                PBtmp.Refresh();

            }
        }
        void InitChipsTB()
        {
            for (int i = 0; i < Poker.numOfPlayers; i++)
            {
                TextBox TBtmp = ((TextBox)Controls.Find("p" + (i + 1) + "Chips_TB", true)[0]);
                TBtmp.Text = Poker.myPlayers[i].MyChipsProperty.ToString();
                TBtmp.Refresh();
            }
        }

        void TakesBet()
        {
            for (int m = 0; m < Poker.numOfPlayers; m++)
                Poker.myPlayers[m].getMyHand();
            int raisedplayer = -1;
            bool raising = false;
            List<status> possibleStatuses;

            //0 1 2 3  
            for (int k = 0; k < Poker.numOfPlayers; k++)
            {
                if (raisedplayer == k)
                    break;

                if (!Poker.myPlayers[k].Active)
                    continue;


                if (raising || Poker.communityCards.Count == 0)
                    possibleStatuses = new List<status>(){status.Raise, status.Fold, status.Call, status.All_In};
                else
                    possibleStatuses = new List<status>(){status.Raise, status.Fold, status.Check, status.All_In};

                if (k != 0)
                {
                    Poker.myPlayers[k].getMyDecision(possibleStatuses);
                    Poker.decisions[k] = Poker.myPlayers[k].myDecision;
                }
                else 
                {
                    Poker.myPlayers[k].getMyDecision(possibleStatuses);
                    Decision suggested = Poker.myPlayers[k].myDecision;
                    Poker.decisions[k] = GetHumanDecision(suggested, possibleStatuses);
                }

                TextBox TBtmp = ((TextBox)Controls.Find("p" + (k + 1) + "Bet_TextBox", true)[0]);
                TBtmp.Text = Poker.decisions[k].myBet.ToString();

                Label label = ((Label)Controls.Find("p" + (k + 1) + "_status", true)[0]);
                label.Text = Poker.decisions[k].myStatus.ToString();
                Thread.Sleep(1500);
                TBtmp.Refresh();
                if (Poker.decisions[k].myStatus == status.Fold)
                {
                    Poker.myPlayers[k].Active = false;
                    Deactivateplayer(k);
                }
                else if (Poker.decisions[k].myStatus == status.Raise)
                {
                    raising = true;
                    raisedplayer = k;
                    Poker.minBet = Poker.decisions[k].myBet;
                }
                else if (Poker.decisions[k].myStatus == status.All_In)
                {
                    raising = true;
                    raisedplayer = k;
                    Poker.minBet = Poker.decisions[k].myBet;
                }

                if (k == Poker.numOfPlayers - 1 && raising)
                {
                    k = -1;
                    raising = false;
                }
                

            }

            for (int i = 0; i < Poker.numOfPlayers; i++)
            {
                Poker.myPlayers[i].MyChipsProperty -= Poker.decisions[i].myBet;
                TextBox TBtmp = ((TextBox)Controls.Find("p" + (i + 1) + "Chips_TB", true)[0]);
                TBtmp.Text = Poker.myPlayers[i].MyChipsProperty.ToString();
                TBtmp.Refresh();
            }

            Poker.myDealer.addToPot(Poker.decisions.Sum((bet => bet.myBet)));
            totalPot_TextBox.Text = Poker.myDealer.Pot.ToString();
            totalPot_TextBox.Refresh();
            Poker.minBet = Poker.originalMinBet;
        }

        public Decision GetHumanDecision(Decision suggestion, List<status> possibleStatuses)
        {
            InputDecisionForm testDialog = new InputDecisionForm(suggestion, possibleStatuses);
            Decision result = new Decision();
            
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                result = testDialog.result;
            }
            testDialog.Dispose();

            return result;
        }
        
        void StartGame()
        {
            Poker.Initialize(4, 100, 5);
            InitChipsTB();
            int sleeptime = 1500;

            for (int i = 0; i < Poker.numOfRounds; i++)
            {
                FlipCommunity();

                Poker.PreFlop();              
                UpdatePlayers();
                TakesBet();
                if (checkActivity())
                    continue;
                Thread.Sleep(sleeptime);

                Poker.Flop();
                UpdateCommunity();
                TakesBet();
                if (checkActivity())
                    continue;
                Thread.Sleep(sleeptime);

                Poker.Turn();
                UpdateCommunity();
                TakesBet();
                if (checkActivity())
                    continue;
                Thread.Sleep(sleeptime);

                Poker.River();
                UpdateCommunity();             
                TakesBet();
                if (checkActivity())
                    continue;

                Thread.Sleep(sleeptime);
                Poker.communityCards.Clear();
                Thread.Sleep(sleeptime);
               
                Poker.EndOfRound();
                Thread.Sleep(sleeptime);
            }
        }

        private bool checkActivity()
        {
            List<Player> activePlayers = new List<Player>();
            activePlayers = Poker.myPlayers.FindAll(player => player.Active == true);

            if (activePlayers.Count == 1)
            {
                Poker.myPlayers[Poker.myPlayers.IndexOf(activePlayers[0])].MyChipsProperty += Poker.myDealer.Pot;
                return true;
            }
            return false;
        }

        private void pokerPictureBox_Paint(object sender, PaintEventArgs e)
        {   //Draw dealer
            Image newImage = Image.FromFile(path + "dealer.png");
            Rectangle destRect = new Rectangle(530, 5, 110, 110);
            e.Graphics.DrawImage(newImage, destRect);      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}