using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public partial class InputDecisionForm : Form
    {
        public Decision result;
        public InputDecisionForm(Decision suggestion, List<status> possibleStatuses)
        {
            InitializeComponent();
            probOfWinningTB.Hide();
            ProbOfWinningLabel.Hide();
            hint_RTB.Hide();
            suggestedStatusTB.Text = suggestion.myStatus.ToString();
            suggestedBetTB.Text = suggestion.myBet.ToString();
            betTextBox.Text = Poker.minBet.ToString();
            LoadDecisionCombo(possibleStatuses);
        }


        void LoadDecisionCombo(List<status> possibleStatuses)
        {       
            for(int i = 0 ;i < possibleStatuses.Count;i++)
            {
                decisionComboBox.Items.Add(possibleStatuses[i]);
            }
            decisionComboBox.SelectedIndex = 0;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            result = new Decision((status)decisionComboBox.SelectedItem, int.Parse(betTextBox.Text));
            this.DialogResult = DialogResult.OK;
        }

        private void decisionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((status)decisionComboBox.SelectedItem == status.All_In)
                betTextBox.Text = Poker.myPlayers[0].MyChipsProperty.ToString();
            else if ((status)decisionComboBox.SelectedItem == status.Call)
                betTextBox.Text = Poker.minBet.ToString();
            else if ((status)decisionComboBox.SelectedItem == status.Check)
                betTextBox.Text = "0";
            else if ((status)decisionComboBox.SelectedItem == status.Fold)
                betTextBox.Text = "0";
            else
                betTextBox.Text = (Poker.minBet * 2).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 form1 = new Form1();
            this.Close();
            //form1.Close();
        }

        private void hintButton_Click(object sender, EventArgs e)
        {
            if (Poker.communityCards.Count == 0)
                return;

            Hand index = Hand.TwoPairs;
            for (int i = 0; i < Poker.myPlayers[0].probability.Count - 1; i++)
            {
                hint_RTB.AppendText(Math.Round(Poker.myPlayers[0].probability[i], 5).ToString() + " prob to get " + (++index).ToString());
                hint_RTB.AppendText(Environment.NewLine);
            }

            List<Card> cards = Poker.myPlayers[0].neededCards.Keys.ToList();
            List<Hand> hands = new List<Hand>();

            neededCards_RTB.AppendText("You need : ");
            neededCards_RTB.AppendText(Environment.NewLine);
            for (int i = 0; i < cards.Count; i++)
            {
                neededCards_RTB.AppendText(cards[i].myValue.ToString() + " of " + cards[i].myType.ToString() + " to get : ");
                neededCards_RTB.AppendText(Environment.NewLine);
                hands = Poker.myPlayers[0].neededCards[cards[i]];

                for (int j = 0; j < hands.Count; j++)
                {
                    neededCards_RTB.AppendText(hands[j].ToString());
                    if (j == hands.Count - 1)
                        neededCards_RTB.AppendText(".");
                    else
                        neededCards_RTB.AppendText(" , ");
                }

                neededCards_RTB.AppendText(Environment.NewLine);
            }

            probOfWinningTB.Text = Math.Round(Poker.myPlayers[0].probabilityOfWinning, 5).ToString();
            probOfWinningTB.Show();
            ProbOfWinningLabel.Show();
            hint_RTB.Show();
        }
    }
}
