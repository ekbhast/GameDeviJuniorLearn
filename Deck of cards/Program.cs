using System;
using System.Collections.Generic;

namespace Deck_of_cards
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            Dealer dealer = new Dealer();
            dealer.Play();
        }
    }

    class Dealer
    {
        private DeckOfCard _deckOfCard = new DeckOfCard();
        private Player _player;

        public Dealer()
        {
            _deckOfCard.AddCard(new Card("Тройка червей"));
            _deckOfCard.AddCard(new Card("Пятерка червей"));
            _deckOfCard.AddCard(new Card("Валет червей"));
            _deckOfCard.AddCard(new Card("Туз червей"));
            _deckOfCard.AddCard(new Card("Дама червей"));
            _deckOfCard.AddCard(new Card("Двойка червей"));
            _deckOfCard.AddCard(new Card("Джокер"));

            _player = new Player();
        }

        public void DealCards(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++) 
            {
                _player.ReceiveCard(_deckOfCard.DealCard());
            }
        }

        public void Play()
        {          
            Console.WriteLine("Введите сколько карт вам нужно");

            bool isNumber = false;

            while (isNumber == false)
            {
                if (isNumber = int.TryParse(Console.ReadLine(), out int numberOfCards))
                {
                    if (_deckOfCard.GetSize() < numberOfCards)
                    {
                        Console.WriteLine("В колоде не достаточно карт");
                    }
                    else
                    {
                        DealCards(numberOfCards);
                        _deckOfCard.ShowCards();
                        _player.ShowCards();
                    }                        
                }
                else
                {
                    Console.WriteLine("Вы ввели не число!");
                }
            }            
        }
    }

    class DeckOfCard
    {
        private Stack<Card> _cards = new Stack<Card>();

        public void AddCard(Card card)
        {
            _cards.Push(card);
        }

        public Card DealCard()
        {
            return _cards.Pop();
        }

        public void ShowCards()
        {
            Console.WriteLine("Карты оставлшиеся в колоде:");

            foreach (Card card in _cards)
            {
                Console.WriteLine(card.Name);
            }
        }

        public int GetSize()
        {
            return _cards.Count;
        }

    }

    class Card
    {
        public string Name { get; private set; }

        public Card(string name)
        {
            Name = name;
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void ReceiveCard(Card card)
        {
            _cards.Add(card);
        }

        public void ShowCards()
        {
            Console.WriteLine("\nКарты на руках у игрока:");

            foreach(Card card in _cards)
            {
                Console.WriteLine(card.Name);
            }
        }
    }
}